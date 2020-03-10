#include "Palette.h"

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

Palette * Palette::FindPaletteByID(int nID)
{
	auto find = Palette::PaletteTable.find(nID);
	if (find != Palette::PaletteTable.end()) {
		return find->second.get();
	}
	return nullptr;
}

Palette::Palette()
{
	//PaletteTable[reinterpret_cast<int>(this)] = this;
}

Palette::~Palette()
{
	//PaletteTable.erase(reinterpret_cast<int>(this));
}

Palette::Palette(const char* pFileName) : Palette()
{
	this->LoadFromFile(pFileName);
}

Palette::Palette(Palette & Right) : Palette()
{
	memcpy_s(this->Entries, sizeof this->Entries, &Right[0], sizeof this->Entries);
}

Palette::Palette(LPVOID pFileBuffer):Palette()
{
	this->LoadFromFileInBuffer(pFileBuffer);
}

ColorStruct& Palette::operator[](int nIndex)
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
