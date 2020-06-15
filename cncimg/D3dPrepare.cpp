#include "D3dPrepare.h"

#include "VertexFormats.h"

#include "CncImageAPI.h"
#include "DemoCellClass.h"

namespace Graphic
{
	std::vector<int> VxlFiles;
	std::vector<int> TmpFiles;
	std::vector<int> SlopeFilesSW;

	std::vector<int> SceneObjects;

	int MouseObject;
	int CliffObject, CliffExtraObject;
	int UnitPalette, TmpPalette, SnoPalette, DesPalette;
	int ShpFile;
	int roadTileFile;
	int roadObject[3];
	int alphaObject1, alphaObject2;
}

bool Graphic::TryCreateIndexedTexture()
{
	auto pDevice = SceneClass::Instance.GetDevice();
	if (!pDevice)
		return false;

	LPDIRECT3DTEXTURE9 pIndexed = nullptr, pPalette = nullptr;
	ShpFileClass ShpFile;
	Palette Pal;

	LPD3DXBUFFER pErrorMessage = nullptr, pCodeBuffer = nullptr;
	D3DXHANDLE hConstant = NULL;
	LPDIRECT3DPIXELSHADER9 pPixShader = nullptr;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer = nullptr;
	LPD3DXCONSTANTTABLE pConstantTable = nullptr;

	auto pShaderFileName = ".\\shaders\\transformation.hlsl";
	auto pShaderTypeName = "ps_3_0";
	auto pShaderMainFuncName = "tsmain";
	const int SourceImageIndex = 0;
	const int PaletteImageIndex = 1;

	if(!ShpFile.LoadFromFile("images\\ggcnst.shp"))
		return false;

	Pal.LoadFromFile("palettes\\unittem.pal");

	auto FrameBound = ShpFile.GetFrameBounds(0);
	auto width = FrameBound.right - FrameBound.left;
	auto height = FrameBound.bottom - FrameBound.top;
	auto pIndex = ShpFile.GetFrameData(0);

	D3DLOCKED_RECT LockedRect;

	if (!pIndex)
		return false;

	if (FAILED(pDevice->CreateTexture(width, height, 0, NULL, D3DFMT_L8, D3DPOOL_MANAGED, &pIndexed, nullptr)))
		goto Failed;

	if (FAILED(pDevice->CreateTexture(1, 256, 0, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pPalette, nullptr)))
		goto Failed;

	if (FAILED(pIndexed->LockRect(0, &LockedRect, nullptr, D3DLOCK_DISCARD)))
		goto Failed;

	auto pTextureData = reinterpret_cast<PBYTE>(LockedRect.pBits);
	if (ShpFile.HasCompression(0))
	{
		for (int i = 0; i < height; i++)
		{
			auto size = *reinterpret_cast<short*>(pIndex);
			auto source = pIndex + sizeof(short);
			auto dest = pTextureData;

			while (source < pIndex + size)
			{
				if (auto ncolor = *source++) {
					*dest++ = ncolor;
				}
				else {
					dest += *source++;
				}
			}
			pIndex += size;
			pTextureData += LockedRect.Pitch;
		}
	}
	else
	{
		for (int i = 0; i < height; i++)
		{
			RtlCopyMemory(pTextureData, pIndex, width);
			pTextureData += LockedRect.Pitch;
			pIndex += width;
		}
	}

	pIndexed->UnlockRect(0);

	if (FAILED(pPalette->LockRect(0, &LockedRect, nullptr, D3DLOCK_DISCARD)))
		goto Failed;

	auto pColors = reinterpret_cast<PDWORD>(LockedRect.pBits);
	for (int i = 0; i < 256; i++)
	{
		auto palcolor = Pal[i];

		if (i == 0)
			pColors[i] = D3DCOLOR_ARGB(0, palcolor.R, palcolor.G, palcolor.B);
		else
			pColors[i] = D3DCOLOR_XRGB(palcolor.R, palcolor.G, palcolor.B);
	}

	pPalette->UnlockRect(0);

	if (FAILED(pDevice->CreateVertexBuffer(4 * sizeof PlainVertex, D3DUSAGE_DYNAMIC, PlainVertex::dwFVFType,
		D3DPOOL_SYSTEMMEM, &pVertexBuffer, nullptr)))
		goto Failed;

	PlainVertex* pVertexData = nullptr;
	if (FAILED(pVertexBuffer->Lock(0, 0, reinterpret_cast<void**>(&pVertexData), D3DLOCK_DISCARD)))
		goto Failed;

	pVertexData[0] = { {0.0,0.0,0.0},1.0f,0.0,0.0 };
	pVertexData[1] = { { (float)width,0.0,0.0 },1.0f,1.0f,0.0 };
	pVertexData[2] = { {0.0,(float)height,0.0},1.0f,0.0,1.0f };
	pVertexData[3] = { {(float)width,(float)height,0.0},1.0f,1.0f,1.0f };
	pVertexBuffer->Unlock();

	if (FAILED(D3DXCompileShaderFromFileA(pShaderFileName, nullptr, nullptr, pShaderMainFuncName, pShaderTypeName,
		D3DXSHADER_DEBUG, &pCodeBuffer, &pErrorMessage, &pConstantTable)))
	{
		if (pErrorMessage)
			printf_s("%s.\n", pErrorMessage->GetBufferPointer());

		goto Failed;
	}

	if (FAILED(pDevice->CreatePixelShader(reinterpret_cast<PDWORD>(pCodeBuffer->GetBufferPointer()), &pPixShader)))
		goto Failed;
	//coords is transformed

	pDevice->Clear(0, nullptr, D3DCLEAR_ZBUFFER | D3DCLEAR_TARGET, D3DCOLOR_XRGB(0,0,0), 1.0f, 0);
	if (SUCCEEDED(pDevice->BeginScene()))
	{
		LPDIRECT3DVERTEXBUFFER9 PrevStream;
		UINT PrevStride, PrevOffset;
		LPDIRECT3DBASETEXTURE9 PrevTex1, PrevTex2;
		LPDIRECT3DPIXELSHADER9 PrevShader;

		pDevice->SetFVF(PlainVertex::dwFVFType);

		pDevice->GetTexture(SourceImageIndex, &PrevTex1);
		pDevice->GetTexture(PaletteImageIndex, &PrevTex2);
		pDevice->GetStreamSource(0, &PrevStream, &PrevOffset, &PrevStride);
		pDevice->GetPixelShader(&PrevShader);

		pDevice->SetTexture(SourceImageIndex, pIndexed);
		pDevice->SetTexture(PaletteImageIndex, pPalette);
		pDevice->SetStreamSource(0, pVertexBuffer, 0, sizeof PlainVertex);
		pDevice->SetPixelShader(pPixShader);

		pDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2);

		pDevice->SetTexture(SourceImageIndex, PrevTex1);
		pDevice->SetTexture(PaletteImageIndex, PrevTex2);
		pDevice->SetStreamSource(0, PrevStream, PrevOffset, PrevStride);
		pDevice->SetPixelShader(PrevShader);

		pDevice->EndScene();
		pDevice->Present(nullptr, nullptr, NULL, nullptr);

		printf_s("draw complete.\n");
	}

	printf_s("pause program.\n");
	getchar();

	SAFE_RELEASE(pIndexed);
	SAFE_RELEASE(pPalette);
	SAFE_RELEASE(pVertexBuffer);
	SAFE_RELEASE(pErrorMessage);
	SAFE_RELEASE(pCodeBuffer);
	SAFE_RELEASE(pPixShader);
	SAFE_RELEASE(pConstantTable);
	return true;

