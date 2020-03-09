#pragma once

#include "Palette.h"

#include <Windows.h>
#include <stdio.h>

class BitmapExtraction
{
public:
	BitmapExtraction();
	BitmapExtraction(const char* pFileName);
	~BitmapExtraction();

	void LoadFile(const char* pFileName);
	void ExtractAsPal(const char* pPalFile);
	void Clear();
	bool IsLoaded();

private:
	BITMAPFILEHEADER FileHeader;
	BITMAPINFOHEADER BitmapInfo;
	PBYTE pData;
};