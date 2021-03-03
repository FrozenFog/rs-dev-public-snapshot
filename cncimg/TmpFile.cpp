#include "VertexFormats.h"
#include "TmpFile.h"

#include <stdio.h>
#include <algorithm>

#include <d3dx9.h>

//const RECT EmptyRect = { 0,0,0,0 };
std::unordered_map<int, std::unique_ptr<TmpFileClass>> TmpFileClass::FileObjectTable;

//clear all object on scene
void TmpFileClass::ClearAllObjectForAllFile()
{
	for (auto& file : FileObjectTable) {
		if (file.second)
			file.second->ClearAllObjects();
	}
}

TmpFileClass::TmpFileClass() : DrawObject(), pFileData(nullptr)
{
}

TmpFileClass::TmpFileClass(const char * pFileName) : TmpFileClass()
{
	this->LoadFromFile(pFileName);
}

TmpFileClass::TmpFileClass(LPVOID pFileBuffer, ULONG nSize, bool bCopy)
{
	this->LoadFromFileInBuffer(pFileBuffer, nSize, bCopy);
}

TmpFileClass::~TmpFileClass()
{
	this->Clear();
}

void TmpFileClass::Clear()
{
	for (auto& Pair : this->CellTextures)
		CommitIsotatedTexture(Pair.second);
	this->CellTextures.clear();

	for (auto& Pair : this->ExtraTextures)
		CommitIsotatedTexture(Pair.second);
	this->ExtraTextures.clear();

	for (auto& Pair : this->ExtraZTextures)
		CommitIsotatedTexture(Pair.second);
	this->ExtraZTextures.clear();

	for (auto& Pair : this->CellZTextures)
		CommitIsotatedTexture(Pair.second);
	this->CellZTextures.clear();

	if (pFileData)
		free(pFileData);
	pFileData = nullptr;
}

bool TmpFileClass::IsLoaded()
{
	return this->pFileData != nullptr;
}

void TmpFileClass::LoadFromFile(const char * pFileName)
{
	HANDLE hFile;
	TmpFile* pFileData;
	ULONG uOut;
	DWORD dwFileSize;

	this->Clear();
	//read the whole file
	hFile = CreateFile(pFileName, FILE_READ_ACCESS, FILE_SHARE_READ | FILE_SHARE_WRITE,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	dwFileSize = GetFileSize(hFile, &uOut);
	pFileData = reinterpret_cast<TmpFile*>(malloc(dwFileSize));
	if (!pFileData)
		goto EndProc;

	if (!ReadFile(hFile, pFileData, dwFileSize, &uOut, NULL) || uOut != dwFileSize)
		goto EndProc;

	this->LoadFromFileInBuffer(pFileData, dwFileSize, false);
EndProc:
	CloseHandle(hFile);
}

void TmpFileClass::LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, bool bCopy)
{
	TmpFile* pFileData;

#ifdef _WIN64
	ULONGLONG dwPointerBuffer;
#else
	PBYTE dwPointerBuffer;
#endif

	int nBlocks;

	if (bCopy)
	{
		pFileData = reinterpret_cast<TmpFile*>(malloc(nSize));
		memcpy_s(pFileData, nSize, pFileBuffer, nSize);
	}
	else
	{
		pFileData = reinterpret_cast<TmpFile*>(pFileBuffer);
	}
	//count block counts
	nBlocks = pFileData->Header.nXBlocks*pFileData->Header.nYBlocks;
	//correct address

#ifdef _WIN64
	auto ImageHeaders = new DWORD[nBlocks];
	RtlCopyMemory(ImageHeaders, pFileData->ImageHeaders, nBlocks * sizeof DWORD);
#endif

	for (int i = 0; i < nBlocks; i++)
	{
#ifdef _WIN64
		dwPointerBuffer = ImageHeaders[i];
		if (dwPointerBuffer)
		{
			dwPointerBuffer += reinterpret_cast<ULONGLONG>(pFileData);
			pFileData->ImageHeaders[i] = reinterpret_cast<TmpImageHeader*>(dwPointerBuffer);
		}
#else
		//PointerBuffer is an RVA or a correct address
		dwPointerBuffer = reinterpret_cast<decltype(dwPointerBuffer)>(pFileData->ImageHeaders[i]);
		//the PointerBuffer is a valid offset
		if (dwPointerBuffer)
		{
			//check if the PointerBuffer is an RVA rather than a correct address
			if (dwPointerBuffer<reinterpret_cast<decltype(dwPointerBuffer)>(pFileData))
				//if it's an RVA ,add an ImageBase to it to make it a correct address;
				dwPointerBuffer += reinterpret_cast<ULONGLONG>(pFileData);
			pFileData->ImageHeaders[i] = reinterpret_cast<TmpImageHeader*>(dwPointerBuffer);
		}
#endif
	}
	//set it to the class
#ifdef _WIN64
	delete[] ImageHeaders;
#endif
	this->pFileData = pFileData;
}

