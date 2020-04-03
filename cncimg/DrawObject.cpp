#include "VertexFormats.h"

#include "DrawObject.h"
#include "VxlMath.h"
#include "VxlFile.h"
#include "VPLFile.h"

#include "SceneClass.h"

#include <algorithm>

#define PAINTING_START_ID 4

long PaintingStruct::ID = PAINTING_START_ID;

std::unordered_map<int, PaintingStruct*> DrawObject::GlobalOpaqueObjects;
std::unordered_map<int, PaintingStruct*> DrawObject::GlobalTransperantObjects;
std::vector<LPDIRECT3DTEXTURE9> DrawObject::IsolatedTextures;
DWORD DrawObject::idTextureManagementThread = 0;
HANDLE DrawObject::hTextureManagementThread = INVALID_HANDLE_VALUE;

void DrawObject::UpdateScene(LPDIRECT3DDEVICE9 pDevice, DWORD dwBackground)
{
	if (!pDevice)
		return;

	SceneClass::Instance.ResetShaderMatrix();
	//integrate first, than sort by distance, finally;
	//draw opaque first, transpetant objects then;
	//should check if the object is within our sight
	std::vector<PaintingStruct*> DrawingOpaqueObject, DrawingTransperantObject;

	DrawingOpaqueObject.reserve(GlobalOpaqueObjects.size());
	DrawingTransperantObject.reserve(GlobalTransperantObjects.size());

	for (auto& pair : GlobalOpaqueObjects) {
		if (pair.second->IsWithinSight())
			DrawingOpaqueObject.push_back(pair.second);
	}

	for (auto& pair : GlobalTransperantObjects) {
		if (pair.second->IsWithinSight())
			DrawingTransperantObject.push_back(pair.second);
	}

	//the shorter the distance is , the later they will be painted
	auto DistanceCompairFunction = [](PaintingStruct*& Left, PaintingStruct*& Right)->bool {
		return SceneClass::Instance.GetDistanceToScreen(Left->Position + Left->CompareOffset) > 
			SceneClass::Instance.GetDistanceToScreen(Right->Position + Right->CompareOffset);
	};

	std::sort(DrawingOpaqueObject.begin(), DrawingOpaqueObject.end(), DistanceCompairFunction);
	std::sort(DrawingTransperantObject.begin(), DrawingTransperantObject.end(), DistanceCompairFunction);
	
	pDevice->Clear(0, nullptr, D3DCLEAR_ZBUFFER | D3DCLEAR_TARGET, dwBackground, 1.0f, 0);
	if (SUCCEEDED(pDevice->BeginScene()))
	{
		for (auto paint : DrawingOpaqueObject) {
			paint->Draw(pDevice);
		}

		for (auto paint : DrawingTransperantObject) {
			paint->Draw(pDevice);
		}
		pDevice->EndScene();
	}

	auto hResult = pDevice->Present(nullptr, nullptr, NULL, nullptr);
	if (hResult == D3DERR_DEVICELOST) {
		while (!SceneClass::Instance.HandleDeviceLost());
	}
}

void DrawObject::CommitIsotatedTexture(LPDIRECT3DTEXTURE9 pTexture)
{
	if (IsTextureIsolated(pTexture))
		return;

	IsolatedTextures.push_back(pTexture);
}

bool DrawObject::IsTextureIsolated(LPDIRECT3DTEXTURE9 pTexture)
{
	auto find = std::find_if(IsolatedTextures.begin(), IsolatedTextures.end(), [pTexture](LPDIRECT3DTEXTURE9& item)->bool {
		return item == pTexture;
	});

	return find != IsolatedTextures.end();
}

bool DrawObject::CanIsolatedTextureUnloadNow(LPDIRECT3DTEXTURE9 pTexture)
{
	unsigned long reference = 0;
	for (auto& item : GlobalTransperantObjects) {
		if (item.second->pTexture == pTexture)
			reference++;
	}

	for (auto& item : GlobalOpaqueObjects) {
		if (item.second->pTexture == pTexture)
			reference++;
	}

	return reference == 0;
}

