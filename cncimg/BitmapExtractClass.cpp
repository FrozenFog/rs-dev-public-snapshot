#include "BitmapExtractClass.h"

#include <fstream>
#include "DllLoggerClass.h"

BitmapExtraction::BitmapExtraction() : pData(nullptr)
{
}

BitmapExtraction::BitmapExtraction(const char * pFileName)
{
	this->pData = nullptr;

	this->LoadFile(pFileName);
}

BitmapExtraction::~BitmapExtraction()
{
	this->Clear();
}

void BitmapExtraction::LoadFile(const char * pFileName)
{
	this->Clear();

	auto hFile = CreateFile(pFileName, FILE_READ_ACCESS, FILE_SHARE_WRITE | FILE_SHARE_READ,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	ULONG nReadSize;
	if (!ReadFile(hFile, &this->FileHeader, sizeof this->FileHeader, &nReadSize, nullptr) || sizeof this->FileHeader != nReadSize)
		Logger::WriteLine(__FUNCTION__" : ""invalid load.\n");

	if (!ReadFile(hFile, &this->BitmapInfo, sizeof this->BitmapInfo, &nReadSize, nullptr) || sizeof this->BitmapInfo != nReadSize)
		Logger::WriteLine(__FUNCTION__" : ""invalid load.\n");

	SetFilePointer(hFile, this->FileHeader.bfOffBits, nullptr, FILE_BEGIN);
	this->pData = new BYTE[this->FileHeader.bfSize - this->FileHeader.bfOffBits];

	if (!ReadFile(hFile, this->pData, this->FileHeader.bfSize - this->FileHeader.bfOffBits, &nReadSize, nullptr) ||
		this->FileHeader.bfSize - this->FileHeader.bfOffBits != nReadSize)
		Logger::WriteLine(__FUNCTION__" : ""invalid pixel loading.\n");

	CloseHandle(hFile);
}

void BitmapExtraction::ExtractAsPal(const char * pPalFile)
{
	if (!this->IsLoaded())
		return;

	ULONG nWrittenBytes;
	auto hFile = CreateFile(pPalFile, FILE_WRITE_ACCESS, FILE_SHARE_READ,
		NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	auto nWidthUnit = this->BitmapInfo.biWidth;
	auto nHeightUnit = std::abs(this->BitmapInfo.biHeight);

	auto pColorData = reinterpret_cast<RGBQUAD*>(this->pData);

	ColorStruct PalColor;
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < 32; j++)
		{
			//coords is ( (0.5 + i) * nWidthUnit , (0.5 + j) * nHeightUnit )
			auto& Color = pColorData[(nHeightUnit / 64 + j*nHeightUnit / 32)*this->BitmapInfo.biWidth + nWidthUnit / 16 + i*nWidthUnit / 8];
			PalColor.R = Color.rgbRed >> 2;
			PalColor.G = Color.rgbGreen >> 2;
			PalColor.B = Color.rgbBlue >> 2;
			
			WriteFile(hFile, &PalColor, sizeof PalColor, &nWrittenBytes, nullptr);
		}
	}

	CloseHandle(hFile);
}

void BitmapExtraction::Clear()
{
	if (this->pData)
		delete[] this->pData;

	this->pData = nullptr;
}

bool BitmapExtraction::IsLoaded()
{
	return this->pData != nullptr;
}
