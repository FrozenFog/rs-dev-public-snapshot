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
	DWORD dwPointerBuffer;
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
	for (int i = 0; i < nBlocks; i++)
	{
		//PointerBuffer is an RVA or a correct address
		dwPointerBuffer = reinterpret_cast<DWORD>(pFileData->ImageHeaders[i]);
		//the PointerBuffer is a valid offset
		if (dwPointerBuffer)
		{
			//check if the PointerBuffer is an RVA rather than a correct address
			if (dwPointerBuffer<reinterpret_cast<DWORD>(pFileData))
				//if it's an RVA ,add an ImageBase to it to make it a correct address;
				dwPointerBuffer += reinterpret_cast<DWORD>(pFileData);
			pFileData->ImageHeaders[i] = reinterpret_cast<TmpImageHeader*>(dwPointerBuffer);
		}
	}
	//set it to the class

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
	return pImageHeader ? pImageHeader->nExtraHeight > 0 && pImageHeader->nExtraWidth > 0 : false;
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
	return pPixels && this->HasExtraData(nIndex) ? &pPixels[pFileData->Header.nBlocksHeight*pFileData->Header.nBlocksWidth / 2] : nullptr;
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

bool TmpFileClass::MakeTextures(LPDIRECT3DDEVICE9 pDevice, Palette & Palette)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	RECT SizeRect, ExtraSizeRect;
	LPDIRECT3DTEXTURE9 pTexture, pExtraTexture;
	D3DLOCKED_RECT LockedRect;
	PBYTE pData, pFileData;

	for (auto item : this->CellTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	for (auto item : this->ExtraTextures) {
		DrawObject::CommitIsotatedTexture(item.second);
	}

	this->RemoveAllTextures();

	for (int i = 0; i < this->GetMaxBlockCount(); i++)
	{
		if (this->GetCellSizeRect(i, SizeRect), !SizeRect)
			continue;

		if (FAILED(pDevice->CreateTexture(this->GetFileData()->Header.nBlocksWidth, this->GetFileData()->Header.nBlocksHeight, 1, NULL,
			D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pTexture, nullptr)))
		{
			SAFE_RELEASE(pTexture);
			continue;
		}

		if (FAILED(pTexture->LockRect(0, &LockedRect, &SizeRect, NULL)))
		{
			SAFE_RELEASE(pTexture);
			continue;
		}

		pData = reinterpret_cast<PBYTE>(LockedRect.pBits);
		pFileData = this->GetPixelData(i);
		
		int y = 0;
		for (int x = this->GetFileData()->Header.nBlocksWidth / 2 - 2; x > 0; x -= 2, y++)
		{
			auto pTextureData = pData + y*LockedRect.Pitch;// +x * sizeof(D3DCOLOR_ARGB(0, 0, 0, 0));
			auto nSize = this->GetFileData()->Header.nBlocksWidth - 2 * x;
			bool bFirstEnter;

			bFirstEnter = true;
			RtlZeroMemory(pTextureData, this->GetFileData()->Header.nBlocksWidth * sizeof D3DCOLOR);
			pTextureData += x * sizeof D3DCOLOR;

			for (int i = 0; i < nSize; i++)
			{
				auto pColorData = reinterpret_cast<PDWORD>(pTextureData);
				auto nColor = *pFileData++;
				auto& Color = Palette[nColor];
				auto dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);

				if (nColor) {
					pColorData[0] = dwColor;
					if (bFirstEnter && x + i >= 1) {
						pColorData[-1] = dwColor;
						if (x + i >= 2)
							pColorData[-2] = dwColor;

						bFirstEnter = false;
					}
					if (!bFirstEnter && x + i <= this->GetFileData()->Header.nBlocksWidth - 1 && (*pFileData == 0 || i == nSize - 1)) {
						pColorData[1] = dwColor;
						if (x + i <= this->GetFileData()->Header.nBlocksWidth - 2)
							pColorData[2] = dwColor;

						bFirstEnter = true;
					}
				}
				pTextureData += sizeof D3DCOLOR;
			}
		}

		for (int x = 0; x <= this->GetFileData()->Header.nBlocksWidth / 2; x += 2, y++)
		{
			auto pTextureData = pData + y*LockedRect.Pitch;
			auto nSize = this->GetFileData()->Header.nBlocksWidth - 2 * x;
			bool bFirstEnter;

			bFirstEnter = true;
			RtlZeroMemory(pTextureData, this->GetFileData()->Header.nBlocksWidth * sizeof D3DCOLOR);
			pTextureData += x * sizeof D3DCOLOR;

			if (!nSize)
			{
				auto pColorData = reinterpret_cast<PDWORD>(pTextureData);
				BYTE nColor = 0;
				int i = 0;
				while (!nColor)
				{
					i++;
					nColor = *(pFileData - i);
				}
				auto& Color = Palette[nColor];
				auto dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
				pColorData[-2] = pColorData[-1] = pColorData[0] = pColorData[1] = dwColor;
			}
			for (int i = 0; i < nSize; i++)
			{
				auto pColorData = reinterpret_cast<PDWORD>(pTextureData);
				auto nColor = *pFileData++;
				auto& Color = Palette[nColor];
				auto dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);

				if (nColor) {
					pColorData[0] = dwColor;
					if (bFirstEnter && x + i >= 1) {
						pColorData[-1] = dwColor;
						if (x + i >= 2)
							pColorData[-2] = dwColor;
						bFirstEnter = false;
					}
					if (!bFirstEnter && x + i <= this->GetFileData()->Header.nBlocksWidth - 1 && (*pFileData == 0 || i == nSize - 1)) {
						pColorData[1] = dwColor;
						if (x + i <= this->GetFileData()->Header.nBlocksWidth - 2)
							pColorData[2] = dwColor;
						bFirstEnter = true;
					}
				}
				pTextureData += sizeof D3DCOLOR;
			}
		}

		pTexture->UnlockRect(0);
		this->AddTexture(i, pTexture);
