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

CommonTextureFileClass::CommonTextureFileClass(LPDIRECT3DDEVICE9 pDevice, float Radius, float Thickness, DWORD dwColor)
{
	CreateCircle(pDevice, Radius, Thickness, dwColor);
}

CommonTextureFileClass::CommonTextureFileClass(LPDIRECT3DDEVICE9 pDevice, const void* pColorBuffer, ULONG ulWidth, ULONG ulHeight)
{
	LoadARGB32TextureFromBuffer(pDevice, pColorBuffer, ulWidth, ulHeight);
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

bool CommonTextureFileClass::LoadARGB32TextureFromBuffer(LPDIRECT3DDEVICE9 pDevice, const void* pColorBuffer, ULONG ulWidth, ULONG ulHeight)
{
	if (this->IsLoaded() || !pColorBuffer)
		return false;

	D3DLOCKED_RECT LockedRect;
	HRESULT hResult = pDevice ? pDevice->CreateTexture(ulWidth, ulHeight, 1, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &this->pTexture, nullptr) : E_POINTER;
	if (FAILED(hResult)) {
		SAFE_RELEASE(this->pTexture);
		return false;
	}

	hResult = this->pTexture->LockRect(0, &LockedRect, nullptr, NULL);
	if (FAILED(hResult)) {
		this->pTexture->UnlockRect(0);
		SAFE_RELEASE(this->pTexture);
		return false;
	}

	ULONG ulDataSize = ulWidth * ulHeight * sizeof D3DCOLOR;
	memcpy_s(LockedRect.pBits, ulDataSize, pColorBuffer, ulDataSize);
	return SUCCEEDED(this->pTexture->UnlockRect(0));
}

bool CommonTextureFileClass::CreateCircle(LPDIRECT3DDEVICE9 pDevice, float Radius, float Thickness, DWORD dwColor)
{
	const float BorderSafeRange = 3.0f;

	auto GetCenterY = [Radius](float x)->float {
		return sqrt(Radius*Radius - x*x) / 2.0f;
	};
	
	if (!pDevice)
		return false;

	if (this->IsLoaded())
		return false;

	int TextureDimension = (Radius + BorderSafeRange + Thickness / 2.0f)*2.0f;
	
	if (FAILED(pDevice->CreateTexture(TextureDimension, TextureDimension, 1, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED,
		&this->pTexture, nullptr)))
		return false;

	D3DLOCKED_RECT LockedRect;
	if (FAILED(pTexture->LockRect(0, &LockedRect, nullptr, NULL)))
	{
		SAFE_RELEASE(pTexture);
		return false;
	}

	auto pTextureData = reinterpret_cast<PBYTE>(LockedRect.pBits);
	for (int i = 0; i < TextureDimension; i++)
	{
		ZeroMemory(pTextureData + LockedRect.Pitch*i, TextureDimension * sizeof D3DCOLOR);
	}

	auto GetTexturePixel = [&LockedRect](int x, int y)->PDWORD {
		auto pTextureData = reinterpret_cast<PBYTE>(LockedRect.pBits);
		auto pPixel = reinterpret_cast<PDWORD>(pTextureData + LockedRect.Pitch*y);
		return pPixel + x;
	};

	auto FillThicknessRectWithColor = [&](int x, int y)->void {
		int startX = x - Thickness / sqrt(2.0);
		int startY = y - Thickness / sqrt(2.0);

		for (int y = 0; y < static_cast<int>(Thickness); y++)
		{
			auto pPixel = GetTexturePixel(startX, y + startY);
			for (int x = 0; x < static_cast<int>(Thickness); x++)
				pPixel[x] = dwColor;
		}
	};

	for (int x = 0; x <= static_cast<int>(2.0f*Radius); x++)
	{
		int y = GetCenterY(x - Radius);
		int nexty = GetCenterY(x + (x < static_cast<int>(2.0f*Radius) ? 1 : -1) - Radius);

		FillThicknessRectWithColor(x + BorderSafeRange + Thickness / 2.0f, Radius + BorderSafeRange + Thickness / 2.0f + y);
		FillThicknessRectWithColor(x + BorderSafeRange + Thickness / 2.0f, Radius + BorderSafeRange + Thickness / 2.0f - y);

		if (std::abs(y - nexty) >= Thickness)
		{
			int deltay = nexty > y ? Thickness : -Thickness;
			do
			{
				y += deltay / 2.0f;
				FillThicknessRectWithColor(x + BorderSafeRange + Thickness / 2.0f, Radius + BorderSafeRange + Thickness / 2.0f + y);
				FillThicknessRectWithColor(x + BorderSafeRange + Thickness / 2.0f, Radius + BorderSafeRange + Thickness / 2.0f - y);
			} while (std::abs(y - nexty) >= Thickness);
		}
	}

	pTexture->UnlockRect(0);
	return true;
}

void CommonTextureFileClass::RemoveTexture()
{
	if (pTexture) {
		DrawObject::CommitIsotatedTexture(pTexture);
		pTexture = nullptr;
	}
}

int CommonTextureFileClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position,bool bFlat)
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

	TexturedVertex VertexBuffer[6];
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPVOID pVertexData;
	PaintingStruct Object;
	D3DXVECTOR3 HeightPosition(height / sqrt(2.0), height / sqrt(2.0), 0.0);

	if (!bFlat)
	{
		VertexBuffer[0] = { { startingX,startingY,Position.z },0.0,1.0 };
		VertexBuffer[1] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[2] = { { startingX,startingY,Position.z + h },0.0,0.0 };

		VertexBuffer[3] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[4] = { { startingX,startingY,Position.z + h },0.0,0.0 };
		VertexBuffer[5] = { { startingX + l,startingY - l,Position.z + h },1.0,0.0 };
	}
	else
	{
		VertexBuffer[0] = { { startingX,startingY,Position.z },0.0,1.0 };
		VertexBuffer[1] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[2] = { { startingX - h,startingY - h,Position.z },0.0,0.0 };

		VertexBuffer[3] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[4] = { { startingX - h,startingY - h,Position.z },0.0,0.0 };
		VertexBuffer[5] = { { startingX + l - h,startingY - l - h,Position.z },1.0,0.0 };
	}

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

void DrawObject::RemoveCommonTextureObject(int nID)
{
	for (auto& file : CommonTextureFileClass::FileObjectTable) {
		if (!file.second)
			continue;
		//try find and erase
		file.second->RemoveTransperantObject(nID);
	}
}
