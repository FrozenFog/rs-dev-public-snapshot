#pragma once

#include "TmpFile.h"
#include "VxlFile.h"
#include "ShpFileClass.h"
#include "SceneClass.h"

#define EXPORT_START extern "C" {
#define EXPORT_END }
#define EXPORT __declspec(dllexport)
#define IMPORT __declspec(dllimport)

EXPORT_START

//palette api
EXPORT int WINAPI CreatePaletteFile(const char* pFileName);
EXPORT int WINAPI CreatePaletteFromFileInBuffer(LPVOID pFileBuffer);
EXPORT void WINAPI RemovePalette(int nID);

//file object
EXPORT int WINAPI CreateVxlFile(const char* pFileName);
EXPORT int WINAPI CreateVxlFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize, LPVOID pHvaBuffer, ULONG nHvaSize);
EXPORT bool WINAPI RemoveVxlFile(int nFileId);

EXPORT int WINAPI CreateTmpFile(const char* pFileName);
EXPORT int WINAPI CreateTmpFileFromFilenMemory(LPVOID pFileBuffer, ULONG nSize);
EXPORT bool WINAPI RemoveTmpFile(int nFileId);
EXPORT bool WINAPI LoadTmpTextures(int nFileId, int nPaletteId);

EXPORT int WINAPI CreateShpFile(const char* pFileName);
EXPORT int WINAPI CreateShpFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize);
EXPORT bool WINAPI RemoveShpFile(int nFileId);
EXPORT bool WINAPI LoadShpTextures(int nFileId, int nPaletteId, DWORD dwRemapColor);

//image object
EXPORT int WINAPI CreateVxlObjectAtScene(int nFileId, D3DXVECTOR3 Position,
	float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor);

EXPORT bool WINAPI CreateTmpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int nTileIndex, int& OutTileIndex, int& OutExtraIndex);

EXPORT int WINAPI CreateShpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int idxFrame, int nPaletteId, DWORD dwRemapColor, bool bFlat);

EXPORT void WINAPI MakeVxlFrameShot(int nFileId, LPCSTR pFileName, int idxFrame, float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor);

EXPORT void WINAPI RemoveObjectFromScene(int nID);

EXPORT void WINAPI RotateObject(int nID, float RotationX, float RotationY, float RotationZ);

EXPORT void WINAPI MoveObject(int nID, D3DXVECTOR3 Displacement);

EXPORT void WINAPI SetObjectLocation(int nID, D3DXVECTOR3 Position);

EXPORT void WINAPI SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient);

//scene api
EXPORT bool WINAPI SetUpScene(HWND hWnd);
EXPORT void WINAPI SetBackgroundColor(BYTE R, BYTE G, BYTE B);
EXPORT bool WINAPI ResetSceneView();

EXPORT void WINAPI PresentAllObject();

EXPORT void WINAPI MoveFocusOnScreen(float x, float y);
EXPORT void WINAPI MoveFocusOnScene(D3DXVECTOR3 Displacement);
EXPORT void WINAPI SetFocusOnScene(D3DXVECTOR3 Position);

EXPORT void WINAPI ScenePositionToClientPosition(D3DXVECTOR3 Position, POINT& Out);
EXPORT void WINAPI ClientPositionToScenePosition(POINT Position, D3DXVECTOR3& Out);

EXPORT void WINAPI ClearSceneObjects();

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