void DrawObject::UnloadIsolatedTexture(LPDIRECT3DTEXTURE9 pTexture)
{
	auto find = std::find_if(IsolatedTextures.begin(), IsolatedTextures.end(), [pTexture](LPDIRECT3DTEXTURE9& item)->bool {
		return item == pTexture;
	});

	if (find != IsolatedTextures.end()) {
		IsolatedTextures.erase(find);
		pTexture->Release();
	}
}

DWORD WINAPI DrawObject::TextureManagementThreadProc(LPVOID pNothing)
{
	while (true) 
	{
		for (auto item : IsolatedTextures) 
		{
			if (CanIsolatedTextureUnloadNow(item))
				UnloadIsolatedTexture(item);
		}
		Sleep(5000u);
	};
}

DrawObject::~DrawObject()
{
	this->ClearObjects();
	this->ClearAllObjects();
}

void DrawObject::ClearObjects()
{
	for (auto& Pair : this->CreatedVertex)
		Pair.first->Release();
	this->CreatedVertex.clear();

	for (auto& Pair : this->CreatedExtraVertex)
		Pair.first->Release();
	this->CreatedExtraVertex.clear();
}

LPDIRECT3DVERTEXBUFFER9 DrawObject::FindDrawnObject(D3DXVECTOR3 Position)
{
	if (!this->CreatedVertex.size())
		return nullptr;

	for (auto& pair : this->CreatedVertex) {
		if (pair.second == Position)
			return pair.first;
	}
	return nullptr;
}

LPDIRECT3DVERTEXBUFFER9 DrawObject::FindDrawnExtraObject(D3DXVECTOR3 Position)
{
	if (!this->CreatedExtraVertex.size())
		return nullptr;

	for (auto& pair : this->CreatedExtraVertex) {
		if (pair.second == Position)
			return pair.first;
	}
	return nullptr;
}

void DrawObject::AddDrawnObject(LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, D3DXVECTOR3 Position)
{
	if (this->FindDrawnObject(Position))
		return;

	this->CreatedVertex[pVertexBuffer] = Position;
}

void DrawObject::AddDrawnExtraObject(LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, D3DXVECTOR3 Position)
{
	if (this->FindDrawnExtraObject(Position))
		return;

	this->CreatedExtraVertex[pVertexBuffer] = Position;
}

void DrawObject::RemoveFromScene(D3DXVECTOR3 Position)
{
	if (auto find = this->FindDrawnObject(Position)) {
		find->Release();
		this->CreatedVertex.erase(find);
	}

	if (auto find = this->FindDrawnExtraObject(Position)) {
		find->Release();
		this->CreatedExtraVertex.erase(find);
	}

	this->RemoveTextures(Position);
}

void DrawObject::TransformObject(D3DXVECTOR3 Position, D3DXMATRIX & Matirx)
{
	D3DVERTEXBUFFER_DESC Desc;
	LPVOID pVertecies;

	if (auto find = this->FindDrawnObject(Position))
	{
		find->GetDesc(&Desc);
		find->Lock(0, 0, &pVertecies, D3DLOCK_DISCARD);
		if (Desc.FVF == Vertex::dwFVFType)
		{
			auto pVertexData = reinterpret_cast<Vertex*>(pVertecies);
			auto Count = Desc.Size / sizeof Vertex;
			for (int i = 0; i < Count; i++)
				pVertexData[i].Vector *= Matirx;
		}
		else if (Desc.FVF == TexturedVertex::dwFVFType)
		{
			auto pVertexData = reinterpret_cast<TexturedVertex*>(pVertecies);
			auto Count = Desc.Size / sizeof TexturedVertex;
			for (int i = 0; i < Count; i++)
				pVertexData[i].Vector *= Matirx;
		}
	}

	if (auto find = this->FindDrawnExtraObject(Position))
	{
		find->GetDesc(&Desc);
		find->Lock(0, 0, &pVertecies, D3DLOCK_DISCARD);
		if (Desc.FVF == Vertex::dwFVFType)
		{
			auto pVertexData = reinterpret_cast<Vertex*>(pVertecies);
			auto Count = Desc.Size / sizeof Vertex;
			for (int i = 0; i < Count; i++)
				pVertexData[i].Vector *= Matirx;
		}
		else if (Desc.FVF == TexturedVertex::dwFVFType)
		{
			auto pVertexData = reinterpret_cast<TexturedVertex*>(pVertecies);
			auto Count = Desc.Size / sizeof TexturedVertex;
			for (int i = 0; i < Count; i++)
				pVertexData[i].Vector *= Matirx;
		}
	}
}

