#pragma once

#include <Windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include <stdio.h>

#include "ColorScheme.h"
#include "DrawObject.h"

struct ShaderStruct
{
	LPD3DXBUFFER pShader;
	LPD3DXCONSTANTTABLE pConstantTable;
	D3DXHANDLE hConstant,hRemapConstant;

	union {
		LPDIRECT3DPIXELSHADER9 pShaderObject;
		LPDIRECT3DVERTEXSHADER9 pVertexShader;
	};

	ShaderStruct() :pShader(nullptr),
		pConstantTable(nullptr),
		pShaderObject(nullptr),
		hConstant(NULL),
		hRemapConstant(NULL)
	{}

	~ShaderStruct() 
		{ this->ReleaseResources(); }

	bool IsLoaded();
	bool CompileFromFile(const char* pSource, const char* pEntry, bool bVertexShader = false);
	bool LinkConstants(const char* pVarName);
	bool LinkRemapConstants(const char* pRemapName);
	bool SetRemapColor(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR4 Color = D3DXVECTOR4(1.0, 1.0, 1.0, 1.0));
	bool SetConstantVector(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR4 Vector = D3DXVECTOR4(1.0, 1.0, 1.0, 1.0));
	bool SetConstantMatrix(LPDIRECT3DDEVICE9 pDevice, D3DXMATRIX Matrix);
	bool SetVector(LPDIRECT3DDEVICE9 pDevice, LPCSTR pName, D3DXVECTOR4 Vector);
	bool SetMatrix(LPDIRECT3DDEVICE9 pDevice, LPCSTR pName, D3DXMATRIX Matrix);
	bool SetConstantF(LPDIRECT3DDEVICE9 pDevice, LPCSTR pName, const FLOAT fValue);
	bool CreateShader(LPDIRECT3DDEVICE9 pDevice);
	bool CreateVertexShader(LPDIRECT3DDEVICE9 pDevice);
	LPDIRECT3DPIXELSHADER9 GetShaderObject();
	LPDIRECT3DVERTEXSHADER9 GetVertexShader();
	void ReleaseResources();
};

class SceneClass
{
public:
	static const int nMaxViewWidth = 1920;
	static const int nMaxViewHeight = 1080;

	static SceneClass Instance;

	static D3DXVECTOR3 FructumTransformation(RECT Screen, D3DXVECTOR3 Coords);

	SceneClass();
	SceneClass(HWND hWnd, int nWidth, int nHeight);
	~SceneClass();

	void ClearScene();
	void ClearDevice();
	bool CreateSurfaces();
	LPDIRECT3DSURFACE9 SetUpScene(HWND hWnd, int nWidth, int nHeight);
	LPDIRECT3DSURFACE9 SetSceneSize(int nWidth, int nHeight);
	LPDIRECT3DSURFACE9 SetupNewRenderTarget(const size_t nWidth, const size_t nHeight);
	bool IsDeviceLoaded();
	bool LoadShaders();

	//focus
	void MoveFocus(FLOAT x, FLOAT y);
	void MoveFocus(D3DXVECTOR3 Displacement);
	void SetFocus(D3DXVECTOR3 Location);
	D3DXVECTOR3 GetFocus();
	void RotateScene(float Angle);
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
	LPDIRECT3DTEXTURE9 GetPassSurface();
	LPDIRECT3DTEXTURE9 GetAlphaSurface();
	LPDIRECT3DSURFACE9 GetRenderTarget();

	ShaderStruct& GetVXLShader();
	ShaderStruct& GetPlainArtShader();
	ShaderStruct& GetShadowShader();
	ShaderStruct& GetAlphaShader();
	ShaderStruct& GetPassShader();
	//other
	bool HandleDeviceLost();
	void InitializeDeviceState();
	bool ResetDevice();
	void SetUpCamera();
	void SetUpCamera(const D3DXVECTOR3& Eye, const D3DXVECTOR3& At);
	void SetUpCameraPerspective();
	void SetUpCameraPerspective(const D3DXVECTOR3& Eye, const D3DXVECTOR3& At);
	void SetBackgroundColor(DWORD dwColor);
	void ResetShaderMatrix();
	DWORD GetBackgroundColor();
	void EnableZWrite();
	void DisableZWrite();

	//alpha specifics
	void RefillAlphaImageSurface();
	void DrawAlphaImageToAlphaSurface(const PaintingStruct& paint);

private:
	//for color & calculation
	TheaterType Theater;
	D3DXVECTOR3 CurrentFocusLocation;
	DWORD dwBackgroundColor;
	size_t Width;
	size_t Height;
	//RECT CurrentViewPort;
	
	//for direct3d only
	LPDIRECT3D9 pResource;
	LPDIRECT3DDEVICE9 pDevice;
	LPDIRECT3DSURFACE9 pBackBuffer;
	LPDIRECT3DTEXTURE9 pPassSurface;
	LPDIRECT3DTEXTURE9 pAlphaSurface;
	LPDIRECT3DSURFACE9 pRenderTarget;
	D3DPRESENT_PARAMETERS SceneParas;

	ShaderStruct VoxelShader, PlainArtShader;
	ShaderStruct VertexShader;
	ShaderStruct ShadowShader;
	ShaderStruct AlphaShader;
	ShaderStruct PassShader;
};