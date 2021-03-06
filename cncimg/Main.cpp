#include "D3dPrepare.h"
#include "BitmapExtractClass.h"
#include "TmpFile.h"
#include "DllLoggerClass.h"

#include <Windows.h>
#include <stdio.h>
#include <unordered_map>
#include <string>

#include <winternl.h>
#include <dsound.h>
#include <ddraw.h>

void PrepareConsole()
{
	AllocConsole();
	freopen("CONOUT$", "w", stdout);
	freopen("CONIN$", "r", stdin);
}

void CompileShader(const char* pSource, const char* pEntry)
{
	//
	// Compile shader
	//

	ID3DXConstantTable* TransformConstantTable = 0;
	ID3DXBuffer* shader = 0;
	ID3DXBuffer* errorBuffer = 0;

	auto hr = D3DXCompileShaderFromFile(pSource,
		0,
		0,
		pEntry,             // entry point function name
		"ps_1_1",           // HLSL shader name 
		D3DXSHADER_DEBUG,
		&shader,            // containing the created shader
		&errorBuffer,       // containing a listing of errors and warnings
		&TransformConstantTable);           // used to access shader constants
							// output any error messages
	if (errorBuffer)
	{
		MessageBox(0, (char*)errorBuffer->GetBufferPointer(), 0, 0);
		errorBuffer->Release();
	}

	if (FAILED(hr))
	{
		::MessageBox(0, "D3DXCreateEffectFromFile() - FAILED", 0, 0);
	}
}
LRESULT WINAPI WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

//暂时只画第一个section
//文件名flata.vxl / unittem.pals

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
	PrepareConsole();/*
	CompileShader("D:\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication2\\Release\\shaders\\Transformation.hlsl", "main");
	CompileShader("D:\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication2\\Release\\shaders\\Transformation.hlsl", "pmain");
*/
	LPDIRECTSOUND3DBUFFER;
	WNDCLASSEX WndClass;;
	ZeroMemory(&WndClass, sizeof WndClass);
	WndClass.lpszClassName = "D3DWIN";
	WndClass.lpfnWndProc = WndProc;
	WndClass.hInstance = hInstance;
	WndClass.cbSize = sizeof WndClass;
	WndClass.style = CS_PARENTDC;
	WndClass.hIcon = LoadIcon(hInstance, IDI_APPLICATION);
	WndClass.hIconSm = LoadIcon(hInstance, IDI_APPLICATION);
	WndClass.hCursor = LoadCursor(hInstance, IDC_ARROW);
	RegisterClassEx(&WndClass);

	const DWORD dwStyle = WS_OVERLAPPEDWINDOW | WS_VISIBLE;
	RECT WndRect = { 0,0,VIEW_WIDTH,VIEW_HEIGHT };

	HWND hWnd = CreateWindow("D3DWIN", "V.X.L", dwStyle, 0, 0, 0, 0,
		HWND_DESKTOP, NULL, hInstance, NULL);

	DWORD dwCurrentStyle = GetWindowLong(hWnd, GWL_STYLE);
	DWORD dwExStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
	BOOL bHasMenu = GetMenu(hWnd) != NULL;

	AdjustWindowRectEx(&WndRect, dwCurrentStyle, bHasMenu, dwExStyle);
	OffsetRect(&WndRect, 200, 200);
	MoveWindow(hWnd, WndRect.left, WndRect.top, WndRect.right - WndRect.left, WndRect.bottom - WndRect.top, TRUE);

	if (!hWnd)
	{
		Logger::WriteLine(__FUNCTION__" : ""window creation failed.\n");
		UnregisterClass("D3DWIN", hInstance);
		return 0;
	}

	if (!strlen(lpCmdLine) && !Graphic::Direct3DInitialize(hWnd))
	{
		Logger::WriteLine(__FUNCTION__" : ""d3d creation failed.\n");
		getchar();
		DestroyWindow(hWnd);
		UnregisterClass("D3DWIN", hInstance);
		return 0;
	}
	else if (strlen(lpCmdLine)) {
		char szFileName[MAX_PATH]{ 0 };
		char szOutPath[MAX_PATH]{ "\\Output" };
		int nDirections;
		int nTurretOff;
		int bUnion;
		int bSkipAnim;
		double dStartDirection;

		sscanf_s(lpCmdLine, "%d %d %d %d %lf %s %s", &nDirections, &nTurretOff, &bUnion, &bSkipAnim, &dStartDirection, szFileName, sizeof szFileName, szOutPath, sizeof szOutPath);
		printf_s("szFileName = %s \n", szFileName);
		printf_s("dStartDirection = %lf \n", dStartDirection);
		printf_s("szOutPath = %s \n", szOutPath);
		//getchar();
		if (!Graphic::Direct3DInitialize(hWnd, szFileName, bUnion, nDirections, nTurretOff, szOutPath, dStartDirection, bSkipAnim))
		{
			Logger::WriteLine(__FUNCTION__" : ""d3d creation failed.\n");
			DestroyWindow(hWnd);
			UnregisterClass("D3DWIN", hInstance);
			Sleep(2000);
			return 0;
		}
		else
		{
			printf_s("render complete .\n");
			DestroyWindow(hWnd);
		}
	}


	MSG Msg;
	ZeroMemory(&Msg, sizeof Msg);
	while (true)
	{
		if (PeekMessage(&Msg, NULL, 0, 0, PM_REMOVE))
		{
			if (Msg.message == WM_QUIT)
				break;

			TranslateMessage(&Msg);
			DispatchMessage(&Msg);
		}
		else
		{
			//Graphic::ClearScene();
			//Graphic::DrawScene();
			//Graphic::WorldRotation();
		}
	}

	Graphic::Direct3DUninitialize();

	//getchar();
	UnregisterClass("D3DWIN", hInstance);
	return 0;
}

