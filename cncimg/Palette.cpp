#include "Palette.h"

#include "DrawObject.h"

#include <stdio.h>
#include <memory>

std::unordered_map<int, std::unique_ptr<Palette>> Palette::PaletteTable;

void Palette::ShiftColors()
{
	for (auto& Entry : this->Entries)
	{
		Entry.R <<= 2;
		Entry.G <<= 2;
		Entry.B <<= 2;
	}
}

void Palette::MakeRemapColor(DWORD dwBaseColor)
{
	if (dwBaseColor == INVALID_COLOR_VALUE)
		return;

	ColorStruct RemapColor;

	RemapColor.R = dwBaseColor & 0x000000FF;
	RemapColor.G = (dwBaseColor & 0x0000FF00) >> 8;
	RemapColor.B = (dwBaseColor & 0x00FF0000) >> 16;

	for (int i = GLOBAL_REMAP_START; i <= GLOBAL_REMAP_END; i++)
	{
		BYTE R, G, B;
		R = RemapColor.R*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		G = RemapColor.G*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		B = RemapColor.B*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		(*this)[i] = { R,G,B };
	}
}

bool Palette::Construct1DPaletteTexture(LPDIRECT3DDEVICE9 pDevice)
{
	if (!pDevice)
		return false;

	LPDIRECT3DTEXTURE9 pTexture = nullptr;
	D3DLOCKED_RECT LockedRect;

	if (FAILED(pDevice->CreateTexture(256, 1, 0, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pTexture, nullptr)))
		return false;

	if (FAILED(pTexture->LockRect(0, &LockedRect, nullptr, D3DLOCK_DISCARD)))
		goto Failed;

	auto pData = reinterpret_cast<PDWORD>(LockedRect.pBits);
	for (int i = 0; i < 256; i++)
	{
		auto color = this->Entries[i];
		if (i == 0)
			pData[i] = D3DCOLOR_ARGB(0, color.R, color.G, color.B);
		else
			pData[i] = D3DCOLOR_XRGB(color.R, color.G, color.B);
	}

	pTexture->UnlockRect(0);
	this->PaletteTexture = pTexture;

	return true;
Failed:
	if (pTexture)
		pTexture->Release();

	return false;
}

void Palette::Set1DPaletteTexture(LPDIRECT3DTEXTURE9 pTex)
{
	this->PaletteTexture = pTex;
}

LPDIRECT3DTEXTURE9 Palette::GetPaletteTexture()
{
	return this->PaletteTexture;
}

Palette * Palette::FindPaletteByID(int nID)
{
	auto find = Palette::PaletteTable.find(nID);
	if (find != Palette::PaletteTable.end()) {
		return find->second.get();
	}
	return nullptr;
}

Palette::Palette():PaletteTexture(nullptr)
{

}

Palette::~Palette()
{
	if (this->PaletteTexture)
		DrawObject::CommitIsotatedTexture(this->PaletteTexture);

	this->PaletteTexture = nullptr;
}

Palette::Palette(const char* pFileName, LPDIRECT3DDEVICE9 pDevice) : Palette()
{
	this->LoadFromFile(pFileName);
	this->Construct1DPaletteTexture(pDevice);
}

Palette::Palette(const Palette & Right) : Palette()
{
	memcpy_s(this->Entries, sizeof this->Entries, &const_cast<Palette&>(Right)[0], sizeof this->Entries);
}

Palette::Palette(LPVOID pFileBuffer, LPDIRECT3DDEVICE9 pDevice) : Palette()
{
	this->LoadFromFileInBuffer(pFileBuffer);
	this->ShiftColors();
	this->Construct1DPaletteTexture(pDevice);
}

ColorStruct& Palette::operator[](const int nIndex)
{
	return this->Entries[nIndex];
}

void Palette::LoadFromFile(const char* pFileName)
{
	auto hFile = CreateFile(pFileName, FILE_READ_ACCESS, FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr,
		OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	ULONG nReadBytes;
	auto pBuffer = malloc(sizeof this->Entries);
	if (!pBuffer || !ReadFile(hFile, pBuffer, sizeof this->Entries, &nReadBytes, nullptr) || nReadBytes != sizeof this->Entries)
		printf_s("failed to read palette.\n");

	this->LoadFromFileInBuffer(pBuffer);
	this->ShiftColors();
	
	if (pBuffer)
		free(pBuffer);
	CloseHandle(hFile);
}

void Palette::LoadFromFileInBuffer(LPVOID pFileBuffer)
{
	if (!pFileBuffer)
		return;

	memcpy_s(this->Entries, sizeof this->Entries, pFileBuffer, sizeof this->Entries);
}