void DrawObject::MoveObject(D3DXVECTOR3 Position, D3DXVECTOR3 ToPosition)
{
	D3DXMATRIX Matrix;
	D3DXVECTOR3 Displace = ToPosition - Position;
	D3DXMatrixTranslation(&Matrix, Displace.x, Displace.y, Displace.z);
	
	if (this->FindDrawnExtraObject(ToPosition) || this->FindDrawnObject(ToPosition))
		return;

	if (auto find = this->FindDrawnObject(Position))
	{
		this->TransformObject(Position, Matrix);
		this->CreatedVertex[find] = ToPosition;
		if (auto extra = this->FindDrawnExtraObject(Position))
			this->CreatedExtraVertex[extra] = ToPosition;
	}
}

void DrawObject::RotateObject(D3DXVECTOR3 Position, float RotationX, float RotationY, float RotationZ)
{
	D3DXVECTOR3 ToZero = -Position;
	D3DXMATRIX RotateX, RotateY, RotateZ, TranslationToZero, TranslationBack, Matrix;
	D3DXMatrixTranslation(&TranslationToZero, ToZero.x, ToZero.y, ToZero.z);
	D3DXMatrixTranslation(&TranslationBack, Position.x, Position.y, Position.z);
	D3DXMatrixRotationX(&RotateX, RotationX);
	D3DXMatrixRotationY(&RotateY, RotationY);
	D3DXMatrixRotationZ(&RotateZ, RotationZ);

	Matrix = TranslationToZero*RotateX*RotateY*RotateZ*TranslationBack;
	this->TransformObject(Position, Matrix);
}

void DrawObject::AddTextureAtPosition(D3DXVECTOR3 Position, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindTexture(Position))
		return;

	this->PositionVertextable.push_back(std::make_pair(Position, pTexture));
}

void DrawObject::AddExtraTextureAtPosition(D3DXVECTOR3 Position, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindExrtaTexture(Position))
		return;

	this->PositionExtraVertextable.push_back(std::make_pair(Position, pTexture));
}

void DrawObject::RemoveTextures(D3DXVECTOR3 Position)
{
	auto it = std::remove_if(this->PositionVertextable.begin(), this->PositionVertextable.end(),
		[Position](std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>&it) {return it.first == Position; });
	if (it != this->PositionVertextable.end())
		this->PositionVertextable.erase(it);

	it = std::remove_if(this->PositionExtraVertextable.begin(), this->PositionExtraVertextable.end(),
		[Position](std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>&it) {return it.first == Position; });
	if (it != this->PositionExtraVertextable.end())
		this->PositionExtraVertextable.erase(it);
}

LPDIRECT3DTEXTURE9 DrawObject::FindTexture(D3DXVECTOR3 Position)
{
	if (!this->PositionVertextable.size())
		return nullptr;

	auto it = std::find_if(this->PositionVertextable.begin(), this->PositionVertextable.end(),
		[Position](std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>&it) {return it.first == Position; });
	if (it != this->PositionVertextable.end())
		return it->second;
	return nullptr;
}

LPDIRECT3DTEXTURE9 DrawObject::FindExrtaTexture(D3DXVECTOR3 Position)
{
	if (!this->PositionExtraVertextable.size())
		return nullptr;

	auto it = std::find_if(this->PositionExtraVertextable.begin(), this->PositionExtraVertextable.end(),
		[Position](std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>&it) {return it.first == Position; });
	if (it != this->PositionExtraVertextable.end())
		return it->second;
	return nullptr;
}

