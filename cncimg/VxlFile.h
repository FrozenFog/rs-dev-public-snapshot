#pragma once
#include "Palette.h"
#include "VxlMath.h"
#include "VPLFile.h"
#include "DrawObject.h"

#include <Windows.h>
#include <stdio.h>

#include <vector>


#define NORMALTYPE_TS 0x2
#define NORMALTYPE_RA 0x4

struct HVAStruct
{
public:
	HVAStruct();
	HVAStruct(const char* pFileName);
	HVAStruct(LPVOID pFileBuffer, ULONG nSize, bool bCopy);
	~HVAStruct();

	void Clear();
	bool LoadFromFile(const char* pFileName);
	bool LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, bool bCopy = true);
	bool IsLoaded();
	int GetFrameCount();
	int GetSectionCount();
	TransformationMatrix* GetTransformMatrix(int idxFrame, int idxSection);

private:
	char FileSignature[16];
	int nFrameCount;
	int nSectionCount;
	TransformationMatrix* FrameMatrices;
};

#pragma pack(1)
struct VxlFileHeader
{
	char szFileTypeName[16];
	DWORD dwUnused;
	UINT nNumberOfLimbs;
	UINT nAlsoNumberOfLimbs;
	UINT nBodySize;
	BYTE nRemapStartIndex;
	BYTE nRemapEndIndex;
	Palette ContainedPalette;

	void LoadFromBuffer(PBYTE pBuffer);
};
#pragma pack()

struct VxlLimbHeader
{
	char szLimbName[16];
	int nLimbNumber;
	DWORD dwReserved1;
	DWORD dwReserved2;

	void LoadFromBuffer(PBYTE pBuffer);
};

struct VxlLimbTailer
{
	UINT nSpanStartOffset;
	UINT nSpanEndOffset;
	UINT nSpanDataOffset;
	float fScale;
	TransformationMatrix Matrix;
	CoordStruct MinBounds;
	CoordStruct MaxBounds;
	BYTE nXSize;
	BYTE nYSize;
	BYTE nZSize;
	BYTE nNormalType;

	void LoadFromBuffer(PBYTE pBuffer);
};

struct SpanData
{
	BYTE nVoxels;
	std::vector<Voxel> Voxels;

	SpanData();
	void LoadSpanDataFromBuffer(PBYTE pBuffer, PBYTE pBufferEnd);
};

struct VxlLimbBody
{
	std::vector<INT> SpanStarts;
	std::vector<INT> SpanEnds;
	std::vector<SpanData> SpanData;

	VxlLimbBody();
	~VxlLimbBody();
	void Clear();
	void LoadFromBuffer(PBYTE pBuffer, VxlLimbTailer& TailerInfo);
};

class VxlFile : public DrawObject
{
public:
	static Palette TSNormals;
	static Palette RANormals;
	static D3DXVECTOR3 LightReversed;

	static std::unordered_map<int, std::unique_ptr<VxlFile>> FileObjectTable;

	static void ClearAllObjectForAllFile();

	VxlFile();
	VxlFile(const char* pFileName);
	VxlFile(const char* pFileName, const char* pPaletteFileName);
	VxlFile(LPVOID pFileBuffer, ULONG nSize,  LPVOID pHVABuffer, ULONG nHVASize, bool bCopy, bool bHVACopy);
	~VxlFile();

	void Clear();
	void LoadFromFile(const char* pFileName);
	void LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, LPVOID pHVABuffer, ULONG nHVASize, bool bCopy = true, bool bHVACopy = true);
	void PrintInfo();
	bool IsLoaded();
	bool GetVoxelRH(int nLimb, int x, int y, int z, Voxel& Voxel);
	bool GetVoxelLH(int nLimb, int x, int y, int z, Voxel& Voxel);
	void LoadPalette(const char* pPaletteName);
	int DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position,
		float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor, VPLFile& Vpl = VPLFile::GlobalVPL);
	void MakeFrameScreenShot(LPDIRECT3DDEVICE9 pDevice, int idxFrame, float RotationX, float RotationY, float RotationZ, int nPaletteID,
		DWORD dwRemapColor, VPLFile& Vpl = VPLFile::GlobalVPL);

#ifdef _DEBUG
public:
#else
private:
#endif

	PBYTE pFileBuffer;
	VxlFileHeader FileHeader;
	std::vector<VxlLimbBody> BodyData;
	std::vector<VxlLimbHeader> LimbHeaders;
	std::vector<VxlLimbTailer> LimbTailers;

	HVAStruct AssociatedHVA;
};