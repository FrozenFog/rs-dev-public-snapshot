#pragma once

#include "DrawObject.h"
#include "Palette.h"

#include <Windows.h>
#include <stdio.h>

#include <d3dx9.h>
#include <d3d9.h>

struct ShpFileHeader
{
	USHORT nType;
	USHORT nImageWidth;
	USHORT nImageHeight;
	USHORT nFrameCount;
};

struct ShpFrameHeader
{
	short X, Y;
	short nWidth, nHeight;
	DWORD dwFlags;
	DWORD dwColor;
	DWORD dwUnk;
	DWORD dwOffset;
};

struct ShpFile
{
	ShpFileHeader FileHeader;
	ShpFrameHeader FrameHeaders[1];
};

class ShpFileClass :public DrawObject
{
public:
	static std::unordered_map<int, std::unique_ptr<ShpFileClass>> FileObjectTable;

	static void ClearAllObjectForAllFile();

	ShpFileClass();
	ShpFileClass(const char* pFileName);
	ShpFileClass(LPVOID pFileBuffer, ULONG nFileSize, bool bCopy);
	~ShpFileClass();

	void Clear();
	bool LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nFileSize,bool bCopy = true);
	bool LoadFromFile(const char* pFileName);
	bool IsLoaded();
	PBYTE GetFrameData(int idxFrame);
	int GetFrameCount();
	bool HasCompression(int idxFrame);
	RECT GetImageBounds();
	RECT GetFrameBounds(int idxFrame);
	ShpFrameHeader* GetFrameHeader(int idxFrame);
	ShpFileHeader* GetFileHeader();

	void RemoveAllTextures(int nPaletteId, DWORD dwRemapColor);
	bool MakeTextures(LPDIRECT3DDEVICE9 pDevice, int nPaletteID, DWORD dwRemapColor);
	int DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position, int idxFrame, bool bFlat, int nPaletteID, DWORD dwRemapColor);

private:
	ShpFile* FileData;
	//std::vector<LPDIRECT3DTEXTURE9> FrameTextures;
	//std::unordered_map<int, std::vector<LPDIRECT3DTEXTURE9>> FrameTextures;
	//frames = f(Palette, Remap)
	std::unordered_map<int, std::unordered_map<DWORD, std::vector<LPDIRECT3DTEXTURE9>>> FrameTextures;
};