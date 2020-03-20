#pragma once

#include <Windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include <stdio.h>

#include "ColorScheme.h"

class SceneClass
{
public:
	static const int nMaxViewWidth = 1920;
	static const int nMaxViewHeight = 1080;

	static SceneClass Instance;

	static D3DXVECTOR3 FructumTransformation(RECT Screen, D3DXVECTOR3 Coords);

	SceneClass();
	SceneClass(HWND hWnd);
	~SceneClass();

	void ClearScene();
	void ClearDevice();
	bool SetUpScene(HWND hWnd);
	bool IsDeviceLoaded();

	//focus
	void MoveFocus(FLOAT x, FLOAT y);
	void MoveFocus(D3DXVECTOR3 Displacement);
	void SetFocus(D3DXVECTOR3 Location);
	D3DXVECTOR3 GetFocus();
	//view port
	RECT GetCurrentViewPort();
	RECT GetWindowRect();
	//theater
	void SetTheater(TheaterType Theater);
	TheaterType GetTheater();
	//transform functions
	POINT CoordsToClient(D3DXVECTOR3 Position);
	POINT CoordsToScreen(D3DXVECTOR3 Position);
	D3DXVECTOR3 ClientToCoords(POINT Point);
	D3DXVECTOR3 ScreenToCoords(POINT Point);
	FLOAT GetDistanceToScreen(D3DXVECTOR3 Position);
	//device accessment
	LPDIRECT3DDEVICE9 GetDevice();
	LPDIRECT3DSURFACE9 GetBackSurface();
	//other
	bool HandleDeviceLost();
	void InitializeDeviceState();
	bool ResetDevice();
	void SetUpCamera();
	void SetBackgroundColor(DWORD dwColor);
	DWORD GetBackgroundColor();

private:
	//for color & calculation
	TheaterType Theater;
	D3DXVECTOR3 CurrentFocusLocation;
	DWORD dwBackgroundColor;
	//RECT CurrentViewPort;
	
	//for direct3d only
	LPDIRECT3D9 pResource;
	LPDIRECT3DDEVICE9 pDevice;
	LPDIRECT3DSURFACE9 pBackBuffer;
	D3DPRESENT_PARAMETERS SceneParas;
};