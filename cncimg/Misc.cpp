#include "Misc.h"

#include "VertexFormats.h"

LineClass LineClass::GlobalLineGenerator;
FontClass FontClass::GlobalFont;

void LineClass::ClearAllSceneObject()
{
	LineClass::GlobalLineGenerator.ClearAllObjects();
}

int LineClass::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Start, D3DXVECTOR3 End, DWORD dwStartColor, DWORD dwEndColor)
{
	if (!pDevice)
		return false;

	PaintingStruct Object;
	std::vector<Vertex> VertexBuffer;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPVOID pVertexData;

	VertexBuffer.push_back({ Start.x,Start.y,Start.z,dwStartColor });
	VertexBuffer.push_back({ End.x,End.y,End.z,dwEndColor });

	if (FAILED(pDevice->CreateVertexBuffer(VertexBuffer.size() * sizeof Vertex, D3DUSAGE_DYNAMIC, Vertex::dwFVFType,
		D3DPOOL_SYSTEMMEM, &pVertexBuffer, nullptr)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}
	
	if (FAILED(pVertexBuffer->Lock(0, 0, &pVertexData, D3DLOCK_DISCARD)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}

	RtlCopyMemory(pVertexData, VertexBuffer.data(), VertexBuffer.size() * sizeof Vertex);

	pVertexBuffer->Unlock();

	PaintingStruct::InitializePaintingStruct(Object, pVertexBuffer, Start);
	return this->CommitOpaqueObject(Object);
}

void FontClass::ClearAllSceneObject()
{
	FontClass::GlobalFont.ClearAllObjects();
}

FontClass::FontClass() : hFont(NULL)
{
}

FontClass::~FontClass()
{
	this->ClearFont();
}

void FontClass::ClearFont()
{
	DeleteObject(hFont);
}

bool FontClass::LoadFont(HDC hDC, const char * pName, int nSize)
{
	LOGFONT Font;

	ZeroMemory(&Font, sizeof Font);
	strcpy_s(Font.lfFaceName, pName);

	Font.lfHeight = -MulDiv(nSize, GetDeviceCaps(hDC, LOGPIXELSY), 72);

	hFont = CreateFontIndirect(&Font);
	return hFont != NULL;
}

HFONT FontClass::GetHFont()
{
	return hFont;
}

int FontClass::DrawAtScene(D3DXVECTOR3 Position, DWORD dwColor, const char * pString)
{
	if (!this->hFont)
		return false;

	PaintingStruct Object;

	PaintingStruct::InitializePaintingStruct(Object, nullptr, Position, nullptr, false, nullptr, nullptr, -1, dwColor, pString);

	printf_s("string = %s.\n", Object.String.c_str());
	return this->CommitTopObject(Object);
}
