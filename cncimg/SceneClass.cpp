#include "SceneClass.h"
#include "DrawObject.h"

#include "TmpFile.h"
#include "VxlFile.h"
#include "ShpFileClass.h"

SceneClass SceneClass::Instance;

SceneClass::SceneClass() :pResource(nullptr),
	pDevice(nullptr),
	pBackBuffer(nullptr),
	VoxelShader(),
	PlainArtShader()
{
	ZeroMemory(this, sizeof *this);
	dwBackgroundColor = D3DCOLOR_XRGB(0, 0, 252);
}

SceneClass::SceneClass(HWND hWnd) :SceneClass()
{
	SetUpScene(hWnd);
}

SceneClass::~SceneClass()
{
	this->ClearScene();
	this->ClearDevice();
}

void SceneClass::ClearScene()
{
	VxlFile::ClearAllObjectForAllFile();
	TmpFileClass::ClearAllObjectForAllFile();
	ShpFileClass::ClearAllObjectForAllFile();
}

void SceneClass::ClearDevice()
{
	SAFE_RELEASE(pBackBuffer);
	SAFE_RELEASE(pDevice);
	SAFE_RELEASE(pResource);
}

bool SceneClass::SetUpScene(HWND hWnd)
{
	auto& Para = this->SceneParas;
	Para.BackBufferCount = 1;
	Para.BackBufferFormat = D3DFMT_A8R8G8B8;
	Para.PresentationInterval = D3DPRESENT_INTERVAL_IMMEDIATE;
	Para.Flags = D3DPRESENTFLAG_LOCKABLE_BACKBUFFER;
	Para.SwapEffect = D3DSWAPEFFECT_DISCARD;
	Para.EnableAutoDepthStencil = TRUE;
	Para.AutoDepthStencilFormat = D3DFMT_D24S8;
	Para.Windowed = TRUE;
	Para.hDeviceWindow = hWnd;

	this->pResource = Direct3DCreate9(D3D_SDK_VERSION);
	if (!this->pResource)
		return false;

	if (FAILED(this->pResource->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_HARDWARE_VERTEXPROCESSING, &Para, &this->pDevice)))
	{
		this->ClearDevice();
		return false;
	}

	if (FAILED(this->pDevice->GetBackBuffer(0, 0, D3DBACKBUFFER_TYPE_MONO, &this->pBackBuffer)))
	{
		this->ClearDevice();
		return false;
	}

	if (!this->LoadShaders())
	{
		printf_s("failed loading shader.\n");
		this->ClearDevice();
		return false;
	}

	this->InitializeDeviceState();
	this->GetDevice()->SetVertexShader(this->VertexShader.GetVertexShader());

	DrawObject::hTextureManagementThread =
		CreateThread(nullptr, 0, DrawObject::TextureManagementThreadProc, nullptr, NULL, &DrawObject::idTextureManagementThread);

	return DrawObject::hTextureManagementThread != INVALID_HANDLE_VALUE;
}

bool SceneClass::IsDeviceLoaded()
{
	return this->pDevice != nullptr;// backbuffer is the last to be initialized
}

bool SceneClass::LoadShaders()
{
	const char* pShaderFile = ".\\shaders\\transformation.hlsl";
	const char* pVoxelShaderMain = "main";
	const char* pPlainShaderMain = "pmain";
	const char* pVertexMain = "vmain";
	const char* pVarName = "vec";
	const char* pMatrixName = "vpmatrix";

	if (this->VoxelShader.CompileFromFile(pShaderFile, pVoxelShaderMain) &&
		this->PlainArtShader.CompileFromFile(pShaderFile, pPlainShaderMain) &&
		this->VertexShader.CompileFromFile(pShaderFile, pVertexMain, true))
	{
		return
			this->VoxelShader.CreateShader(this->GetDevice()) &&
			this->PlainArtShader.CreateShader(this->GetDevice()) &&
			this->VertexShader.CreateVertexShader(this->GetDevice()) &&
			this->VoxelShader.LinkConstants(pVarName) &&
			this->PlainArtShader.LinkConstants(pVarName) &&
			this->VertexShader.LinkConstants(pMatrixName);
	}
	else
	{
		printf_s("compile error.\n");
		return false;
	}
}