TmpFile * TmpFileClass::GetFileData()
{
	return this->pFileData;
}

void TmpFileClass::GetWholeRect(RECT & Rectangle)
{
	Rectangle = EmptyRect;

	RECT CurrentRect, CurrentExtraRect;
	for (int i = 0; i < this->GetMaxBlockCount(); i++)
	{
		if (this->GetBlockRect(i, CurrentRect), CurrentRect != EmptyRect)
		{
			//first init
			if (Rectangle == EmptyRect)
			{
				Rectangle = CurrentRect;
			}
			else
			{
				UnionRect(&Rectangle, &Rectangle, &CurrentRect);
			}
		}
		if (this->GetExtraBlockRect(i, CurrentExtraRect), CurrentExtraRect != EmptyRect)
		{
			UnionRect(&Rectangle, &Rectangle, &CurrentExtraRect);
		}
	}
}

TmpImageHeader * TmpFileClass::GetImageHeader(int nIndex)
{
	return this->pFileData && nIndex >= 0 && nIndex < this->GetMaxBlockCount() ? 
		this->pFileData->ImageHeaders[nIndex] : nullptr;
}

int TmpFileClass::GetMaxBlockCount()
{
	return this->pFileData ? this->pFileData->Header.nXBlocks*this->pFileData->Header.nYBlocks : 0;
}

int TmpFileClass::GetValidBlockCount()
{
	ULONG nResult = 0;
	for (int i = 0; i < this->GetMaxBlockCount(); i++)
		if (this->GetImageHeader(i))
			nResult++;

	return nResult;
}

bool TmpFileClass::HasExtraData(int nIndex)
{
	auto pImageHeader = this->GetImageHeader(nIndex);
	return pImageHeader ? pImageHeader->dwExtraFlag & 1 : false;
}

RampType TmpFileClass::GetRampType(int nIndex)
{
	return this->GetImageHeader(nIndex) ? this->GetImageHeader(nIndex)->RampType : RampType::Plane;
}

PBYTE TmpFileClass::GetPixelData(int nIndex)
{
	return this->GetImageHeader(nIndex) ? this->GetImageHeader(nIndex)->PixelData : nullptr;
}

PBYTE TmpFileClass::GetExtraPixelData(int nIndex)
{
	auto pPixels = this->GetPixelData(nIndex);
	//2*width*height/2
	return pPixels ? &pPixels[pFileData->Header.nBlocksHeight*pFileData->Header.nBlocksWidth] : nullptr;
}

PBYTE TmpFileClass::GetZShapeData(int nIndex)
{
	auto pPixels = this->GetPixelData(nIndex);
	//1*width*height/2
	return pPixels ? &pPixels[pFileData->Header.nBlocksHeight*pFileData->Header.nBlocksWidth / 2] : nullptr;
}

PBYTE TmpFileClass::GetExtraZShapeData(int nIndex)
{
	auto pPixels = this->GetPixelData(nIndex);
	auto pImageHeader = this->GetImageHeader(nIndex);
	//2*width*height/2 + ExtraWidth*ExtraHeight
	return pPixels && this->HasExtraData(nIndex) ?
		&pPixels[pFileData->Header.nBlocksHeight*pFileData->Header.nBlocksWidth + pImageHeader->nExtraHeight*pImageHeader->nExtraWidth] : nullptr;
}

void TmpFileClass::GetBlockRect(int nIndex, RECT & Rectangle)
{
	auto pImageHeader = this->GetImageHeader(nIndex);
	if (pImageHeader)
		Rectangle = {
		pImageHeader->X - pFileData->Header.nBlocksWidth / 2,
		pImageHeader->Y - pImageHeader->nHeight * pFileData->Header.nBlocksHeight / 2,
		pImageHeader->X + pFileData->Header.nBlocksWidth / 2,
		pImageHeader->Y - pImageHeader->nHeight * pFileData->Header.nBlocksHeight / 2 + pFileData->Header.nBlocksHeight
	};
	else
		Rectangle = EmptyRect;
}

void TmpFileClass::GetBlockSizeRect(int nIndex, RECT & Rectangle)
{
	Rectangle = EmptyRect;
	if (this->GetBlockRect(nIndex, Rectangle), !!Rectangle)
	{
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
	}
}

void TmpFileClass::GetExtraBlockRect(int nIndex, RECT & Rectangle)
{
	auto pImageHeader = this->GetImageHeader(nIndex);
	RECT ImageRect;
	if (pImageHeader && this->HasExtraData(nIndex))
	{
		if (this->GetBlockRect(nIndex, ImageRect), !!ImageRect)
		{
			Rectangle = {
				ImageRect.left - pImageHeader->X + pImageHeader->nExtraX,
				ImageRect.top - pImageHeader->Y + pImageHeader->nExtraY,
				ImageRect.left - pImageHeader->X + pImageHeader->nExtraX + pImageHeader->nExtraWidth,
				ImageRect.top - pImageHeader->Y + pImageHeader->nExtraY + pImageHeader->nExtraHeight
			};
			return;
		}
	}
	Rectangle = EmptyRect;
}

