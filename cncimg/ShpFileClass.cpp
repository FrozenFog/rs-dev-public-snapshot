#include "ShpFileClass.h"
#include "VertexFormats.h"
#include "ColorScheme.h"
#include "SceneClass.h"

std::unordered_map<int, std::unique_ptr<ShpFileClass>> ShpFileClass::FileObjectTable;

void ShpFileClass::ClearAllObjectForAllFile()
{
	for (auto& file : FileObjectTable) {
		if (file.second)
			file.second->ClearAllObjects();
	}
}

ShpFileClass::ShpFileClass() :DrawObject(), FileData(nullptr)
{
}

ShpFileClass::ShpFileClass(const char * pFileName) : ShpFileClass()
{
	this->LoadFromFile(pFileName);
}

ShpFileClass::ShpFileClass(LPVOID pFileBuffer, ULONG nFileSize, bool bCopy) : ShpFileClass()
{
	this->LoadFromFileInBuffer(pFileBuffer, nFileSize, bCopy);
}

ShpFileClass::~ShpFileClass()
{
	this->Clear();
}

void ShpFileClass::Clear()
{
	for (auto textures : this->FrameTextures) {
		for (auto texture : textures.second) {
			for (auto item : texture.second)
				//SAFE_RELEASE(item);
				CommitIsotatedTexture(item);
		}
	}
	this->FrameTextures.clear();

	if (this->IsLoaded())
		free(this->FileData);
	this->FileData = nullptr;
}

//bCopy should be true if it's called by api
bool ShpFileClass::LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nFileSize, bool bCopy)
{
	if (!bCopy)
		return (this->FileData = reinterpret_cast<ShpFile*>(pFileBuffer)) != nullptr;

	if (!pFileBuffer || !nFileSize)
		return false;

	this->FileData = reinterpret_cast<ShpFile*>(malloc(nFileSize));
	if (!this->FileData)
		return false;

	memcpy_s(this->FileData, nFileSize, pFileBuffer, nFileSize);
	return true;
}