void SceneClass::MoveFocus(FLOAT x, FLOAT y)
{
	D3DXVECTOR3 Move = { float((x + y) / sqrt(2.0)),float((y - x) / sqrt(2.0)),0.0 };
	this->MoveFocus(Move);
}

void SceneClass::MoveFocus(D3DXVECTOR3 Displacement)
{
	this->CurrentFocusLocation += Displacement;
	this->SetUpCamera();
}

void SceneClass::SetFocus(D3DXVECTOR3 Location)
{
	this->CurrentFocusLocation = Location;
	this->SetUpCamera();
}

D3DXVECTOR3 SceneClass::GetFocus()
{
	return this->CurrentFocusLocation;
}

void SceneClass::RotateScene(float Angle)
{
	if (!this->IsDeviceLoaded())
		return;

	D3DXMATRIX World, Rotation;

	this->GetDevice()->GetTransform(D3DTS_WORLD, &World);
	D3DXMatrixRotationZ(&Rotation, Angle);

	World *= Rotation;
	this->GetDevice()->SetTransform(D3DTS_WORLD, &World);
}

RECT SceneClass::GetCurrentViewPort()
{
	auto WndRect = this->GetWindowRect();
	auto CenterPoint = this->CoordsToScreen(this->GetFocus());

	return RECT{ CenterPoint.x - WndRect.right / 2,CenterPoint.y - WndRect.bottom / 2,
		CenterPoint.x + WndRect.right / 2,CenterPoint.y + WndRect.bottom / 2 };
}

RECT SceneClass::GetWindowRect()
{
	RECT WndRect;

	if (!this->IsDeviceLoaded())
		return EmptyRect;

	GetClientRect(this->SceneParas.hDeviceWindow, &WndRect);
	return WndRect;
}

void SceneClass::SetTheater(TheaterType Theater)
{
	this->Theater = Theater;
}

TheaterType SceneClass::GetTheater()
{
	return this->Theater;
}

POINT SceneClass::CoordsToClient(D3DXVECTOR3 Position)
{
	auto Focus = this->GetFocus();
	auto Displace = Position - Focus;
	auto ViewPort = this->GetWindowRect();

	D3DXVECTOR2 ScreenCenter = { (ViewPort.right - ViewPort.left) / 2.0f,(ViewPort.bottom - ViewPort.top) / 2.0f };

	float dx = (Displace.x - Displace.y) / sqrt(2.0);
	float dy = (Displace.x + Displace.y) / 2.0 / sqrt(2.0) - sqrt(3.0)*Displace.z / 2.0;
	
	D3DXVECTOR2 Target = ScreenCenter + D3DXVECTOR2(dx, dy);

	return POINT{ (long)Target.x,(long)Target.y };
}

POINT SceneClass::CoordsToScreen(D3DXVECTOR3 Position)
{
	float dx = Position.x / sqrt(2.0) - Position.y / sqrt(2.0);
	float dy = (Position.x / sqrt(2.0) + Position.y / sqrt(2.0)) / 2.0 - Position.z*sqrt(3.0) / 2.0;

	return POINT{ long(dx), long(dy) };
}

D3DXVECTOR3 SceneClass::ClientToCoords(POINT Point)
{
	auto Focus = this->GetFocus();
	auto ViewPort = this->GetWindowRect();

	D3DXVECTOR2 ScreenCenter = { (ViewPort.right - ViewPort.left) / 2.0f,(ViewPort.bottom - ViewPort.top) / 2.0f };
	auto Displace = D3DXVECTOR2(Point.x, Point.y) - ScreenCenter;

	float dx = sqrt(2.0)*Displace.y + Displace.x / sqrt(2.0);
	float dy = sqrt(2.0)*Displace.y - Displace.x / sqrt(2.0);

	return Focus + D3DXVECTOR3(dx, dy, 0.0f);
}

D3DXVECTOR3 SceneClass::ScreenToCoords(POINT Point)
{
	float dx = sqrt(2.0)*Point.y + Point.x / sqrt(2.0);
	float dy = sqrt(2.0)*Point.y - Point.x / sqrt(2.0);

	return D3DXVECTOR3(dx, dy, 0.0);
}

//relative distance
FLOAT SceneClass::GetDistanceToScreen(D3DXVECTOR3 Position)
{
	const FLOAT FarScreen = 4000.0f/3.0f*sqrt(2.0f);
	return sqrt(3.0) / 2.0*(FarScreen - (Position.x + Position.y) / sqrt(2.0) - Position.z / sqrt(3.0));
}

