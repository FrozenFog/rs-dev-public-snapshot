#pragma once
#include <Windows.h>

#include <d3d9.h>

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
	Palette(const char* pFileName, LPDIRECT3DDEVICE9 pDevice);
	Palette(const Palette& Right);
	Palette(LPVOID pFileBuffer, LPDIRECT3DDEVICE9 pDevice);
	~Palette();

	Palette& operator=(const Palette& Right);

	void LoadFromFile(const char* pFileName);
	void LoadFromFileInBuffer(LPVOID pFileBuffer);
	void ShiftColors();
	void MakeRemapColor(DWORD dwBaseColor);
	bool Construct1DPaletteTexture(LPDIRECT3DDEVICE9 pDevice);
	void Set1DPaletteTexture(LPDIRECT3DTEXTURE9 pTex);
	LPDIRECT3DTEXTURE9 GetPaletteTexture();
	ColorStruct& operator[](const int nIndex);

private:
	ColorStruct Entries[256];
	LPDIRECT3DTEXTURE9 PaletteTexture;
};