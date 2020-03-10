#pragma once
#include <Windows.h>

#include <unordered_map>
#include <memory>

#define INVALID_COLOR_VALUE 0xFFFFFFFFu
#define MAX_THEATER_COUNT 6
#define BASE_PALETTE_INDEX -1

#define GLOBAL_REMAP_START 16
#define GLOBAL_REMAP_END 31

struct ColorStruct
{
	BYTE R, G, B;
};

//RA2 Palette Class
class Palette
{
public:
	static std::unordered_map<int, std::unique_ptr<Palette>> PaletteTable;

	static Palette* FindPaletteByID(int nID);

	Palette();
	Palette(const char* pFileName);
	Palette(Palette& Right);
	Palette(LPVOID pFileBuffer);
	~Palette();

	void LoadFromFile(const char* pFileName);
	void LoadFromFileInBuffer(LPVOID pFileBuffer);
	void ShiftColors();
	void MakeRemapColor(DWORD dwBaseColor);
	ColorStruct& operator[](int nIndex);

private:
	ColorStruct Entries[256];
};