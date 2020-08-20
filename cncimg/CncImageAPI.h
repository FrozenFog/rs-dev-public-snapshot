#pragma once

#include "TmpFile.h"
#include "VxlFile.h"
#include "ShpFileClass.h"
#include "SceneClass.h"
#include "CommonTextureFileClass.h"
#include "Misc.h"

#define EXPORT_START extern "C" {
#define EXPORT_END }
#define EXPORT __declspec(dllexport)
#define IMPORT __declspec(dllimport)

EXPORT_START

namespace GlobalID
{
	static int AllocatedGlobalId = 1;
}

//palette api
EXPORT int WINAPI CreatePaletteFile(const char* pFileName);
EXPORT int WINAPI CreatePaletteFromFileInBuffer(LPVOID pFileBuffer);
EXPORT void WINAPI RemovePalette(int nID);

//file object
EXPORT int WINAPI CreateVxlFile(const char* pFileName);
EXPORT int WINAPI CreateVxlFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize, LPVOID pHvaBuffer, ULONG nHvaSize);
EXPORT bool WINAPI RemoveVxlFile(int nFileId);

EXPORT int WINAPI CreateTmpFile(const char* pFileName);
EXPORT int WINAPI CreateTmpFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize);
EXPORT bool WINAPI RemoveTmpFile(int nFileId);
EXPORT bool WINAPI LoadTmpTextures(int nFileId);

EXPORT int WINAPI CreateShpFile(const char* pFileName);
EXPORT int WINAPI CreateShpFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize);
EXPORT bool WINAPI RemoveShpFile(int nFileId);
EXPORT bool WINAPI LoadShpTextures(int nFileId, int idxFrame);
EXPORT bool WINAPI IsShpFrameLoadedAsTexture(int nFileID, int idxFrame);

EXPORT int WINAPI CreateCommonTextureFile(const char* pFileName);
EXPORT int WINAPI CreateCircularCommonTextureFile(float Radius, float Thickness, DWORD dwD3DColor);

EXPORT bool WINAPI RemoveCommonTextureFile(int nFileId);
//image object
EXPORT int WINAPI CreateVxlObjectAtScene(int nFileId, D3DXVECTOR3 Position,
	float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor);
EXPORT bool WINAPI CreateVxlObjectCached(int nFileID, D3DXVECTOR3 Position, D3DXVECTOR3 ShadowPosition,
	float RotationZ, int nPaletteID, DWORD dwRemapColor, int& nID, int& nShadowId);

EXPORT bool WINAPI CreateTmpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int nPaletteID, int nTileIndex, int& OutTileIndex, int& OutExtraIndex);

EXPORT int WINAPI CreateShpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int idxFrame, int nPaletteId, DWORD dwRemapColor, char bFlat,
	int nFoundationX, int nFoundationY, int nHeight, char cSpecialDrawType);

EXPORT int WINAPI CreateCommonTextureObjectAtScene(int nFileId, D3DXVECTOR3 Position, bool bFlat = false);

EXPORT void WINAPI MakeVxlFrameShot(int nFileId, LPCSTR pFileName, int idxFrame, float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor);

EXPORT void WINAPI RemoveObjectFromScene(int nID);
EXPORT void WINAPI RemoveTmpFromScene(int nID);
EXPORT void WINAPI RemoveVxlFromScene(int nID);
EXPORT void WINAPI RemoveShpFromScene(int nID);
EXPORT void WINAPI RemoveCommonFromScene(int nID);
EXPORT void WINAPI RemoveCommonTextureFromScene(int nID);


EXPORT void WINAPI RotateObject(int nID, float RotationX, float RotationY, float RotationZ);

EXPORT void WINAPI MoveObject(int nID, D3DXVECTOR3 Displacement);

EXPORT void WINAPI SetObjectLocation(int nID, D3DXVECTOR3 Position);
EXPORT void WINAPI GetObjectLocation(int nID, D3DXVECTOR3& ReturnedLocation);

EXPORT void WINAPI SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient);
EXPORT void WINAPI SetObjectZAdjust(int nID, float zAdjust);

//scene api
EXPORT bool WINAPI SetUpScene(HWND hWnd);
EXPORT void WINAPI SetBackgroundColor(BYTE R, BYTE G, BYTE B);
EXPORT bool WINAPI ResetSceneView();
EXPORT void WINAPI EnableZWrite();
EXPORT void WINAPI DisableZWrite();

EXPORT void WINAPI PresentAllObject();

EXPORT void WINAPI MoveFocusOnScreen(float x, float y);
EXPORT void WINAPI MoveFocusOnScene(D3DXVECTOR3 Displacement);
EXPORT void WINAPI SetFocusOnScene(D3DXVECTOR3 Position);

EXPORT void WINAPI ScenePositionToClientPosition(D3DXVECTOR3 Position, POINT& Out);
EXPORT void WINAPI ClientPositionToScenePosition(POINT Position, D3DXVECTOR3& Out);

EXPORT void WINAPI ClearSceneObjects();

EXPORT int WINAPI CreateLineObjectAtScene(D3DXVECTOR3 Start, D3DXVECTOR3 End, DWORD dwStartColor, DWORD dwEndColor);
EXPORT int WINAPI CreateRectangleObjectAtScene(D3DXVECTOR3 Position, float XLength, float YLength, DWORD dwColor);

EXPORT bool WINAPI SetSceneFont(const char* pFontName, int nSize);
EXPORT int WINAPI CreateStringObjectAtScene(D3DXVECTOR3 Position, DWORD dwColor, const char* pString);
/*
Obsolete apis
*/

void MakeShots(const char* VxlFileName, int nTurretOffset, int nPaletteID, bool bUnion = false, int nDirections = 8,
	DWORD dwRemapColor = INVALID_COLOR_VALUE, int TurretOff = 0);



TheaterType GetCurrentTheater();
void SetCurrentTheater(TheaterType Theater);

//color scheme api DO NOT USE
void SetColorScheme(TheaterType Theater, int nColorSchemeID, COLORREF RemapColor);

bool IsColorSchemeInitialized();
void RotateWorld(float Angle);

EXPORT_END