D3DXVECTOR3 SceneClass::FructumTransformation(RECT Screen, D3DXVECTOR3 Coords)
{
	float w = Screen.right - Screen.left;
	float h = Screen.bottom - Screen.top;
	float f = 5000.0f;

	D3DXVECTOR3 Result;

	Result.x = w / 2.0 + (Coords.x - Coords.y) / sqrt(2.0);
	Result.y = h / 2.0 + (Coords.x + Coords.y) / 2.0 / sqrt(2.0) - Coords.z*sqrt(3.0) / 2.0;
	Result.z = sqrt(3.0) / 2.0 / f*(4000.0*sqrt(2.0) / 3.0 - (Coords.x + Coords.y) / sqrt(2.0) - Coords.z / sqrt(3.0));

	return Result;
}

LPDIRECT3DDEVICE9 SceneClass::GetDevice()
{
	return this->pDevice;
}

LPDIRECT3DSURFACE9 SceneClass::GetBackSurface()
{
	return this->pBackBuffer;
}

ShaderStruct & SceneClass::GetVXLShader()
{
	return this->VoxelShader;
}

ShaderStruct & SceneClass::GetPlainArtShader()
{
	return this->PlainArtShader;
}

bool SceneClass::HandleDeviceLost()
{
	if (!this->IsDeviceLoaded())
		return false;

	auto hResult = this->pDevice->TestCooperativeLevel();
	if (SUCCEEDED(hResult) || hResult == D3DERR_DEVICENOTRESET)
	{
		return this->ResetDevice();
	}
	else if(hResult == D3DERR_DEVICELOST)
	{
		Sleep(200u);
	}

	return SUCCEEDED(hResult);
}

void SceneClass::InitializeDeviceState()
{
	if (!this->IsDeviceLoaded())
		return;

	D3DXMATRIX Project, View;

	auto hWnd = this->SceneParas.hDeviceWindow;
	RECT WindowRect;

	GetClientRect(hWnd, &WindowRect);
	D3DXMatrixOrthoLH(&Project, WindowRect.right, WindowRect.bottom, 0.0, 1000000.0);

	this->pDevice->SetTransform(D3DTS_PROJECTION, &Project);

	this->pDevice->Clear(0, nullptr, D3DCLEAR_ZBUFFER, this->dwBackgroundColor, 1.0, 0);

	this->pDevice->SetRenderState(D3DRS_LIGHTING, FALSE);
	this->pDevice->SetRenderState(D3DRS_ALPHABLENDENABLE, TRUE);
	//this->pDevice->SetTextureStageState(0, D3DTSS_ALPHAOP, D3DTOP_MODULATE);
	this->pDevice->SetRenderState(D3DRS_SRCBLEND, D3DBLEND_SRCALPHA);
	this->pDevice->SetRenderState(D3DRS_DESTBLEND, D3DBLEND_INVSRCALPHA);
	this->pDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE);
	this->pDevice->SetRenderState(D3DRS_SHADEMODE, D3DSHADE_FLAT);

	this->SetUpCamera();
	this->ResetShaderMatrix();
}

bool SceneClass::ResetDevice()
{
	if (!this->IsDeviceLoaded())
		return false;

	//minimize
	if (this->GetWindowRect().right == 0)
		return true;

	SAFE_RELEASE(this->pBackBuffer);

	this->GetDevice()->SetVertexShader(nullptr);
	SAFE_RELEASE(this->VertexShader.pVertexShader);
	SAFE_RELEASE(this->VoxelShader.pShaderObject);
	SAFE_RELEASE(this->PlainArtShader.pShaderObject);
	
	this->SceneParas.BackBufferWidth = this->SceneParas.BackBufferHeight = 0;
	auto hResult = this->pDevice->Reset(&this->SceneParas);

	this->pDevice->GetBackBuffer(0, 0, D3DBACKBUFFER_TYPE_MONO, &this->pBackBuffer);

	if (SUCCEEDED(hResult)) {
		this->InitializeDeviceState();
		this->VertexShader.CreateVertexShader(this->GetDevice());
		this->VoxelShader.CreateShader(this->GetDevice());
		this->PlainArtShader.CreateShader(this->GetDevice());
		this->GetDevice()->SetVertexShader(this->VertexShader.GetVertexShader());
	}

	return SUCCEEDED(hResult);
}