Failed:
	SAFE_RELEASE(pIndexed);
	SAFE_RELEASE(pPalette);
	SAFE_RELEASE(pVertexBuffer);
	SAFE_RELEASE(pErrorMessage);
	SAFE_RELEASE(pCodeBuffer);
	SAFE_RELEASE(pPixShader);
	SAFE_RELEASE(pConstantTable);
	return false;
}

bool Graphic::Direct3DInitialize(HWND hWnd, const char* pShotFileName, bool bUnion, int nDirections, int TurretOff)
{
	return SetUpScene(hWnd) && PrepareVertexBuffer(pShotFileName, bUnion, nDirections, TurretOff);
}

void Graphic::Direct3DUninitialize()
{
	return ClearSceneObjects();
}

bool Graphic::PrepareVertexBuffer(const char* pShotFileName, bool bUnion, int nDirections, int TurretOff)
{
	const float TileLength = 30.0f*sqrt(2.0);
	const float CellHeight = 10.0f * sqrt(3.0);

	UnitPalette = CreatePaletteFile("palettes\\unittem.pal");
	TmpPalette = CreatePaletteFile("palettes\\isotem.pal");
	SnoPalette = CreatePaletteFile("palettes\\isosno.pal");
	DesPalette = CreatePaletteFile("palettes\\isodes.pal");
/*
	if (TryCreateIndexedTexture())
	{
		printf_s("create indexed texture successfully.\n");
	}*/

	if (!SetSceneFont("RussellSquare", 14))
	{
		printf_s("failed to set font.\n");
		return false;
	}

	if (auto id = CreateStringObjectAtScene({ 0.0,-100.0,0.0 }, RGB(242, 0, 242), "702"))
	{
		printf_s("print success.\n");
	}

	if (auto id = CreateLineObjectAtScene({ 0.0,0.0,0.1f }, { 0.0,-300.0f,0.1f }, D3DCOLOR_XRGB(242, 0, 0), D3DCOLOR_XRGB(0, 0, 0)))
	{
		printf_s("Line success.\n");
	}

	if (MouseObject = CreateRectangleObjectAtScene({ 0.0,0.0,0.1f },88,88,D3DCOLOR_XRGB(76,76,100)))
	{
		printf_s("Rect success.\n");
	}

	SetBackgroundColor(0, 0, 0);

	if (!UnitPalette || !TmpPalette || !SnoPalette || !DesPalette)
		return false;

	//MakeVxlFrameShot(VxlFiles[0], "Shot.png", 0, 0.0, 0.0, 0.0, UnitPalette, INVALID_COLOR_VALUE);
	if (pShotFileName) {
		MakeShots(pShotFileName, 0.0, UnitPalette, bUnion, nDirections, INVALID_COLOR_VALUE, TurretOff);
	}

	if (roadTileFile = CreateTmpFile("tile\\proad01a.tem")) {
		LoadTmpTextures(roadTileFile);
	}

	if (auto id = CreateVxlFile("flata.vxl")) {
		VxlFiles.push_back(id);
	}

	if (auto id = CreateVxlFile("flata___.vxl")) {
		VxlFiles.push_back(id);
	}
/*
	if (auto id = CreateCircularCommonTextureFile(280.0,4.0,D3DCOLOR_XRGB(242,0,242))) {
		CreateCommonTextureObjectAtScene(id, { 0.0,0.0,0.0 });
	}
	*/

	if (VxlFiles.size() >= 2)
	{
		if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette, RGB(0, 252, 0))) {
			SceneObjects.push_back(vxlid);
			SetObjectColorCoefficient(vxlid, { 1.0f,0.6f,0.6f,1.0f });
		}

		if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[1], { 0.0,0.0,0.0 }, 0.0, 0.0, D3DX_PI / 2.0f, UnitPalette, RGB(0, 0, 252))) {
			SceneObjects.push_back(vxlid);
			//SetObjectColorCoefficient(vxlid, { 0.6f,1.0f,0.6f,0.2f });
		}
	}

	if (ShpFile = CreateShpFile("images\\ggcnst.shp")) {
		if (LoadShpTextures(ShpFile, 2) && LoadShpTextures(ShpFile, 4)) {
			/*MouseObject = */CreateShpObjectAtScene(ShpFile, { 0.0,0.0,0.1f }, 2, UnitPalette, RGB(0, 252, 252), 2, 4, 4, 8, SPECIAL_NORMAL);
			CreateShpObjectAtScene(ShpFile, { 0.0,0.0,0.1f }, 4, UnitPalette, RGB(0, 252, 252), 1, 4, 4, 8, SPECIAL_SHADOW);
		}
	}


	if (auto sid = CreateShpFile("images\\repring.shp")) {
		if (LoadShpTextures(sid, 0)) {
			alphaObject1 = CreateShpObjectAtScene(sid, { 0.0,0.0,0.1f }, 0,
				UnitPalette, RGB(0, 252, 252), 0, 4, 4, 8, SPECIAL_ALPHA);
			alphaObject2 = CreateShpObjectAtScene(sid, { 0.0,-200.0,0.1f }, 0,
				UnitPalette, RGB(0, 252, 252), 0, 4, 4, 8, SPECIAL_ALPHA);
		}
	}
	if (auto vid = CreateVxlFile("images\\ytnk.vxl"))
	{
		if (auto tid = CreateVxlFile("images\\ytnktur.vxl"))
		{
			auto Position = D3DXVECTOR3(200.0, 0, 0);
			CreateVxlObjectAtScene(vid, Position, 0, 0, 0, UnitPalette, INVALID_COLOR_VALUE);
			CreateVxlObjectAtScene(tid, Position, 0, 0, -D3DX_PI / 4.0f , UnitPalette, INVALID_COLOR_VALUE);
		}
	}
	D3DXVECTOR3 Position{ 100.0f,0.0f,0.0f };
	int baseid, turid, barlid;

	char cIndex = 'a';
	char szFileName[MAX_PATH];
	for (int i = 0; i < 8; i++) {
		sprintf_s(szFileName, "Tile\\clear01%c.tem", cIndex + i);

		if (auto id = CreateTmpFile(szFileName)) {
			TmpFiles.push_back(id);
			LoadTmpTextures(id);
			//LoadTmpTextures(id, TmpPalette);
		}
	}

	if (auto vid = CreateShpFile("images\\ygggun.shp")) {
		if (!LoadShpTextures(vid, 0))
			printf_s("failed to load as texture.\n");
		if (auto tid = CreateVxlFile("images\\yaggun.vxl")) {

			float turretY = 15.0f;
			float delta = turretY*2.0f / sqrt(3.0);

			CreateShpObjectAtScene(vid, Position, 0, UnitPalette, INVALID_COLOR_VALUE, 1, 1, 1, 4, false);
			Position.z -= delta;
			if (!CreateVxlObjectAtScene(tid, Position, 0, 0, D3DX_PI, UnitPalette, RGB(252, 0, 0)))
				printf_s("failed to put vxl.\n");
		}
	}

	for (int i = 0; i < 4; i++) {
		sprintf_s(szFileName, "Tile\\rmpfx12%c.tem", cIndex + i);

		if (auto id = CreateTmpFile(szFileName)) {
			SlopeFilesSW.push_back(id);
			LoadTmpTextures(id);
		}
	}