/*
		char szFileName[MAX_PATH];
		sprintf_s(szFileName, "dump\\Otile_%p_%d.png", this, i);
		D3DXSaveTextureToFile(szFileName, D3DXIFF_PNG, pTexture, nullptr);*/

		if (!this->HasExtraData(i))
			continue;

		if (this->GetExtraSizeRect(i, ExtraSizeRect), !ExtraSizeRect)
			continue;

		if (FAILED(pDevice->CreateTexture(ExtraSizeRect.right, ExtraSizeRect.bottom, 0, NULL, 
			D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pExtraTexture, nullptr)))
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
			RtlZeroMemory(pData, ExtraSizeRect.right * sizeof D3DCOLOR);
			auto pColorData = reinterpret_cast<PDWORD>(pData);
			bool bEnter;

			bEnter = true;
			for (int i = 0; i < ExtraSizeRect.right; i++)
			{
				if (auto nColor = *pFileData++)
				{
					auto& Color = Palette[nColor];
					auto dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
					pColorData[i] = dwColor;
					if (bEnter) {
						if (i >= 1)
							pColorData[i - 1] = dwColor;
						if (i >= 2)
							pColorData[i - 2] = dwColor;
						bEnter = false;
					}
					if (!bEnter && *pFileData == 0 && i < ExtraSizeRect.right - 1) {
						pColorData[i + 1] = dwColor;
						if (i < ExtraSizeRect.right - 2)
							pColorData[i + 2] = dwColor;
						bEnter = true;
					}
				}
			}
			pData += LockedRect.Pitch;
		}

		pExtraTexture->UnlockRect(0);
		this->AddExtraTexture(i, pExtraTexture);
/*
		sprintf_s(szFileName, "dump\\tile_%p_%d.png", this, i);
		D3DXSaveTextureToFile(szFileName, D3DXIFF_PNG, pExtraTexture, nullptr);*/
	}

	return this->CellTextures.size() == this->GetValidBlockCount();
}