void HandleDeleter(PHANDLE hObject)
{
	CloseHandle(hObject);
}

void PrepareLogFile()
{
	char szFileName[0x80];
	SYSTEMTIME time;

	if (!CreateDirectory(TEXT(".\\DllDebug"), nullptr) && GetLastError() != ERROR_ALREADY_EXISTS)
		return;

	GetLocalTime(&time);
	_snprintf_s(szFileName, _TRUNCATE, "DllDebug\\Debug-%04u%02u%02u-%02u%02u%02u-%05u.LOG",
		time.wYear, time.wMonth, time.wDay, time.wHour, time.wMinute, time.wSecond, time.wMilliseconds);

	Logger::Instance.OpenLogFile(szFileName);
	Logger::WriteLine("Log file created.");
}

bool WINAPI DllMain(HANDLE hInstance, DWORD dwReason, LPVOID v)
{
	if (dwReason == DLL_PROCESS_ATTACH)
		PrepareLogFile();
	else if (dwReason == DLL_PROCESS_DETACH)
		Logger::Instance.CloseLogFile();

	return true;
}

LRESULT WINAPI WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	static int x, y, z;
	POINTS Position;


	switch (uMsg)
	{
	case WM_PAINT:
		Graphic::DrawScene();
		Graphic::WorldRotation();
		break;

	case WM_LBUTTONDOWN:
		Position = MAKEPOINTS(lParam);
		Graphic::PlaceVXL(POINT{ Position.x,Position.y });
		break;

	case WM_MOUSEMOVE:/*
		Position = MAKEPOINTS(lParam);
		Graphic::MouseMove(POINT{ Position.x,Position.y });

		if (wParam == MK_MBUTTON)
			Graphic::SceneRotation();*/

		Graphic::MouseMovePerspective(MAKEPOINTS(lParam));
		break;

	case WM_SIZE:
		//Graphic::ResetDevice();
		break;

	case WM_KEYDOWN:
		switch (wParam)
		{
		case VK_SPACE:
			z = 1;
			break;

		case VK_UP:
		case 'W':
			x = 1;
			break;

		case VK_DOWN:
		case 'S':
			x = -1;
			break;

		case VK_LEFT:
		case 'A':
			y = -1;
			break;

		case VK_RIGHT:
		case 'D':
			y = 1;
			break;

		case VK_SHIFT:
			z = -1;
			break;

		case VK_ESCAPE:
			Graphic::ClearScene();
			break;
		case VK_RETURN:
			Graphic::RemoveLastTmp();
			break;
		default:
			break;
		}
		break;

	case WM_KEYUP:
		switch (wParam)
		{
		case VK_SPACE:
		case VK_SHIFT:
			z = 0;
			break;
		case VK_LEFT:
		case VK_RIGHT:
		case 'A':
		case 'D':
			y = 0;
			break;
		case VK_DOWN:
		case VK_UP:
		case 'W':
		case 'S':
			x = 0;
			break;
		default:
			break;
		}
		break;

	case WM_CLOSE:
		DestroyWindow(hWnd);
		break;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;

	case WM_ERASEBKGND:
		return TRUE;

	default:
		return DefWindowProc(hWnd, uMsg, wParam, lParam);
	}

	if (x)
	{
		Graphic::KeyDownMoveCamera(x, 0);
	}
	if (y)
	{
		Graphic::KeyDownMoveCamera(0, y);
	}
	if (z)
	{
		Graphic::KeyDownLiftCamera(z);
	}

	return 0;
}