void TmpFileClass::GetExtraSizeRect(int nIndex, RECT & Rectangle)
{
	Rectangle = EmptyRect;
	if (this->GetExtraBlockRect(nIndex, Rectangle), !!Rectangle)
	{
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
	}
}

void TmpFileClass::GetWholeSizeRect(RECT & Rectangle)
{
	if (this->GetWholeRect(Rectangle), !!Rectangle)
	{
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
	}
}

void TmpFileClass::GetBlockRectWithHeight(int nIndex, RECT & Rectangle)
{
	RECT BaseRect;
	if (this->GetBlockRect(nIndex, Rectangle), !!Rectangle)
	{
		auto pImageHeader = this->GetImageHeader(nIndex);
		BaseRect = Rectangle;
		BaseRect.top += pImageHeader->nHeight*pFileData->Header.nBlocksHeight / 2;
		BaseRect.bottom += pImageHeader->nHeight*pFileData->Header.nBlocksHeight / 2;
		UnionRect(&Rectangle, &Rectangle, &BaseRect);
	}
}

void TmpFileClass::GetWholeRectWithHeight(RECT & Rectangle)
{
	Rectangle = EmptyRect;

	RECT CurrentRect, CurrentExtraRect;
	for (int i = 0; i < this->GetMaxBlockCount(); i++)
	{
		if (this->GetBlockRectWithHeight(i, CurrentRect), !!CurrentRect)
		{
			//first init
			if (Rectangle == EmptyRect)
			{
				Rectangle = CurrentRect;
			}
			else
			{
				UnionRect(&Rectangle, &Rectangle, &CurrentRect);
			}
		}
		if (this->GetExtraBlockRect(i, CurrentExtraRect), !!CurrentExtraRect)
		{
			UnionRect(&Rectangle, &Rectangle, &CurrentExtraRect);
		}
	}
}

void TmpFileClass::GetWholeSizeRectWithHeight(RECT & Rectangle)
{
	if (this->GetWholeRectWithHeight(Rectangle), !!Rectangle)
	{
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
	}
}

void TmpFileClass::GetCellRect(int nIndex, RECT & Rectangle)
{
	RECT ExtraRect;
	if (this->GetBlockRect(nIndex, Rectangle), !!Rectangle)
	{
		if (this->GetExtraBlockRect(nIndex, ExtraRect), !!ExtraRect)
			UnionRect(&Rectangle, &Rectangle, &ExtraRect);
	}
}

void TmpFileClass::GetCellSizeRect(int nIndex, RECT & Rectangle)
{
	if (this->GetCellRect(nIndex, Rectangle), !!Rectangle)
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
}

void TmpFileClass::GetCellRectWithHeight(int nIndex, RECT & Rectangle)
{
	RECT ExtraRect;
	if (this->GetBlockRectWithHeight(nIndex, Rectangle), !!Rectangle)
	{
		if (this->GetExtraBlockRect(nIndex, ExtraRect), !!ExtraRect)
			UnionRect(&Rectangle, &Rectangle, &ExtraRect);
	}
}

void TmpFileClass::GetCellSizeRectWithHeight(int nIndex, RECT & Rectangle)
{
	if (this->GetCellRectWithHeight(nIndex, Rectangle), !!Rectangle)
		OffsetRect(&Rectangle, -Rectangle.left, -Rectangle.top);
}