/*
	if (auto id = CreateTmpFile("cliff05.tem")) {
		LoadTmpTextures(id, TmpPalette);
		CreateTmpObjectAtScene(id, { 0.0,3.0f*TileLength,0.0 }, 2, CliffObject, CliffExtraObject);
		SetObjectColorCoefficient(CliffExtraObject, { 1.0f,0.6f,1.0f,0.3f });
	}
*/
	//SetColorScheme(GetCurrentTheater(), 8848, RGB(0, 252, 0));

/*
	if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette,RGB(252,0,0))) {
		MouseObject = vxlid;
	}
*/
	int idxTile, idxExtra;

	if (!TmpFiles.empty()) {
		for (int x = -4; x < 6; x++) {
			for (int y = -4; y < 6; y++) {
				auto RamdomIndex = Randomizer::RandomRanged(0, TmpFiles.size());
				auto Position = D3DXVECTOR3((-0.5 + x)*TileLength, (-0.5 + y)*TileLength, 0.0f);

				if (y >= 0 && y < 3) {
					roadObject[y] = CellClass::CreateCellAt(Position, TmpPalette, roadTileFile, y);
					continue;
				}

				if (CellClass::CreateCellAt(Position, TmpPalette, TmpFiles[RamdomIndex], 0)) {
				}
				else {
					printf_s("failed to draw.\n");
				}
			}
		}
	}