void DrawObject::UpdateObject(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position)
{
	auto pTexture = this->FindTexture(Position);
	//auto pExtraTexture = this->FindExrtaTexture(Position);

	D3DVERTEXBUFFER_DESC Desc;
	LPDIRECT3DBASETEXTURE9 pOldTexture;

	if (auto find = this->FindDrawnObject(Position))
	{
		find->GetDesc(&Desc);
		if (SUCCEEDED(pDevice->BeginScene()))
		{
			pDevice->GetTexture(0, &pOldTexture);
			pDevice->SetTexture(0, pTexture);
			pDevice->SetFVF(Desc.FVF);
			pDevice->SetStreamSource(0, find, 0, Desc.FVF == Vertex::dwFVFType ? sizeof Vertex : sizeof TexturedVertex);
			if (Desc.FVF == Vertex::dwFVFType)
				pDevice->DrawPrimitive(D3DPT_POINTLIST, 0, Desc.Size / sizeof Vertex);
			else
				pDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);
			pDevice->EndScene();
			pDevice->SetTexture(0, pOldTexture);
		}
	}
	/*
	if (auto find = this->FindDrawnExtraObject(Position))
	{
		find->GetDesc(&Desc);
		if (SUCCEEDED(pDevice->BeginScene()))
		{
			pDevice->GetTexture(0, &pOldTexture);
			pDevice->SetTexture(0, pExtraTexture);
			pDevice->SetFVF(Desc.FVF);
			pDevice->SetStreamSource(0, find, 0, sizeof TexturedVertex);
			pDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);
			pDevice->EndScene();
			pDevice->SetTexture(0, pOldTexture);
		}
	}
	*/
}

void DrawObject::UpdateExtraObject(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position)
{
	auto pExtraTexture = this->FindExrtaTexture(Position);

	D3DVERTEXBUFFER_DESC Desc;
	LPDIRECT3DBASETEXTURE9 pOldTexture;

	if (auto find = this->FindDrawnExtraObject(Position))
	{
		find->GetDesc(&Desc);
		if (SUCCEEDED(pDevice->BeginScene()))
		{
			pDevice->GetTexture(0, &pOldTexture);
			pDevice->SetTexture(0, pExtraTexture);
			pDevice->SetFVF(Desc.FVF);
			pDevice->SetStreamSource(0, find, 0, sizeof TexturedVertex);
			pDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);
			pDevice->EndScene();
			pDevice->SetTexture(0, pOldTexture);
		}
	}
}

void DrawObject::UpdateAllObject(LPDIRECT3DDEVICE9 pDevice)
{
	//cell first
	for (auto& pair : this->CreatedVertex)
		this->UpdateObject(pDevice, pair.second);

	for (auto& pair : this->CreatedExtraVertex)
		this->UpdateExtraObject(pDevice, pair.second);
}

int DrawObject::CommitTransperantObject(PaintingStruct & Object)
{
	int nCurrentID = PaintingStruct::ID;
	InterlockedAdd(&PaintingStruct::ID, 2);

	this->TransperantImageTable[nCurrentID] = Object;
	GlobalTransperantObjects[nCurrentID] = &this->TransperantImageTable[nCurrentID];
	return nCurrentID;
}

int DrawObject::CommitOpaqueObject(PaintingStruct & Object)
{
	int nCurrentID = PaintingStruct::ID;
	InterlockedAdd(&PaintingStruct::ID, 2);

	this->OpaqueImageTable[nCurrentID] = Object;
	GlobalOpaqueObjects[nCurrentID] = &this->OpaqueImageTable[nCurrentID];
	return nCurrentID;
}