bool TmpFileClass::MakeTextures(LPDIRECT3DDEVICE9 pDevice)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	RECT SizeRect, ExtraSizeRect;
	LPDIRECT3DTEXTURE9 pTexture, pZTexture, pExtraTexture, pExtraZTexture;
	D3DLOCKED_RECT LockedRect, LockedRectZ;
	PBYTE pData, pFileData, pDataZ, pFileDataZ;

	for (auto item : this->CellTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	for (auto item : this->ExtraTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	for (auto item : this->ExtraZTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	for (auto item : this->CellZTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	this->RemoveAllTextures();

	for (int i = 0; i < this->GetMaxBlockCount(); i++)
	{
		if (this->GetCellSizeRect(i, SizeRect), !SizeRect)
			continue;

		if (FAILED(pDevice->CreateTexture(this->GetFileData()->Header.nBlocksWidth, this->GetFileData()->Header.nBlocksHeight, 1, NULL,
			D3DFMT_L8, D3DPOOL_MANAGED, &pTexture, nullptr))||
			FAILED(pDevice->CreateTexture(this->GetFileData()->Header.nBlocksWidth, this->GetFileData()->Header.nBlocksHeight, 1, NULL,
				D3DFMT_L8, D3DPOOL_MANAGED, &pZTexture, nullptr)))
		{
			SAFE_RELEASE(pTexture);
			SAFE_RELEASE(pZTexture);
			continue;
		}

		if (FAILED(pTexture->LockRect(0, &LockedRect, &SizeRect, NULL))||
			FAILED(pZTexture->LockRect(0, &LockedRectZ, &SizeRect, NULL)))
		{
			SAFE_RELEASE(pTexture);
			SAFE_RELEASE(pZTexture);
			continue;
		}

		pData = reinterpret_cast<PBYTE>(LockedRect.pBits);
		pDataZ = reinterpret_cast<PBYTE>(LockedRectZ.pBits);
		pFileData = this->GetPixelData(i);
		pFileDataZ = this->GetZShapeData(i);
		
		int y = 0;
		for (int x = this->GetFileData()->Header.nBlocksWidth / 2 - 2; x > 0; x -= 2, y++)
		{
			auto pTextureData = pData + y*LockedRect.Pitch;// +x * sizeof(D3DCOLOR_ARGB(0, 0, 0, 0));
			auto pTextureDataZ = pDataZ + y * LockedRectZ.Pitch;
			auto nSize = this->GetFileData()->Header.nBlocksWidth - 2 * x;
			bool bFirstEnter = true;

			RtlZeroMemory(pTextureData, this->GetFileData()->Header.nBlocksWidth);
			pTextureData += x;
			RtlZeroMemory(pTextureDataZ, this->GetFileData()->Header.nBlocksWidth);
			pTextureDataZ += x;

			for (int i = 0; i < nSize; i++)
			{
				auto pColorData = reinterpret_cast<PBYTE>(pTextureData);
				auto pZData = reinterpret_cast<PBYTE>(pTextureDataZ);
				auto nColor = *pFileData++;
				auto nZVal = *pFileDataZ++;

				if (nColor) {
					pColorData[0] = nColor;
					pZData[0] = nZVal;
					if (bFirstEnter && x + i >= 1) {
						pColorData[-1] = nColor;
						pZData[-1] = nZVal;
						if (x + i >= 2)
						{
							pColorData[-2] = nColor;
							pZData[-2] = nZVal;
						}

						bFirstEnter = false;
					}
					if (!bFirstEnter && x + i <= this->GetFileData()->Header.nBlocksWidth - 1 && (*pFileData == 0 || i == nSize - 1)) {
						pColorData[1] = nColor;
						pZData[1] = nZVal;
						if (x + i <= this->GetFileData()->Header.nBlocksWidth - 2)
						{
							pColorData[2] = nColor;
							pZData[2] = nZVal;
						}

						bFirstEnter = true;
					}
				}
				pTextureData++;
				pTextureDataZ++;
			}
		}

		for (int x = 0; x <= this->GetFileData()->Header.nBlocksWidth / 2; x += 2, y++)
		{
			auto pTextureData = pData + y*LockedRect.Pitch;
			auto pTextureDataZ = pDataZ + y * LockedRectZ.Pitch;
			auto nSize = this->GetFileData()->Header.nBlocksWidth - 2 * x;
			bool bFirstEnter;

			bFirstEnter = true;
			RtlZeroMemory(pTextureData, this->GetFileData()->Header.nBlocksWidth);
			pTextureData += x;
			RtlZeroMemory(pTextureDataZ, this->GetFileData()->Header.nBlocksWidth);
			pTextureDataZ += x;

			if (!nSize)
			{
				auto pColorData = reinterpret_cast<PBYTE>(pTextureData);
				auto pColorDataZ = reinterpret_cast<PBYTE>(pTextureDataZ);
				BYTE nColor = 0, nZVal = 0;
				int i = 0;
				while (!nColor)
				{
					i++;
					nColor = *(pFileData - i);
					nZVal = *(pFileDataZ - i);
				}

				pColorData[-2] = pColorData[-1] = pColorData[0] = pColorData[1] = nColor;
				pColorDataZ[-2] = pColorDataZ[-1] = pColorDataZ[0] = pColorDataZ[1] = nZVal;
			}
			for (int i = 0; i < nSize; i++)
			{
				auto pColorData = reinterpret_cast<PBYTE>(pTextureData);
				auto pColorDataZ = reinterpret_cast<PBYTE>(pTextureDataZ);
				auto nColor = *pFileData++;
				auto nZVal = *pFileDataZ++;

				if (nColor) {
					pColorData[0] = nColor;
					pColorDataZ[0] = nZVal;
					if (bFirstEnter && x + i >= 1) {
						pColorData[-1] = nColor;
						pColorDataZ[-1] = nZVal;
						if (x + i >= 2)
						{
							pColorData[-2] = nColor;
							pColorDataZ[-2] = nZVal;
						}

						bFirstEnter = false;
					}
					if (!bFirstEnter && x + i <= this->GetFileData()->Header.nBlocksWidth - 1 && (*pFileData == 0 || i == nSize - 1)) {
						pColorData[1] = nColor;
						pColorDataZ[1] = nZVal;
						if (x + i <= this->GetFileData()->Header.nBlocksWidth - 2)
						{
							pColorData[2] = nColor;
							pColorDataZ[2] = nZVal;
						}
						bFirstEnter = true;
					}
				}
				pTextureData++;
				pTextureDataZ++;
			}
		}

		pTexture->UnlockRect(0);
		pZTexture->UnlockRect(0);
		this->AddTexture(i, pTexture);
		this->AddZTexture(i, pZTexture);
/*
		char szFileName[MAX_PATH];
		sprintf_s(szFileName, "dump\\Otile_%p_%d.png", this, i);
		D3DXSaveTextureToFile(szFileName, D3DXIFF_PNG, pTexture, nullptr);*/

		if (!this->HasExtraData(i))
			continue;

		if (this->GetExtraSizeRect(i, ExtraSizeRect), !ExtraSizeRect)
			continue;

		if (FAILED(pDevice->CreateTexture(ExtraSizeRect.right, ExtraSizeRect.bottom, 0, NULL, 
			D3DFMT_L8, D3DPOOL_MANAGED, &pExtraTexture, nullptr)))
		{
			SAFE_RELEASE(pExtraTexture);
			continue;
		}

		
		if (FAILED(pExtraTexture->LockRect(0, &LockedRect, &ExtraSizeRect, NULL)))
		{
			SAFE_RELEASE(pExtraTexture);
			continue;
		}

		pData = reinterpret_cast<PBYTE>(LockedRect.pBits);
		pFileData = this->GetExtraPixelData(i);
		for (int nLine = 0; nLine < ExtraSizeRect.bottom; nLine++)
		{
			RtlZeroMemory(pData, ExtraSizeRect.right);
			auto pColorData = reinterpret_cast<PBYTE>(pData);
			bool bEnter;

			bEnter = true;
			for (int i = 0; i < ExtraSizeRect.right; i++)
			{
				if (auto nColor = *pFileData++)
				{
					pColorData[i] = nColor;
					if (bEnter) {
						if (i >= 1)
							pColorData[i - 1] = nColor;
						if (i >= 2)
							pColorData[i - 2] = nColor;
						bEnter = false;
					}
					if (!bEnter && *pFileData == 0 && i < ExtraSizeRect.right - 1) {
						pColorData[i + 1] = nColor;
						if (i < ExtraSizeRect.right - 2)
							pColorData[i + 2] = nColor;
						bEnter = true;
					}
				}
			}
			pData += LockedRect.Pitch;
		}

		pExtraTexture->UnlockRect(0);
		this->AddExtraTexture(i, pExtraTexture);

		if (FAILED(pDevice->CreateTexture(ExtraSizeRect.right, ExtraSizeRect.bottom, 0, NULL,
			D3DFMT_L8, D3DPOOL_MANAGED, &pExtraZTexture, nullptr)))
		{
			SAFE_RELEASE(pExtraZTexture);
			continue;
		}

		if (FAILED(pExtraZTexture->LockRect(0, &LockedRect, &ExtraSizeRect, NULL)))
		{
			SAFE_RELEASE(pExtraZTexture);
			continue;
		}

		pData = reinterpret_cast<PBYTE>(LockedRect.pBits);
		pFileData = this->GetExtraZShapeData(i);
		for (int nLine = 0; nLine < ExtraSizeRect.bottom; nLine++)
		{
			RtlZeroMemory(pData, ExtraSizeRect.right);
			auto pColorData = reinterpret_cast<PBYTE>(pData);
			bool bEnter;

			bEnter = true;
			for (int i = 0; i < ExtraSizeRect.right; i++)
			{
				if (auto nColor = *pFileData++)
				{
					pColorData[i] = nColor;
					if (bEnter) {
						if (i >= 1)
							pColorData[i - 1] = nColor;
						if (i >= 2)
							pColorData[i - 2] = nColor;
						bEnter = false;
					}
					if (!bEnter && *pFileData == 0 && i < ExtraSizeRect.right - 1) {
						pColorData[i + 1] = nColor;
						if (i < ExtraSizeRect.right - 2)
							pColorData[i + 2] = nColor;
						bEnter = true;
					}
				}
			}
			pData += LockedRect.Pitch;
		}

		pExtraZTexture->UnlockRect(0);
		this->AddExtraZTexture(i, pExtraZTexture);
/*
		sprintf_s(szFileName, "dump\\tile_%p_%d.png", this, i);
		D3DXSaveTextureToFile(szFileName, D3DXIFF_PNG, pExtraTexture, nullptr);*/
	}

	return this->CellTextures.size() == this->GetValidBlockCount();
}

bool TmpFileClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position, int nPaletteID, int nTileIndex, int &OutTileIndex, int&OutExtraIndex)
{
	OutTileIndex = OutExtraIndex = 0;

	if (!pDevice || !this->IsLoaded()) {
		Logger::WriteLine(__FUNCTION__" : ""not loaded.\n");
		return false;
	}

	//this->RemoveFromScene(Position);

	float PixelCellLength = this->GetFileData()->Header.nBlocksWidth * sqrtf(0.5f);
	float PixelCellVisualHeight = this->GetFileData()->Header.nBlocksHeight;
	float PixelCellVisualWidth = this->GetFileData()->Header.nBlocksWidth;
	const float StretchAdjustment = 0.0f;

	PaintingStruct PaintObject;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, pExtraVertexBuffer;
	LPDIRECT3DTEXTURE9 pTexture, pZTexture,pExtraTexture, pExtraZTexture;
	RECT CoordsRect, CellRect, ExtraRect, CellSizeRect, PureCellRect;
	LPVOID pVertexData;
	D3DXVECTOR3 HeightPoint(PixelCellVisualHeight / sqrt(2.0), PixelCellVisualHeight / sqrt(2.0), 0.0);

	pExtraZTexture = pZTexture = pTexture = pExtraTexture = nullptr;
	pVertexBuffer = pExtraVertexBuffer = nullptr;

	pTexture = this->FindCellTexture(nTileIndex);
	pZTexture = this->FindZTexture(nTileIndex);
	if (!pTexture || !pZTexture)
		return false;

	auto palette = Palette::FindPaletteByID(nPaletteID);
	if (!palette)
		return false;

	this->GetWholeRectWithHeight(CoordsRect);
	this->GetBlockRectWithHeight(nTileIndex, CellRect);
	this->GetBlockSizeRect(nTileIndex, CellSizeRect);
	this->GetBlockRect(nTileIndex, PureCellRect);

	float CellStartX = Position.x + PixelCellLength / 2.0;
	float CellStartY = Position.y + PixelCellLength / 2.0;
	float dh = PixelCellVisualHeight * sqrtf(0.1875f);//sqrt(3)/4
	float dL = PixelCellLength * 0.5f;
	float dH = PixelCellVisualHeight / sqrtf(32.0f);

	TexturedVertex CellVertecies[] =
	{/*
		{ {CellStartX - StretchAdjustment,CellStartY - StretchAdjustment,Position.z},0.50f,0.00f },
		{ {CellStartX + PixelCellLength + StretchAdjustment,CellStartY - StretchAdjustment,Position.z},1.0f,0.50f },
		{ {CellStartX - StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z},0.0f,0.5f },
		{ { CellStartX + PixelCellLength + StretchAdjustment,CellStartY - StretchAdjustment,Position.z },1.0f,0.50f },
		{ { CellStartX - StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z },0.0f,0.5f },
		{ {CellStartX + PixelCellLength + StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z},0.50f,1.0f },*/
		{{CellStartX - 2.0f * dH,CellStartY - 2.0f * dH,Position.z + 2.0f * dh},0.5f,0.0f},
		{{CellStartX + dL - dH,CellStartY - dL - dH,Position.z + dh},1.0f,0.5f},
		{{CellStartX - dL - dH,CellStartY + dL - dH,Position.z + dh},0.0f,0.5f},
		{{CellStartX + dL - dH,CellStartY - dL - dH,Position.z + dh},1.0f,0.5f},
		{{CellStartX - dL - dH,CellStartY + dL - dH,Position.z + dh},0.0f,0.5f},
		{{CellStartX,CellStartY,Position.z},0.5f,1.0f},
	};

	if (FAILED(pDevice->CreateVertexBuffer(sizeof CellVertecies, D3DUSAGE_DYNAMIC, TexturedVertex::dwFVFType,
		D3DPOOL_SYSTEMMEM, &pVertexBuffer, nullptr)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return false;
	}

	if (FAILED(pVertexBuffer->Lock(0, 0, &pVertexData, D3DLOCK_DISCARD)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return false;
	}

	memcpy_s(pVertexData, sizeof CellVertecies, CellVertecies, sizeof CellVertecies);
	pVertexBuffer->Unlock();

	if (this->HasExtraData(nTileIndex))
	{
		this->GetExtraBlockRect(nTileIndex, ExtraRect);
		pExtraTexture = this->FindExtraTexture(nTileIndex);
		pExtraZTexture = this->FindExtraZTexture(nTileIndex);

		if (!pExtraTexture || !pExtraZTexture)
			goto DrawScene;

		float dx = CellRect.left - ExtraRect.left + PixelCellVisualWidth / 2.0;
		float dy = CellRect.top - ExtraRect.top + PixelCellVisualHeight / 2.0;
		TexturedVertex ExtraVertecies[6];

		if (this->GetRampType(nTileIndex) != RampType::Plane)
		{
			float h = (ExtraRect.bottom - ExtraRect.top)*sqrt(2.0);// / sqrt(3.0);
			float l = (ExtraRect.right - ExtraRect.left) / sqrt(2.0);

			float ExtraStartX = Position.x - dx / sqrt(2.0) - dy*sqrt(2.0);
			float ExtraStartY = Position.y + dx / sqrt(2.0) - dy*sqrt(2.0);
			float ExtraStartZ = Position.z;

			ExtraVertecies[0] = { { ExtraStartX - StretchAdjustment,ExtraStartY,ExtraStartZ },0.0,0.0 };
			ExtraVertecies[1] = { { ExtraStartX + l, ExtraStartY - l - StretchAdjustment, ExtraStartZ }, 1.0, 0.0 };
			ExtraVertecies[2] = { { ExtraStartX + h,ExtraStartY + h + StretchAdjustment,ExtraStartZ },0.0,1.0 };
			ExtraVertecies[3] = { { ExtraStartX + l, ExtraStartY - l - StretchAdjustment, ExtraStartZ }, 1.0, 0.0 };
			ExtraVertecies[4] = { { ExtraStartX + h,ExtraStartY + h + StretchAdjustment,ExtraStartZ },0.0,1.0 };
			ExtraVertecies[5] = { { ExtraStartX + l + h + StretchAdjustment,ExtraStartY - l + h,ExtraStartZ },1.0,1.0 };
		}
		else
		{
			//float h = (ExtraRect.bottom - ExtraRect.top)*2.0 / sqrt(3.0);
			//float l = (ExtraRect.right - ExtraRect.left) / sqrt(2.0);
			float dy2 = PureCellRect.bottom - ExtraRect.bottom - 1;// -PixelCellVisualHeight / 2.0f;
			float dx2 = PureCellRect.left - ExtraRect.left - (ExtraRect.right - ExtraRect.left) * 0.5f +PixelCellVisualWidth / 2.0f;
			float dw = (ExtraRect.right - ExtraRect.left) * sqrtf(0.125f);
			float dh = (ExtraRect.bottom - ExtraRect.top) * sqrtf(0.125f);
			float dz = sqrtf(0.75f) * (ExtraRect.bottom - ExtraRect.top + 2);

			float ExtraStartX = Position.x + dL - dx2 * sqrtf(0.5f) - dy2 * sqrtf(0.125f);// -dy2 * sqrtf(2.0f);
			float ExtraStartY = Position.y + dL + dx2 * sqrtf(0.5f) - dy2 * sqrtf(0.125f);// -dy2 * sqrtf(2.0f);
			float ExtraStartZ = Position.z + dy2 * sqrtf(0.75f);

			//ExtraVertecies[0] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ + 1 },0.0,0.0 };
			//ExtraVertecies[1] = { { ExtraStartX + l + StretchAdjustment, ExtraStartY - l - StretchAdjustment, ExtraStartZ + 1 }, 1.0, 0.0 };
			//ExtraVertecies[2] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ - h - 1 },0.0,1.0 };
			//ExtraVertecies[3] = { { ExtraStartX + l + StretchAdjustment, ExtraStartY - l - StretchAdjustment, ExtraStartZ + 1 }, 1.0, 0.0 };
			//ExtraVertecies[4] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ - h - 1 },0.0,1.0 };
			//ExtraVertecies[5] = { { ExtraStartX + l + StretchAdjustment,ExtraStartY - l - StretchAdjustment,ExtraStartZ - h - 1},1.0,1.0 };

			ExtraVertecies[0] = { {ExtraStartX - dw - dh,ExtraStartY + dw - dh,ExtraStartZ + dz},0.0f,0.0f };
			ExtraVertecies[1] = { {ExtraStartX + dw - dh,ExtraStartY - dw - dh,ExtraStartZ + dz},1.0f,0.0f };
			ExtraVertecies[2] = { {ExtraStartX - dw,ExtraStartY + dw,ExtraStartZ},0.0f,1.0f };
			ExtraVertecies[3] = { {ExtraStartX + dw - dh,ExtraStartY - dw - dh,ExtraStartZ + dz},1.0f,0.0f };
			ExtraVertecies[4] = { {ExtraStartX - dw,ExtraStartY + dw,ExtraStartZ},0.0f,1.0f };
			ExtraVertecies[5] = { {ExtraStartX + dw,ExtraStartY - dw,ExtraStartZ},1.0f,1.0f };
		}

		if (FAILED(pDevice->CreateVertexBuffer(sizeof ExtraVertecies, D3DUSAGE_DYNAMIC, TexturedVertex::dwFVFType,
			D3DPOOL_SYSTEMMEM, &pExtraVertexBuffer, nullptr)))
		{
			SAFE_RELEASE(pExtraVertexBuffer);
			goto DrawScene;
		}

		if (FAILED(pExtraVertexBuffer->Lock(0, sizeof ExtraVertecies, &pVertexData, D3DLOCK_DISCARD)))
		{
			SAFE_RELEASE(pExtraVertexBuffer);
			goto DrawScene;
		}

		memcpy_s(pVertexData, sizeof ExtraVertecies, ExtraVertecies, sizeof ExtraVertecies);
		pExtraVertexBuffer->Unlock();
	}

DrawScene:
	if (pTexture && pZTexture)
	{
		//this->AddDrawnObject(pVertexBuffer, Position);
		//this->AddTextureAtPosition(Position, pTexture);

		PaintingStruct::InitializePaintingStruct(PaintObject, pVertexBuffer, Position, pTexture);
		PaintObject.SetPlainArtAttributes(palette->GetPaletteTexture());
		PaintObject.SetZTexture(pZTexture);
		OutTileIndex =  this->CommitOpaqueObject(PaintObject);
	}

	if (pExtraTexture && pExtraZTexture)
	{
		//this->AddDrawnExtraObject(pExtraVertexBuffer, Position);
		//this->AddExtraTextureAtPosition(Position, pExtraTexture);
		PaintingStruct::InitializePaintingStruct(PaintObject, pExtraVertexBuffer, Position/* - HeightPoint*/, pExtraTexture);
		PaintObject.SetCompareOffset(-HeightPoint);
		PaintObject.SetPlainArtAttributes(palette->GetPaletteTexture());
		PaintObject.SetZTexture(pExtraZTexture);
		OutExtraIndex = this->CommitTransperantObject(PaintObject);
	}
	
	return OutExtraIndex || OutTileIndex;
}

LPDIRECT3DTEXTURE9 TmpFileClass::FindCellTexture(int nIndex)
{
	if (!this->CellTextures.size())
		return nullptr;

	auto Find = this->CellTextures.find(nIndex);
	if (Find != this->CellTextures.end())
		return Find->second;
	return nullptr;
}

LPDIRECT3DTEXTURE9 TmpFileClass::FindExtraTexture(int nIndex)
{
	if (!this->ExtraTextures.size())
		return nullptr;

	auto Find = this->ExtraTextures.find(nIndex);
	if (Find != this->ExtraTextures.end())
		return Find->second;
	return nullptr;
}

LPDIRECT3DTEXTURE9 TmpFileClass::FindExtraZTexture(int nIndex)
{
	if (!this->ExtraZTextures.size())
		return nullptr;

	auto Find = this->ExtraZTextures.find(nIndex);
	if (Find != this->ExtraZTextures.end())
		return Find->second;
	return nullptr;
}

LPDIRECT3DTEXTURE9 TmpFileClass::FindZTexture(int nIndex)
{
	if (!this->CellZTextures.size())
		return nullptr;

	auto Find = this->CellZTextures.find(nIndex);
	if (Find != this->CellZTextures.end())
		return Find->second;
	return nullptr;
}


void TmpFileClass::AddTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindCellTexture(nIndex))
		return;

	this->CellTextures[nIndex] = pTexture;
}

void TmpFileClass::AddZTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindZTexture(nIndex))
		return;

	this->CellZTextures[nIndex] = pTexture;
}

