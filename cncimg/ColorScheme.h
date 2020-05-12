#pragma once

#include "Palette.h"

#include <Windows.h>
#include <stdio.h>

#include <vector>
#include <unordered_map>


enum class TheaterType : int
{
	Temperate = 0,
	Urban = 1,
	Snowy = 2,
	NewUrban = 3,
	Dessert = 4,
	Lunar = 5, 
};

//RA2/TS ColorSchemeClass
class ColorScheme
{
public:
	static ColorScheme GlobalColorScheme;

	 ColorScheme();
	~ColorScheme();

	bool IsLoaded();
	void Clear();
	void LoadAllBasePalettes();

	void SetColorScheme(TheaterType Theater, int nColorSchemeID, COLORREF RemapColor);
	bool GetPalette(TheaterType Theater, int nColorSchemeID, Palette& PaletteOut);
	bool GetCurrentTilePalette(Palette& PaletteOut);
	
private:
	std::vector<std::unordered_map<int, Palette>> TheaterPalettes; //6 mpas
	std::vector<Palette> TilePalettes;
};