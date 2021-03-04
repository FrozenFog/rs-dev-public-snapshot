#include "VPLFile.h"
#include "DllLoggerClass.h"
VPLFile VPLFile::GlobalVPL("voxels.vpl");

VPLFile::VPLFile(const char * pFileName)
{
	this->LoadFromFile(pFileName);
}

void VPLFile::Clear()
{
	this->Sections.clear();
}

void VPLFile::LoadFromFile(const char * pFileName)
{
	auto hFile = CreateFile(pFileName, FILE_READ_ACCESS, FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr,
		OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	ULONG nReadBytes;
	if (!ReadFile(hFile, &this->Header, sizeof this->Header, &nReadBytes, nullptr) || nReadBytes != nReadBytes)
		return (void)CloseHandle(hFile);

	this->Sections.resize(this->Header.nSections);
	if (!ReadFile(hFile, &this->ContainedPal, sizeof this->ContainedPal, &nReadBytes, nullptr) || nReadBytes != sizeof this->ContainedPal)
		Logger::WriteLine(__FUNCTION__" : ""failed to load contained normal pal.\n");

	for (int i = 0; i < 256; i++)
	{
		this->ContainedPal[i].R <<= 2;
		this->ContainedPal[i].G <<= 2;
		this->ContainedPal[i].B <<= 2;
	}

	if (!ReadFile(hFile, this->Sections.data(), sizeof VPLSectionTable*this->Sections.size(), &nReadBytes, nullptr) ||
		nReadBytes != sizeof VPLSectionTable*this->Sections.size())
		Logger::WriteLine(__FUNCTION__" : ""failed to load vpl sections.\n");

	CloseHandle(hFile);
}

void VPLFile::ExtractContainedPal(const char * pOutFileName)
{
	if (!this->IsLoaded())
		return;

	auto hFile = CreateFile(pOutFileName, FILE_WRITE_ACCESS, FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr,
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	ULONG nWrittenBytes;
	if (!WriteFile(hFile, &this->ContainedPal, sizeof this->ContainedPal, &nWrittenBytes, nullptr) || nWrittenBytes != sizeof this->ContainedPal)
		Logger::WriteLine(__FUNCTION__" : ""failed to extract the contained normal palette in VPL file.\n");

	CloseHandle(hFile);
}

bool VPLFile::IsLoaded()
{
	return this->Sections.size() != 0;
}

VPLSectionTable & VPLFile::operator[](int nIndex)
{
	return this->Sections[nIndex];
}
