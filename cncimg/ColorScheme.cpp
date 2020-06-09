#include "ColorScheme.h"
#include "SceneClass.h"

#include <cstdio>

ColorScheme ColorScheme::GlobalColorScheme;

ColorScheme::ColorScheme()
{
	//this->LoadAllBasePalettes();
}

ColorScheme::~ColorScheme()
{
	this->Clear();
}

bool ColorScheme::IsLoaded()
{
	return this->TheaterPalettes.size() != 0;
}

void ColorScheme::Clear()
{
	this->TheaterPalettes.clear();
}

void ColorScheme::LoadAllBasePalettes()
{
	if (this->IsLoaded())
		return;

	const char* PalettePrefix = "Palettes\\unit";
	const char* TilePalettePrefix = "Palettes\\iso";
	const char* PaletteSuffixes[] =
	{
		"tem",
		"urb",
		"sno",
		"ubn",
		"des",
		"lun",
	};

	Palette Palette;
	this->TheaterPalettes.resize(MAX_THEATER_COUNT);
	this->TilePalettes.resize(MAX_THEATER_COUNT);

	char FileName[MAX_PATH]{ 0 };
	for (int i = 0; i < MAX_THEATER_COUNT; i++)
	{
		sprintf_s(FileName, "%s%s.pal", PalettePrefix, PaletteSuffixes[i]);
		Palette.LoadFromFile(FileName);
		this->TheaterPalettes[i][BASE_PALETTE_INDEX] = Palette;

		sprintf_s(FileName, "%s%s.pal", TilePalettePrefix, PaletteSuffixes[i]);
		Palette.LoadFromFile(FileName);
		this->TilePalettes[i] = Palette;
	}
}

void ColorScheme::SetColorScheme(TheaterType Theater, int nColorSchemeID, COLORREF RemapColor)
{
	if (!this->IsLoaded())
		return;

	if (nColorSchemeID == BASE_PALETTE_INDEX)
		return;
	
	ColorStruct Color;
	Color.R = RemapColor & 0x000000FF;
	Color.G = (RemapColor & 0x0000FF00) >> 8;
	Color.B = (RemapColor & 0x00FF0000) >> 16;

	Palette NewPalette;

	NewPalette = this->TheaterPalettes[static_cast<int>(Theater)][BASE_PALETTE_INDEX];
	for (int i = GLOBAL_REMAP_START; i <= GLOBAL_REMAP_END; i++)
	{
		BYTE R, G, B;
		R = Color.R*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		G = Color.G*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		B = Color.B*(GLOBAL_REMAP_END + 1 - i) / (GLOBAL_REMAP_END + 1 - GLOBAL_REMAP_START);
		NewPalette[i] = { R,G,B };
	}

	this->TheaterPalettes[static_cast<int>(Theater)][nColorSchemeID] = NewPalette;
}

bool ColorScheme::GetPalette(TheaterType Theater, int nColorSchemeID, Palette & PaletteOut)
{
	if (!this->IsLoaded())
		return false;
	
	auto& TheaterMap = this->TheaterPalettes[static_cast<int>(Theater)];
	auto& Find = TheaterMap.find(nColorSchemeID);
	if (Find != TheaterMap.end())
	{
		PaletteOut = Find->second;
		return true;
	}
	return false;
}

bool ColorScheme::GetCurrentTilePalette(Palette & PaletteOut)
{
	
	auto Theater = SceneClass::Instance.GetTheater();
	
	if (!this->IsLoaded())
		return false;

	PaletteOut = this->TilePalettes[static_cast<int>(Theater)];
	return true;
	
}