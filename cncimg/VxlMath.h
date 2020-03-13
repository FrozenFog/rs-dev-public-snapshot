#pragma once

#include <Windows.h>
#include <stdio.h>

#include <d3d9.h>
#include <d3dx9.h>

struct  CoordStruct
{
	float X, Y, Z;

	D3DXVECTOR3 AsD3dVector();
	D3DXMATRIX AsTranslationMatrix();
};

//RA2 / TS Matirix class
struct TransformationMatrix
{
	static D3DXMATRIX Medium;

	union {
		struct {
			float        _11,	_12,	_13,	_14;
			float        _21, _22, _23, _24;
			float        _31, _32, _33, _34;
		};
		float Data[3][4];
	};

	D3DXMATRIX AsD3dMatrix(float Scale = 1.0);
	D3DXMATRIX AsD3dMatrixWithMove(D3DXVECTOR3& Move);
	D3DXMATRIX AsD3dMatrixWithMove(D3DXVECTOR3&& Move);
	D3DXMATRIX GetTranslationMatrix(float Scale = 1.0);
	D3DXMATRIX GetScaleRotationMatrix();

	void Print();
	static void PrintMatrix(D3DXMATRIX Matrix);
};

class Randomizer
{
public:
	static int RandomRanged(int nMin, int nMax);
	static void ResetRandomizer();
};

D3DXVECTOR3 & operator*(D3DXVECTOR3 & Left, D3DXMATRIX & Right);

D3DXVECTOR3 & operator*=(D3DXVECTOR3 & Left, D3DXMATRIX & Right);

D3DVECTOR & operator*(D3DVECTOR & Left, D3DXMATRIX & Right);

D3DVECTOR & operator*=(D3DVECTOR & Left, D3DXMATRIX & Right);

double operator*(D3DXVECTOR3 & Left, D3DXVECTOR3 & Right);
