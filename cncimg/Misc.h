#pragma once

#include <Windows.h>

#include <d3d9.h>
#include <d3dx9.h>

#include <memory>
#include <stdio.h>

#include "DrawObject.h"

class LineClass : public DrawObject
{
public:
	static LineClass GlobalLineGenerator;
	static void ClearAllSceneObject();

	LineClass() = default;
	~LineClass() = default;

	int DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Start, D3DXVECTOR3 End, DWORD dwStartColor, DWORD dwEndColor);
};

class FontClass : public DrawObject
{
public:
	static FontClass GlobalFont;
	static void ClearAllSceneObject();

	FontClass();
	~FontClass();

	void ClearFont();
	bool LoadFont(HDC hDC, const char* pName, int nSize);
	HFONT GetHFont();
	
	int DrawAtScene(D3DXVECTOR3 Position, DWORD dwColor, const char* pString);


private:
	HFONT hFont;
};