/*
	for (auto tiles : TmpFiles) {
		LoadTmpTextures(tiles, TmpPalette);
	}
*/
	int out;
	if (auto fid = CreateTmpFile("tile\\grdrl10.des"))
	{
		LoadTmpTextures(fid);
		CreateTmpObjectAtScene(fid, { 0.0f,7.5f*TileLength,0.0f }, DesPalette, 0, out, out);
	}

	if (auto fid = CreateTmpFile("tile\\cliffz04.sno"))
	{
		LoadTmpTextures(fid);
		CreateTmpObjectAtScene(fid, { 0.0f,8.5f*TileLength,0.0f }, SnoPalette, 2, out, out);
	}

	if (!TmpFiles.empty() && !SlopeFilesSW.empty())
	for (int x = 0; x < 10; x++) {
		if (CellClass::CreateCellAt(
			{ (-4.5f + x)*TileLength,-6.5f*TileLength,CellHeight },
			TmpPalette,
			SlopeFilesSW[Randomizer::RandomRanged(0, SlopeFilesSW.size())],
			0)) {
		}
		if (CellClass::CreateCellAt({ (-4.5f + x)*TileLength,-5.5f*TileLength,0.0 },
			TmpPalette,
			SlopeFilesSW[Randomizer::RandomRanged(0, SlopeFilesSW.size())],
			0)) {
		}
	}

	//RemoveObjectFromScene(SceneObjects[1]);
	//SceneObjects.erase(SceneObjects.begin() + 1);
	return true;
}

