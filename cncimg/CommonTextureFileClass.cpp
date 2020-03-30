#include "VertexFormats.h"
#include "CommonTextureFileClass.h"

std::unordered_map<int, std::unique_ptr<CommonTextureFileClass>> CommonTextureFileClass::FileObjectTable;

void CommonTextureFileClass::ClearAllObjectForAllFile()
{
	for (auto& file : FileObjectTable) {
		if (file.second)
			file.second->ClearAllObjects();
	}
}

CommonTextureFileClass::CommonTextureFileClass() : pTexture(nullptr)
{
}

CommonTextureFileClass::CommonTextureFileClass(LPDIRECT3DDEVICE9 pDevice, const char * pFileName) : CommonTextureFileClass()
{
	LoadFromFile(pDevice, pFileName);
}

CommonTextureFileClass::~CommonTextureFileClass()
{
	RemoveTexture();
}

bool CommonTextureFileClass::LoadFromFile(LPDIRECT3DDEVICE9 pDevice, const char * pFileName)
{
	if (this->IsLoaded())
		return false;

	return SUCCEEDED(D3DXCreateTextureFromFile(pDevice, pFileName, &pTexture));
}

void CommonTextureFileClass::RemoveTexture()
{
	if (pTexture) {
		DrawObject::CommitIsotatedTexture(pTexture);
		pTexture = nullptr;
	}
}

int CommonTextureFileClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position)
{
	if (!pDevice || !this->IsLoaded())
		return 0;

	D3DSURFACE_DESC Desc;
	if (FAILED(pTexture->GetLevelDesc(0, &Desc)))
		return 0;

	float width = Desc.Width;
	float height = Desc.Height;
	
	float startingX = Position.x + height / sqrt(2.0) - width / 2.0 / sqrt(2.0);
	float startingY = Position.y + height / sqrt(2.0) + width / 2.0 / sqrt(2.0);

	float l = width / sqrt(2.0);
	float h = height * 2.0 / sqrt(3.0);

	TexturedVertex VertexBuffer[4];
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPVOID pVertexData;
	PaintingStruct Object;
	D3DXVECTOR3 HeightPosition(height / sqrt(2.0), height / sqrt(2.0), 0.0);

	VertexBuffer[0] = { { startingX,startingY,Position.z },0.0,1.0 };
	VertexBuffer[1] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
	VertexBuffer[2] = { { startingX,startingY,Position.z + h },0.0,0.0 };
	VertexBuffer[3] = { { startingX + l,startingY - l,Position.z + h },1.0,0.0 };

	if (FAILED(pDevice->CreateVertexBuffer(sizeof VertexBuffer, D3DUSAGE_DYNAMIC, TexturedVertex::dwFVFType,
		D3DPOOL_SYSTEMMEM, &pVertexBuffer, nullptr)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}

	if (FAILED(pVertexBuffer->Lock(0, 0, &pVertexData, D3DLOCK_DISCARD)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}

	memcpy_s(pVertexData, sizeof VertexBuffer, VertexBuffer, sizeof VertexBuffer);
	pVertexBuffer->Unlock();

	PaintingStruct::InitializePaintingStruct(Object, pVertexBuffer, Position /*+ HeightPosition*/, pTexture);
	Object.SetCompareOffset(HeightPosition);
	return this->CommitTransperantObject(Object);
}

bool CommonTextureFileClass::IsLoaded()
{
	return pTexture != nullptr;
}