void DrawObject::ClearAllObjects()
{
	for (auto&pair : this->TransperantImageTable) {
		pair.second.pVertexBuffer->Release();
		GlobalTransperantObjects.erase(pair.first);
	}

	for (auto& pair : this->OpaqueImageTable) {
		pair.second.pVertexBuffer->Release();
		GlobalOpaqueObjects.erase(pair.first);
	}

	this->TransperantImageTable.clear();
	this->OpaqueImageTable.clear();
}

void DrawObject::RemoveTransperantObject(int nID)
{
	auto find = this->TransperantImageTable.find(nID);
	if (find != this->TransperantImageTable.end()) {
		find->second.pVertexBuffer->Release();

		this->TransperantImageTable.erase(nID);
		GlobalTransperantObjects.erase(nID);
	}
}

void DrawObject::RemoveOpaqueObject(int nID)
{
	auto find = this->OpaqueImageTable.find(nID);
	if (find != this->OpaqueImageTable.end()) {
		find->second.pVertexBuffer->Release();

		this->OpaqueImageTable.erase(nID);
		GlobalOpaqueObjects.erase(nID);
	}
}

PaintingStruct * DrawObject::FindObjectById(int nID)
{
	auto find = GlobalTransperantObjects.find(nID);
	if (find != GlobalTransperantObjects.end())
		return find->second;

	find = GlobalOpaqueObjects.find(nID);
	if (find != GlobalOpaqueObjects.end())
		return find->second;
	return nullptr;
}

void DrawObject::ObjectTransformation(int nID, D3DXMATRIX & Matrix)
{
	D3DVERTEXBUFFER_DESC Desc;
	Vertex* pVertexData;
	TexturedVertex* pTexturedVertexData;
	auto pFind = FindObjectById(nID);

	if (!pFind || !pFind->pVertexBuffer)
		return;

	auto &Vpl = VPLFile::GlobalVPL;
	pFind->pVertexBuffer->GetDesc(&Desc);
	if (Desc.FVF == Vertex::dwFVFType)
	{
		//is vxl vertices
		if (pFind->BufferedNormals.empty() || pFind->BufferedVoxels.empty() ||
			pFind->BufferedVoxels.size() != pFind->BufferedNormals.size() ||
			pFind->BufferedVoxels.size() != Desc.Size / sizeof Vertex)
			return;

		Palette Palette;
		if (auto palette = Palette::FindPaletteByID(pFind->nPaletteID))
			Palette = *palette;
		else
			return;

		Palette.MakeRemapColor(pFind->dwRemapColor);
		if (FAILED(pFind->pVertexBuffer->Lock(0, 0, (void**)&pVertexData, D3DLOCK_DISCARD)))
			return;

		D3DXMATRIX NormalMatrix = Matrix;
		NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;// no translation

		pFind->Position *= Matrix;
		//process vertecies and position
		for (int i = 0; i < Desc.Size / sizeof Vertex; i++)
		{
			pVertexData[i].Vector *= Matrix;

			DWORD dwColor;
			auto& OriginalNormalVec = pFind->BufferedNormals[i];
			auto OriginalVoxelData = pFind->BufferedVoxels[i];

			OriginalNormalVec *= NormalMatrix;
			auto fAngle = std::acos((VxlFile::LightReversed * OriginalNormalVec) /
				D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&OriginalNormalVec));

			if (fAngle >= D3DX_PI / 2)
			{
				auto& Color = Palette[Vpl[0].Table[OriginalVoxelData.nColor]];
				dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
			}
			else
			{
				int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
				auto& Color = Palette[Vpl[nIndex].Table[OriginalVoxelData.nColor]];
				dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
			}

			pVertexData[i].dwColor = dwColor;
		}
		pFind->pVertexBuffer->Unlock();
	}
	else
	{
		//is plane art
		if (FAILED(pFind->pVertexBuffer->Lock(0, 0, (void**)&pTexturedVertexData, D3DLOCK_DISCARD)))
			return;

		pFind->Position *= Matrix;
		//process coords only
		for (int i = 0; i < Desc.Size / sizeof TexturedVertex; i++)
			pTexturedVertexData[i].Vector *= Matrix;

		pFind->pVertexBuffer->Unlock();
	}
	pFind->InitializeVisualRect();
}