void Graphic::DrawScene()
{
	PresentAllObject();
}

void Graphic::WorldRotation()
{
	if (SceneObjects.size() >= 2) {
		RotateObject(SceneObjects[0], 0.0, 0.0, 0.01);
		MoveObject(SceneObjects[1], { 0.0,0.05f,0.0 });
	}
}

bool Graphic::PrepareTileGround()
{
	return false;
}

bool Graphic::DrawTileGround()
{
	return false;
}

void Graphic::ClearScene()
{
	return ClearSceneObjects();
}

bool Graphic::HandleDeviceLost()
{
	return SceneClass::Instance.HandleDeviceLost();
}

void Graphic::InitliazeDeviceState()
{
	SceneClass::Instance.InitializeDeviceState();
}

void Graphic::ResetDevice()
{
	if (!ResetSceneView())
		printf_s("failed to handle.\n");
}

void Graphic::SetCamera()
{
	SceneClass::Instance.SetUpCamera();
}

void Graphic::MoveFocus(float x, float y)
{
	MoveFocusOnScreen(x, y);
}

void Graphic::PlaceVXL(POINT Position)
{
	static float StaticAngle = 0.0;
	const float TileLength = 30.0f*sqrt(2.0);
	const float CellHeight = 30.0f / sqrt(3.0);

	if (VxlFiles.size()) {
		D3DXVECTOR3 ScenePositon;

		ClientPositionToScenePosition(Position, ScenePositon);

		if (auto id = CreateVxlObjectAtScene(VxlFiles[0], ScenePositon, 0.0, 0.0, StaticAngle, UnitPalette,RGB(252,0,252))) {
			//RotateObject(id, -atan(CellHeight / TileLength), 0.0, 0.0);
			SceneObjects.push_back(id);
		}

		StaticAngle += 0.2;
	}
}

void Graphic::MouseMove(POINT Position)
{/*
	if (!MouseObject)
		return;
*/
	D3DXVECTOR3 TargetPosition;

	ClientPositionToScenePosition(Position, TargetPosition);
	TargetPosition.z += 0.1f;

	SetObjectLocation(MouseObject, TargetPosition);

	CellClass::MarkCellByMousePosition(Position);
}

void Graphic::RemoveLastTmp()
{
	RemoveTmpFile(TmpFiles.back());
	TmpFiles.erase(TmpFiles.end() - 1);
	RemoveShpFile(ShpFile);
	RemoveVxlFile(VxlFiles[1]);
	//VxlFiles.erase(VxlFiles.begin() + 1);
}

void Graphic::SceneRotation()
{
	RotateWorld(0.05f);
}

void Graphic::RemoveAlphaObjects()
{
	RemoveObjectFromScene(alphaObject1);
	RemoveObjectFromScene(alphaObject2);
}
