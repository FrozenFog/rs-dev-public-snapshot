#pragma once
#include "Palette.h"
#include "DrawObject.h"

#include <Windows.h>

#include <d3d9.h>
#include <d3dx9.h>

#include <unordered_map>
#include <vector>

#define PROTECTED(type,name)\
	protected:\
	type name;\
	public:\

/*
The general structure of a TMP file is defined like this:
TmpFileHeader;
TmpImageHeadersIndex;
+-----------------------------------------------------------------
TmpImageHeader_1
TmpImage_1_BlockData						size = 24*48/2=576 in TS, 30*60/2=900 in RA2/YR
TmpImage_1_BlockZShapeData
TmpImage_1_ExtraImageData_OPTIONAL
TmpImage_1_ExtraImageZShapeData_OPTIONAL
+-----------------------------------------------------------------
TmpImageHeader_2
TmpImage_2_BlockData
TmpImage_2_BlockZShapeData
TmpImage_2_ExtraImageData_OPTIONAL
TmpImage_2_ExtraImageZShapeData_OPTIONAL
+-----------------------------------------------------------------
......
*/

enum RampType :char
{
	Plane,
	NW, NE, SE, SW,
	N, E, S, W,
	NH, EH, SH, WH,
	DmN, DmE, DmS, DmW,
	DnWE, UpWE, DnNS, UpNS
};
struct TmpImageHeader
{
	int X, Y;
	PROTECTED(int, unused_3[3]);
	int nExtraX, nExtraY;
	int nExtraWidth, nExtraHeight;
	int unused_1;
	BYTE nHeight;
	BYTE TerrainType;
	RampType RampType;
	PROTECTED(BYTE, unused[1]);
	PROTECTED(int, unused_2[2]);
	BYTE PixelData[1];
};

struct TmpFileHeader
{
	int nXBlocks, nYBlocks;
	int nBlocksWidth, nBlocksHeight;
};

struct TmpFile
{
	TmpFileHeader Header;
	TmpImageHeader* ImageHeaders[1];
};

class TmpFileClass :public DrawObject
{
public:
	//using HTMP = HANDLE;
	static std::unordered_map<int, std::unique_ptr<TmpFileClass>> FileObjectTable;

	static void ClearAllObjectForAllFile();

	TmpFileClass();
	TmpFileClass(const char* pFileName);
	TmpFileClass(LPVOID pFileBuffer, ULONG nSize,bool bCopy);
	~TmpFileClass();

	void								Clear();
	bool								IsLoaded();
	void								LoadFromFile(const char* pFileName);
	void								LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, bool bCopy = true);
	TmpFile*						GetFileData();
	void								GetWholeRect(RECT& Rectangle);
	TmpImageHeader*			GetImageHeader(int nIndex);
	int									GetMaxBlockCount();
	int									GetValidBlockCount();
	bool								HasExtraData(int nIndex);
	RampType						GetRampType(int nIndex);
	PBYTE							GetPixelData(int nIndex);
	PBYTE							GetExtraPixelData(int nIndex);
	PBYTE							GetZShapeData(int nIndex);
	PBYTE							GetExtraZShapeData(int nIndex);
	void								GetBlockRect(int nIndex, RECT& Rectangle);
	void								GetBlockSizeRect(int nIndex, RECT& Rectangle);
	void								GetExtraBlockRect(int nIndex, RECT& Rectangle);
	void								GetExtraSizeRect(int nIndex, RECT& Rectangle);
	void								GetWholeSizeRect(RECT& Rectangle);
	void								GetBlockRectWithHeight(int nIndex, RECT& Rectangle);
	void								GetWholeRectWithHeight(RECT& Rectangle);
	void								GetWholeSizeRectWithHeight(RECT& Rectangle);
	void								GetCellRect(int nIndex, RECT& Rectangle);
	void								GetCellSizeRect(int nIndex, RECT& Rectangle);
	void								GetCellRectWithHeight(int nIndex, RECT& Rectangle);
	void								GetCellSizeRectWithHeight(int nIndex, RECT& Rectangle);
	bool								MakeTextures(LPDIRECT3DDEVICE9 pDevice, Palette& Palette);
	bool								DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position, int nTileIndex, int &OutTileIndex,int&OutExtraIndex);

private:
	LPDIRECT3DTEXTURE9 FindCellTexture(int nIndex);
	LPDIRECT3DTEXTURE9 FindExtraTexture(int nIndex);

	void AddTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture);
	void AddExtraTexture(int nIndex, LPDIRECT3DTEXTURE9 pTexture);
	void RemoveTexture(int nIndex);
	void RemoveExtraTexture(int nIndex);
	void RemoveAllTextures();

	TmpFile* pFileData;
	std::unordered_map<int, LPDIRECT3DTEXTURE9> CellTextures;
	std::unordered_map<int, LPDIRECT3DTEXTURE9> ExtraTextures;


	//std::vector<std::pair<int, LPDIRECT3DTEXTURE9>> CellTextures;
	//std::vector<std::pair<int, LPDIRECT3DTEXTURE9>> ExtraTextures;
	//std::vector<std::pair<LPDIRECT3DVERTEXBUFFER9, D3DXVECTOR3>> CreatedVertex;
	//std::vector<std::pair<LPDIRECT3DVERTEXBUFFER9, D3DXVECTOR3>> CreatedExtraVertex;
};

bool operator==(const RECT& Left, const RECT& Right);/*
													 {
													 return !memcmp(&Left, &Right, sizeof RECT);
													 }*/

bool operator!=(const RECT& Left, const RECT& Right);/*
													 {
													 return !(Left == Right);
													 }*/
bool operator!(const RECT& Rectangle);/*
									  {
									  return Rectangle == EmptyRect;
									  }*/