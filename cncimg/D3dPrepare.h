#pragma once
#include <Windows.h>

#include <d3d9.h>
#include <d3dx9.h>

#define VIEW_WIDTH 1024
#define VIEW_HEIGHT 768

namespace Graphic
{
	bool Direct3DInitialize(HWND hWnd, const char* pShotFileName = nullptr, bool bUnion = false, int nDirections = 8, int TurretOff = 0);

	void Direct3DUninitialize();

	bool PrepareVertexBuffer(const char* pShotFileName = nullptr, bool bUnion = false, int nDirections = 8, int TurretOff = 0);

	void DrawScene();

	void WorldRotation();

	bool PrepareTileGround();

	bool DrawTileGround();

	void ClearScene();

	bool HandleDeviceLost();

	void InitliazeDeviceState();

	void ResetDevice();

	void SetCamera();

	void MoveFocus(float x, float y);

	void PlaceVXL(POINT Position);

	void MouseMove(POINT Position);

	void RemoveLastTmp();
};