void TmpFileClass::AddExtraTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindExtraTexture(nIndex))
		return;

	this->ExtraTextures[nIndex] = pTexture;
}

void TmpFileClass::AddExtraZTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindExtraZTexture(nIndex))
		return;

	this->ExtraZTextures[nIndex] = pTexture;
}

void TmpFileClass::RemoveTexture(int nIndex)
{
	auto find = this->CellTextures.find(nIndex);
	if (find != this->CellTextures.end()) {
		find->second->Release();
		this->CellTextures.erase(nIndex);
	}
}

void TmpFileClass::RemoveZTexture(int nIndex)
{
	auto find = this->CellZTextures.find(nIndex);
	if (find != this->CellZTextures.end()) {
		find->second->Release();
		this->CellZTextures.erase(nIndex);
	}
}

void TmpFileClass::RemoveExtraTexture(int nIndex)
{
	auto find = this->ExtraTextures.find(nIndex);
	if (find != this->ExtraTextures.end()) {
		find->second->Release();
		this->ExtraTextures.erase(nIndex);
	}
}

void TmpFileClass::RemoveExtraZTexture(int nIndex)
{
	auto find = this->ExtraZTextures.find(nIndex);
	if (find != this->ExtraZTextures.end()) {
		find->second->Release();
		this->ExtraZTextures.erase(nIndex);
	}
}

void TmpFileClass::RemoveAllTextures()
{/*
	for (auto pair : this->CellTextures) {
		pair.second->Release();
	}

	for (auto pair : this->ExtraTextures) {
		pair.second->Release();
	}*/
	this->CellTextures.clear();
	this->CellZTextures.clear();
	this->ExtraTextures.clear();
	this->ExtraZTextures.clear();
}

bool operator==(const RECT & Left, const RECT & Right)
{
	return !memcmp(&Left, &Right, sizeof RECT);
}

bool operator!=(const RECT & Left, const RECT & Right)
{
	return !(Left == Right);
}

bool operator!(const RECT & Rectangle)
{
	return Rectangle == EmptyRect;
}

void DrawObject::RemoveTmpObject(int nID)
{
	for (auto& file : TmpFileClass::FileObjectTable) {
		if (!file.second)
			continue;
		//try find and erase
		file.second->RemoveOpaqueObject(nID);
		file.second->RemoveTransperantObject(nID);
	}
}