void DrawObject::ObjectDisplacement(int nID, D3DXVECTOR3 Displacement)
{
	D3DXMATRIX Translation;

	D3DXMatrixTranslation(&Translation, Displacement.x, Displacement.y, Displacement.z);
	ObjectTransformation(nID, Translation);
}

void DrawObject::ObjectMove(int nID, D3DXVECTOR3 Target)
{
	auto pFind = FindObjectById(nID);
	if (!pFind)
		return;

	auto Displacement = Target - pFind->Position;
	ObjectDisplacement(nID, Displacement);
}

//do not rotate plane arts
void DrawObject::ObjectRotation(int nID, float RotationX, float RotationY, float RotationZ)
{
	D3DXMATRIX TranslationZero;
	D3DXMATRIX TranslationBack;
	D3DXMATRIX RotateX, RotateY, RotateZ;
	D3DXMATRIX Transformation;

	auto pFind = FindObjectById(nID);
	if (!pFind)
		return;
	
	auto Position = pFind->Position;
	D3DXMatrixTranslation(&TranslationZero, -Position.x, -Position.y, -Position.z);
	D3DXMatrixTranslation(&TranslationBack, Position.x, Position.y, Position.z);
	D3DXMatrixRotationX(&RotateX, RotationX);
	D3DXMatrixRotationY(&RotateY, RotationY);
	D3DXMatrixRotationZ(&RotateZ, RotationZ);
	D3DXMatrixIdentity(&Transformation);

	Transformation = Transformation*TranslationZero*RotateX*RotateY*RotateZ*TranslationBack;
	ObjectTransformation(nID, Transformation);
}

void DrawObject::SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient)
{
	auto pFind = FindObjectById(nID);
	if (!pFind)
		return;

	pFind->SetColorCoefficient(Coefficient);
}


void PaintingStruct::InitializePaintingStruct(PaintingStruct & Object, 
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, 
	D3DXVECTOR3 Position, 
	LPDIRECT3DTEXTURE9 pTexture, 
	std::vector<Voxel>* BufferedVoxels, 
	std::vector<D3DXVECTOR3>* BufferedNormals,
	int nPaletteID,
	DWORD dwRemapColor
)
{
	Object.pVertexBuffer = pVertexBuffer;
	Object.Position = Position;
	Object.pTexture = pTexture;
	Object.nPaletteID = nPaletteID;
	Object.dwRemapColor = dwRemapColor;

	if (BufferedVoxels)
		Object.BufferedVoxels = *BufferedVoxels;

	if (BufferedNormals)
		Object.BufferedNormals = *BufferedNormals;

	Object.SetColorCoefficient(D3DXVECTOR4(1.0, 1.0, 1.0, 1.0));
	Object.SetCompareOffset(D3DXVECTOR3(0.0, 0.0, 0.0));
	Object.InitializeVisualRect();
}