void SceneClass::SetUpCamera()
{
	D3DXMATRIX View;
	D3DXVECTOR3 Up{ 0.0,0.0,1.0 };
	D3DXVECTOR3 Target = this->GetFocus();
	D3DXVECTOR3 Eye = Target + D3DXVECTOR3(1000.0f, 1000.0f, 1000.0f*sqrt(2.0) / sqrt(3.0));
	D3DXMatrixLookAtLH(&View, &Eye, &Target, &Up);

	this->pDevice->SetTransform(D3DTS_VIEW, &View);
}

void SceneClass::SetBackgroundColor(DWORD dwColor)
{
	this->dwBackgroundColor = dwColor;
}

void SceneClass::ResetShaderMatrix()
{
	D3DXMATRIX World, View, Project;

	if (!this->IsDeviceLoaded())
		return;

	this->GetDevice()->GetTransform(D3DTS_WORLD, &World);
	this->GetDevice()->GetTransform(D3DTS_VIEW, &View);
	this->GetDevice()->GetTransform(D3DTS_PROJECTION, &Project);

	this->VertexShader.SetConstantMatrix(this->GetDevice(), World*View*Project);
}

DWORD SceneClass::GetBackgroundColor()
{
	return this->dwBackgroundColor;
}

bool ShaderStruct::IsLoaded()
{
	return this->pShader && this->pConstantTable;
}

bool ShaderStruct::CompileFromFile(const char * pSource, const char * pEntry, bool bVertexShader)
{
	LPD3DXBUFFER pErrorBuffer;

	const char* PixShaderName = "ps_3_0";
	const char* VerShaderName = "vs_3_0";

	auto hResult = D3DXCompileShaderFromFile(pSource, nullptr, nullptr, pEntry, bVertexShader ? VerShaderName : PixShaderName,
		D3DXSHADER_DEBUG, &this->pShader, &pErrorBuffer, &this->pConstantTable);

	if (pErrorBuffer) {
		printf_s("compile error : %s\n", pErrorBuffer->GetBufferPointer());
		pErrorBuffer->Release();
	}

	if (FAILED(hResult)) {
		this->ReleaseResources();
		return false;
	}

	return true;
}

bool ShaderStruct::LinkConstants(const char * pVarName)
{
	if (!this->IsLoaded())
		return false;

	this->hConstant = this->pConstantTable->GetConstantByName(NULL, pVarName);

	if (!this->hConstant)
		printf_s("failed to link constant %s.\n", pVarName);

	return this->hConstant != NULL;
}

bool ShaderStruct::SetConstantVector(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR4 Vector)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	return SUCCEEDED(this->pConstantTable->SetVector(pDevice, this->hConstant, &Vector));
}

bool ShaderStruct::SetConstantMatrix(LPDIRECT3DDEVICE9 pDevice, D3DXMATRIX Matrix)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	return SUCCEEDED(this->pConstantTable->SetMatrix(pDevice, hConstant, &Matrix));
}

bool ShaderStruct::CreateShader(LPDIRECT3DDEVICE9 pDevice)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	auto hResult = pDevice->CreatePixelShader(reinterpret_cast<PDWORD>(pShader->GetBufferPointer()), &this->pShaderObject);

	if (FAILED(hResult)) {
		this->ReleaseResources();
		printf_s("failed to create pixel shader.\n");
		return false;
	}

	return true;
}

bool ShaderStruct::CreateVertexShader(LPDIRECT3DDEVICE9 pDevice)
{
	if (!pDevice || !this->IsLoaded())
		return false;

	auto hResult = pDevice->CreateVertexShader(reinterpret_cast<PDWORD>(pShader->GetBufferPointer()), &this->pVertexShader);

	if (FAILED(hResult)) {
		this->ReleaseResources();
		printf_s("failed to create vertex shader.\n");
		return false;
	}

	return true;
}

LPDIRECT3DPIXELSHADER9 ShaderStruct::GetShaderObject()
{
	return this->pShaderObject;
}

LPDIRECT3DVERTEXSHADER9 ShaderStruct::GetVertexShader()
{
	return this->pVertexShader;
}

void ShaderStruct::ReleaseResources()
{
	SAFE_RELEASE(this->pShaderObject);
	SAFE_RELEASE(this->pShader);
	SAFE_RELEASE(this->pConstantTable);
}