bool ShpFileClass::LoadFromFile(const char * pFileName)
{
	auto hFile = CreateFile(pFileName, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr,
		OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return false;

	auto nFileSize = GetFileSize(hFile, nullptr);
	void* FileBuffer;
	ULONG nReadBytes;

	FileBuffer = malloc(nFileSize);
	if (!FileBuffer) {
		CloseHandle(hFile);
		return false;
	}

	if (!ReadFile(hFile, FileBuffer, nFileSize, &nReadBytes, nullptr) || nReadBytes != nFileSize) {
		CloseHandle(hFile);
		free(FileBuffer);
		return false;
	}
	
	CloseHandle(hFile);
	return this->LoadFromFileInBuffer(FileBuffer, nFileSize, false);
}

bool ShpFileClass::IsLoaded()
{
	return this->FileData != nullptr;
}

PBYTE ShpFileClass::GetFrameData(int idxFrame)
{
	if (auto header = this->GetFrameHeader(idxFrame)) {
		return reinterpret_cast<PBYTE>(this->FileData) + header->dwOffset;
	}
	return false;
}

int ShpFileClass::GetFrameCount()
{
	if (auto header = this->GetFileHeader()) {
		return header->nFrameCount;
	}
	return 0;
}

bool ShpFileClass::HasCompression(int idxFrame)
{
	if (auto header = this->GetFrameHeader(idxFrame)) {
		return header->dwFlags & 2;
	}
	return false;
}

RECT ShpFileClass::GetImageBounds()
{
	if (auto header = this->GetFileHeader()) {
		return RECT{ 0,0,header->nImageWidth,header->nImageHeight };
	}
	return EmptyRect;
}

RECT ShpFileClass::GetFrameBounds(int idxFrame)
{
	if (auto header = this->GetFrameHeader(idxFrame)) {
		return RECT{ header->X,header->Y,header->X + header->nWidth,header->Y + header->nHeight };
	}
	return EmptyRect;
}

ShpFrameHeader * ShpFileClass::GetFrameHeader(int idxFrame)
{
	if (!this->IsLoaded())
		return nullptr;

	if (idxFrame < 0 || idxFrame >= this->GetFrameCount())
		return nullptr;

	return &this->FileData->FrameHeaders[idxFrame];
}

ShpFileHeader * ShpFileClass::GetFileHeader()
{
	if (!this->IsLoaded())
		return nullptr;

	return &this->FileData->FileHeader;
}

void ShpFileClass::RemoveAllTextures(int nPaletteId, DWORD dwRemapColor)
{
	/*
	for (auto texture : this->FrameTextures) {
		texture->Release();
	}
	*/
	this->FrameTextures[nPaletteId][dwRemapColor].clear();
}

bool ShpFileClass::MakeTextures(LPDIRECT3DDEVICE9 pDevice, int nPaletteID, DWORD dwRemapColor)
{
	if (!this->IsLoaded() || !pDevice)
		return false;

	D3DLOCKED_RECT LockedRect;
	D3DCOLOR* pTextureData;
	PBYTE dwPointerBuffer;
	LPDIRECT3DTEXTURE9 pTexture;
	int nValidFrames, nNullFrames;

	for (auto item : this->FrameTextures[nPaletteID][dwRemapColor]) {
		DrawObject::CommitIsotatedTexture(item);
	}

	this->RemoveAllTextures(nPaletteID, dwRemapColor);
	this->FrameTextures[nPaletteID][dwRemapColor].resize(this->GetFrameCount());

	Palette NewPalette;
	if (auto palette = Palette::FindPaletteByID(nPaletteID)) {
		NewPalette = *palette;
	}
	else 
		return false;

	NewPalette.MakeRemapColor(dwRemapColor);

	ZeroMemory(this->FrameTextures[nPaletteID][dwRemapColor].data(), 
		this->FrameTextures[nPaletteID][dwRemapColor].size() * sizeof LPDIRECT3DTEXTURE9);

	nNullFrames = 0;
	for (int i = 0; i < this->GetFrameCount(); i++)
	{
		auto bCompressed = this->HasCompression(i);
		auto pData = this->GetFrameData(i);
		auto Bounds = this->GetFrameBounds(i);

		auto width = Bounds.right - Bounds.left;
		auto height = Bounds.bottom - Bounds.top;
		
		if (!pData)
			continue;

		if (!width || !height)
		{
			nNullFrames++;
			continue;
		}

		if (FAILED(pDevice->CreateTexture(width, height, 0, NULL, D3DFMT_A8R8G8B8,
			D3DPOOL_MANAGED, &pTexture, nullptr))) 
		{
			SAFE_RELEASE(pTexture);
			continue;
		}

		if (FAILED(pTexture->LockRect(0, &LockedRect, nullptr, NULL))) 
		{
			SAFE_RELEASE(pTexture);
			continue;
		}

		pTextureData = reinterpret_cast<D3DCOLOR*>(LockedRect.pBits);
		for (int i = 0; i < height; i++) 
		{
			ZeroMemory(reinterpret_cast<PBYTE>(LockedRect.pBits) + i*LockedRect.Pitch, LockedRect.Pitch);
		}

		if (bCompressed) 
		{
			for (int y = 0; y < height; y++) 
			{
				auto size = *reinterpret_cast<short*>(pData);
				auto source = pData + sizeof(short);
				auto dest = pTextureData;

				while (source < pData + size) 
				{
					if (auto ncolor = *source++) {
						auto color = NewPalette[ncolor];
						*dest++ = D3DCOLOR_XRGB(color.R, color.G, color.B);
					}
					else {
						dest += *source++;
					}
				}
				pData += size;
				dwPointerBuffer = reinterpret_cast<PBYTE>(pTextureData);
				dwPointerBuffer += LockedRect.Pitch;
				pTextureData = reinterpret_cast<D3DCOLOR*>(dwPointerBuffer);
			}
		}
		else 
		{
			for (int y = 0; y < height; y++)
			{
				auto source = pData;
				auto dest = pTextureData;
				for (int x = 0; x < width; x++)
				{
					if (auto ncolor = *source++)
					{
						auto color = NewPalette[ncolor];
						*dest = D3DCOLOR_XRGB(color.R, color.G, color.B);
					}
					dest++;
				}
				pData += width;
				dwPointerBuffer = reinterpret_cast<PBYTE>(pTextureData);
				dwPointerBuffer += LockedRect.Pitch;
				pTextureData = reinterpret_cast<D3DCOLOR*>(dwPointerBuffer);
			}
		}

		pTexture->UnlockRect(0);
		this->FrameTextures[nPaletteID][dwRemapColor][i] = pTexture;
	}

	nValidFrames = 0;
	for (auto item : this->FrameTextures[nPaletteID][dwRemapColor]) {
		if (item)
			nValidFrames++;
	}

	//D3DXSaveTextureToFile("destfile.png", D3DXIFF_PNG, this->FrameTextures[nPaletteID][dwRemapColor][0], nullptr);
	return (this->GetFrameCount() - nNullFrames) == nValidFrames;
}

int ShpFileClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position, int idxFrame, char bFlat, int nPaletteID, DWORD dwRemap,
	int nX, int nY, int nHeight, bool bDrawAsShadow)
{
	if (!pDevice || idxFrame < 0 || idxFrame >= this->GetFrameCount())
		return 0;

	if (this->FrameTextures.find(nPaletteID) == this->FrameTextures.end())
		return 0;

	if (this->FrameTextures[nPaletteID].find(dwRemap) == this->FrameTextures[nPaletteID].end())
		return 0;

	if (!this->FrameTextures[nPaletteID][dwRemap].size())
		return 0;

	auto pTexture = this->FrameTextures[nPaletteID][dwRemap][idxFrame];
	if (!pTexture)
		return 0;
	
	auto ImageBounds = this->GetImageBounds();
	auto FrameBounds = this->GetFrameBounds(idxFrame);

	float height = FrameBounds.bottom - FrameBounds.top;
	float width = FrameBounds.right - FrameBounds.left;
	float W = ImageBounds.right - ImageBounds.left;
	float H = ImageBounds.bottom - ImageBounds.top;

	float dx = FrameBounds.left - ImageBounds.right / 2.0;
	float dy = FrameBounds.bottom - ImageBounds.bottom / 2.0;

	float startingX = dy*sqrt(2.0) + dx / sqrt(2.0) + Position.x;
	float startingY = dy*sqrt(2.0) - dx / sqrt(2.0) + Position.y;
	float l = width / sqrt(2.0);

	const float cL = 30.0*sqrt(2.0);
	const float cH = 10.0*sqrt(3.0);

	//TexturedVertex VertexBuffer[6];
	std::vector<TexturedVertex> VertexBuffer;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPVOID pVertexData;
	PaintingStruct Object;
	D3DXVECTOR3 HeightPosition(dy*sqrt(2.0), dy*sqrt(2.0), 0.0);

	if(!bFlat)
	{
		float h = height*2.0f / sqrt(3.0);
		VertexBuffer.resize(6);
		VertexBuffer[0] = { {startingX,startingY,Position.z},0.0,1.0 };
		VertexBuffer[1] = { {startingX + l,startingY - l,Position.z},1.0,1.0 };
		VertexBuffer[2] = { {startingX,startingY,Position.z + h},0.0,0.0 };
		VertexBuffer[3]= { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[4] = { { startingX,startingY,Position.z + h },0.0,0.0 };
		VertexBuffer[5] = { {startingX + l,startingY - l,Position.z + h},1.0,0.0 };
	}
	else if (bFlat == 1)
	{
		float h = height*sqrt(2.0);
		VertexBuffer.resize(6);
		VertexBuffer[0] = { { startingX,startingY,Position.z },0.0,1.0 };
		VertexBuffer[1] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[2] = { { startingX - h,startingY - h,Position.z },0.0,0.0 };
		VertexBuffer[3] = { { startingX + l,startingY - l,Position.z },1.0,1.0 };
		VertexBuffer[4] = { { startingX - h,startingY - h,Position.z },0.0,0.0 };
		VertexBuffer[5] = { { startingX + l - h,startingY - l - h,Position.z },1.0,0.0 };
	}
	else if (bFlat == 2)
	{
		VertexBuffer.resize(18);
		float Ou = (W / 2.0f - FrameBounds.left) / width;
		float Ov = (H / 2.0f - FrameBounds.top) / height;

		VertexBuffer[0] = { {nX*cL,0.0f,nHeight*cH},30.0f*nX / width,15.0f*(nX - nHeight) / height };
		VertexBuffer[1] = { {nX*cL,0.0f,0.0f},30.0f*nX / width,15.0f*nX / height };
		VertexBuffer[2] = { {0.0f,0.0f,nHeight*cH},0.0f,-15.0f*nHeight / height };

		VertexBuffer[3] = { { nX*cL,0.0f,0.0f },30.0f*nX / width,15.0f*nX / height };
		VertexBuffer[4] = { { 0.0f,0.0f,nHeight*cH },0.0f,-15.0f*nHeight / height };
		VertexBuffer[5] = { {0.0f,0.0f,0.0f},0.0f,0.0f };

		VertexBuffer[6] = { { 0.0f,0.0f,nHeight*cH },0.0f,-15.0f*nHeight / height };
		VertexBuffer[7] = { { 0.0f,0.0f,0.0f },0.0f,0.0f };
		VertexBuffer[8] = { {0.0f,nY*cL,nHeight*cH},-30.0f*nY / width,15.0f*(nY - nHeight) / height };

		VertexBuffer[9] = { { 0.0f,0.0f,0.0f },0.0f,0.0f };
		VertexBuffer[10] = { { 0.0f,nY*cL,nHeight*cH },-30.0f*nY / width,15.0f*(nY - nHeight) / height };
		VertexBuffer[11] = { {0.0f,nY*cL,0.0f},-30.0f*nY / width,15.0f*nY / height };

		VertexBuffer[12] = { { 0.0f,nY*cL,0.0f },-30.0f*nY / width,15.0f*nY / height };
		VertexBuffer[13] = { { 0.0f,0.0f,0.0f },0.0f,0.0f };
		VertexBuffer[14] = { {nX*cL,nY*cL,0.0},30.0f*(nX - nY) / width,15.0f*(nX + nY) / height };

		VertexBuffer[15] = { { 0.0f,0.0f,0.0f },0.0f,0.0f };
		VertexBuffer[16] = { { nX*cL,nY*cL,0.0 },30.0f*(nX - nY) / width,15.0f*(nX + nY) / height };
		VertexBuffer[17] = { { nX*cL,0.0f,0.0f },30.0f*nX / width,15.0f*nX / height };

		for (auto &vertex : VertexBuffer)
		{
			vertex.Vector += Position;
			vertex.U += Ou;
			vertex.V += Ov;
		}
	}

	auto nBufferSize = VertexBuffer.size() * sizeof TexturedVertex;
	if (FAILED(pDevice->CreateVertexBuffer(nBufferSize, D3DUSAGE_DYNAMIC, TexturedVertex::dwFVFType,
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
	
	memcpy_s(pVertexData, nBufferSize, VertexBuffer.data(), nBufferSize);
	pVertexBuffer->Unlock();

	PaintingStruct::InitializePaintingStruct(Object, pVertexBuffer, Position /*+ HeightPosition*/, pTexture, bDrawAsShadow);

	if (VertexBuffer.size() == 6)
		Object.SetCompareOffset(HeightPosition);

	return this->CommitTransperantObject(Object);
}
