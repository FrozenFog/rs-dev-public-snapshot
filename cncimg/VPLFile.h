#pragma once

#include "Palette.h"

#include <vector>

struct VPLHeader
{
	int nRemapStart;
	int nRemapEnd;
	int nSections;
	DWORD dwUnk;
};

struct VPLSectionTable
{
	BYTE Table[256];
};

class VPLFile
{
public:
	static VPLFile GlobalVPL;

	VPLFile() = default;
	~VPLFile() = default;
	VPLFile(const char* pFileName);

	void Clear();
	void LoadFromFile(const char*pFileName);
	void ExtractContainedPal(const char* pOutFileName);
	bool IsLoaded();
	VPLSectionTable& operator[](int nIndex);

private:
	VPLHeader Header;
	Palette ContainedPal;
	std::vector<VPLSectionTable> Sections;
};