bool TmpFileClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position, int nTileIndex, int &OutTileIndex, int&OutExtraIndex)
{
	OutTileIndex = OutExtraIndex = 0;

	if (!pDevice || !this->IsLoaded()) {
		printf_s("not loaded.\n");
		return false;
	}

	//this->RemoveFromScene(Position);

	float PixelCellLength = this->GetFileData()->Header.nBlocksWidth / 2.0 * sqrt(2.0);
	float PixelCellVisualHeight = this->GetFileData()->Header.nBlocksHeight;
	float PixelCellVisualWidth = this->GetFileData()->Header.nBlocksWidth;
	const float StretchAdjustment = 0.0f;

	PaintingStruct PaintObject;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, pExtraVertexBuffer;
	LPDIRECT3DTEXTURE9 pTexture, pExtraTexture;
	RECT CoordsRect, CellRect, ExtraRect, CellSizeRect;
	LPVOID pVertexData;
	D3DXVECTOR3 HeightPoint(PixelCellVisualHeight / sqrt(2.0), PixelCellVisualHeight / sqrt(2.0), 0.0);

	pTexture = pExtraTexture = nullptr;
	pVertexBuffer = pExtraVertexBuffer = nullptr;

	pTexture = this->FindCellTexture(nTileIndex);
	if (!pTexture)
		return false;

	this->GetWholeRectWithHeight(CoordsRect);
	this->GetBlockRectWithHeight(nTileIndex, CellRect);
	this->GetBlockSizeRect(nTileIndex, CellSizeRect);

	float CellStartX = Position.x - PixelCellLength / 2.0;
	float CellStartY = Position.y - PixelCellLength / 2.0;

	TexturedVertex CellVertecies[] =
	{
		{ {CellStartX - StretchAdjustment,CellStartY - StretchAdjustment,Position.z},0.50f,0.00f },
		{ {CellStartX + PixelCellLength + StretchAdjustment,CellStartY - StretchAdjustment,Position.z},1.0f,0.50f },
		{ {CellStartX - StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z},0.0f,0.5f },
		{ { CellStartX + PixelCellLength + StretchAdjustment,CellStartY - StretchAdjustment,Position.z },1.0f,0.50f },
		{ { CellStartX - StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z },0.0f,0.5f },
		{ {CellStartX + PixelCellLength + StretchAdjustment,CellStartY + PixelCellLength + StretchAdjustment,Position.z},0.50f,1.0f },
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

		if (!pExtraTexture)
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
			float h = (ExtraRect.bottom - ExtraRect.top)*2.0 / sqrt(3.0);
			float l = (ExtraRect.right - ExtraRect.left) / sqrt(2.0);

			float ExtraStartX = Position.x - dx / sqrt(2.0);// -dy*sqrt(2.0);
			float ExtraStartY = Position.y + dx / sqrt(2.0);// -dy*sqrt(2.0);
			float ExtraStartZ = Position.z + dy*2.0 / sqrt(3.0);

			ExtraVertecies[0] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ + 1 },0.0,0.0 };
			ExtraVertecies[1] = { { ExtraStartX + l + StretchAdjustment, ExtraStartY - l - StretchAdjustment, ExtraStartZ + 1 }, 1.0, 0.0 };
			ExtraVertecies[2] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ - h - 1 },0.0,1.0 };
			ExtraVertecies[3] = { { ExtraStartX + l + StretchAdjustment, ExtraStartY - l - StretchAdjustment, ExtraStartZ + 1 }, 1.0, 0.0 };
			ExtraVertecies[4] = { { ExtraStartX - StretchAdjustment,ExtraStartY + StretchAdjustment,ExtraStartZ - h - 1 },0.0,1.0 };
			ExtraVertecies[5] = { { ExtraStartX + l + StretchAdjustment,ExtraStartY - l - StretchAdjustment,ExtraStartZ - h - 1},1.0,1.0 };
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
	if (pTexture)
	{
		//this->AddDrawnObject(pVertexBuffer, Position);
		//this->AddTextureAtPosition(Position, pTexture);

		PaintingStruct::InitializePaintingStruct(PaintObject, pVertexBuffer, Position, pTexture);
		OutTileIndex =  this->CommitOpaqueObject(PaintObject);
	}

	if (pExtraTexture)
	{
		//this->AddDrawnExtraObject(pExtraVertexBuffer, Position);
		//this->AddExtraTextureAtPosition(Position, pExtraTexture);
		PaintingStruct::InitializePaintingStruct(PaintObject, pExtraVertexBuffer, Position/* - HeightPoint*/, pExtraTexture);
		PaintObject.SetCompareOffset(-HeightPoint);
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


void TmpFileClass::AddTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindCellTexture(nIndex))
		return;

	this->CellTextures[nIndex] = pTexture;
}

void TmpFileClass::AddExtraTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture)
{
	if (this->FindExtraTexture(nIndex))
		return;

	this->ExtraTextures[nIndex] = pTexture;
}

void TmpFileClass::RemoveTexture(int nIndex)
{
	auto find = this->CellTextures.find(nIndex);
	if (find != this->CellTextures.end()) {
		find->second->Release();
		this->CellTextures.erase(nIndex);
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

void TmpFileClass::RemoveAllTextures()
{/*
	for (auto pair : this->CellTextures) {
		pair.second->Release();
	}

	for (auto pair : this->ExtraTextures) {
		pair.second->Release();
	}*/
	this->CellTextures.clear();
	this->ExtraTextures.clear();
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