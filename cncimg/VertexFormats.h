#pragma once
#include <Windows.h>

#include <d3d9.h>
#include <d3dx9.h>

struct PlainVertex
{
	D3DXVECTOR3 Vector;
	float Rhw, U, V;

	static const DWORD dwFVFType = D3DFVF_XYZRHW | D3DFVF_TEX1;
};

struct TexturedVertex
{
	D3DXVECTOR3 Vector;
	float U, V;

	static const DWORD dwFVFType = D3DFVF_XYZ | D3DFVF_TEX1;
};

struct Vertex
{
	union
	{
		struct
		{
			float X, Y, Z;
		};
		D3DXVECTOR3 Vector;
	};
	DWORD dwColor;

	static const DWORD dwFVFType = D3DFVF_XYZ | D3DFVF_DIFFUSE;
};
//unused
struct NormalizedVertex
{
	union
	{
		struct
		{
			float X, Y, Z;
		};
		D3DXVECTOR3 Vector;
	};

	union
	{
		struct
		{
			float Xn, Yn, Zn;
		};
		D3DXVECTOR3 NormalVector;
	};
	DWORD dwColor;

	static const DWORD dwFVFType = D3DFVF_XYZ | D3DFVF_NORMAL | D3DFVF_DIFFUSE;
};