//should BeginScene() at first
bool PaintingStruct::Draw(LPDIRECT3DDEVICE9 pDevice)
{
	if (!pDevice || !this->pVertexBuffer)
		return false;

	bool Result = false;
	D3DVERTEXBUFFER_DESC Desc;
	LPDIRECT3DBASETEXTURE9 pFormerTexture;
	LPDIRECT3DPIXELSHADER9 pFormerShader;

	auto& VxlShader = SceneClass::Instance.GetVXLShader();
	auto& PlainShader = SceneClass::Instance.GetPlainArtShader();

	this->pVertexBuffer->GetDesc(&Desc);
	if (Desc.FVF == Vertex::dwFVFType)
	{
		//is vxl, requires Voxels and Normals,count = Desc.size / sizeof Vertex
		pDevice->GetTexture(0, &pFormerTexture);
		pDevice->GetPixelShader(&pFormerShader);

		pDevice->SetTexture(0, nullptr);
		pDevice->SetFVF(Desc.FVF);
		pDevice->SetStreamSource(0, this->pVertexBuffer, 0, sizeof Vertex);

		VxlShader.SetConstantVector(pDevice, this->ColorCoefficient);
		pDevice->SetPixelShader(VxlShader.GetShaderObject());

		Result = SUCCEEDED(pDevice->DrawPrimitive(D3DPT_POINTLIST, 0, Desc.Size / sizeof Vertex));
		pDevice->SetTexture(0, pFormerTexture);
		pDevice->SetPixelShader(pFormerShader);
	}
	else // Textured vertex.fvf
	{
		//requires pTexture, always 2 rectangles
		pDevice->GetTexture(0, &pFormerTexture);
		pDevice->GetPixelShader(&pFormerShader);

		pDevice->SetTexture(0, this->pTexture);
		pDevice->SetFVF(Desc.FVF);
		pDevice->SetStreamSource(0, this->pVertexBuffer, 0, sizeof TexturedVertex);

		PlainShader.SetConstantVector(pDevice, this->ColorCoefficient);
		pDevice->SetPixelShader(PlainShader.GetShaderObject());

		Result = SUCCEEDED(pDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, Desc.Size / sizeof TexturedVertex - 2));
		pDevice->SetTexture(0, pFormerTexture);
		pDevice->SetPixelShader(pFormerShader);
	}

	return Result;
}

void PaintingStruct::InitializeVisualRect()
{
	const FLOAT VisualHeightFactor = sqrt(3.0) / 2.0f;
	auto& Scene = SceneClass::Instance;

	this->VisualRect = EmptyRect;

	if (!Scene.IsDeviceLoaded() || !this->pVertexBuffer)
		return;

	auto CurrentFocus = Scene.GetFocus();
	RECT ObjectRect;
	D3DVERTEXBUFFER_DESC Desc;

	this->pVertexBuffer->GetDesc(&Desc);
	if (Desc.FVF == Vertex::dwFVFType)
	{
		//is vxl buffer
		auto Point = Scene.CoordsToScreen(this->Position);
		//256*256 rect
		ObjectRect = { Point.x - 128,Point.y - 128,Point.x + 128,Point.y + 128 };
	}
	else
	{
		//is plane art
		TexturedVertex* pTexturedVertexData;
		if (FAILED(this->pVertexBuffer->Lock(0, 0, (void**)&pTexturedVertexData, D3DLOCK_READONLY)))
			return;

		int MinX, MinY, MaxX, MaxY;
		auto FirstPoint = Scene.CoordsToScreen(pTexturedVertexData[0].Vector);
		MinX = MaxX = FirstPoint.x;
		MinY = MaxY = FirstPoint.y;
		for (int i = 0; i < Desc.Size / sizeof TexturedVertex; i++) {
			auto Point = Scene.CoordsToScreen(pTexturedVertexData[i].Vector);
			if (Point.x < MinX)
				MinX = Point.x;
			if (Point.x > MaxX)
				MaxX = Point.x;
			if (Point.y < MinY)
				MinY = Point.y;
			if (Point.y > MaxY)
				MaxY = Point.y;
		}

		this->pVertexBuffer->Unlock();
		ObjectRect = { MinX,MinY,MaxX + 1,MaxY + 1 };
	}

	this->VisualRect = ObjectRect;
}

bool PaintingStruct::IsWithinSight()
{
	auto& Scene = SceneClass::Instance;

	if (!Scene.IsDeviceLoaded() || !this->pVertexBuffer)
		return false;

	RECT SceneViewRect = Scene.GetCurrentViewPort();
	RECT ObjectRect = this->VisualRect;

	return IntersectRect(&ObjectRect, &ObjectRect, &SceneViewRect);
}

void PaintingStruct::SetCompareOffset(D3DXVECTOR3 Offset)
{
	this->CompareOffset = Offset;
}

void PaintingStruct::SetColorCoefficient(D3DXVECTOR4 Coefficient)
{
	this->ColorCoefficient = Coefficient;
}
