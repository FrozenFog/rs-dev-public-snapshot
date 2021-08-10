#include "VxlMath.h"

#include <ctime>
#include "DllLoggerClass.h"

D3DXMATRIX TransformationMatrix::Medium =
{
	1.0,		0.0,		0.0,		0.0,
	0.0,		-1.0,		0.0,		0.0,
	0.0,		0.0,		1.0,		0.0,
	0.0,		0.0,		0.0,		1.0
};

D3DXVECTOR3 CoordStruct::AsD3dVector()
{
	D3DXVECTOR3 Out;

	Out.x = X;
	Out.y = Y;
	Out.z = Z;
	return Out;
}

D3DXMATRIX CoordStruct::AsTranslationMatrix()
{
	D3DXMATRIX Translation, Identity;

	D3DXMatrixIdentity(&Identity);
	D3DXMatrixTranslation(&Translation, X, Y, Z);

	return Identity*Translation;
}

D3DXMATRIX TransformationMatrix::AsD3dMatrix(float Scale)
{
	return this->GetScaleRotationMatrix()*this->GetTranslationMatrix(Scale);
}

D3DXMATRIX TransformationMatrix::AsD3dMatrixWithMove(D3DXVECTOR3 & Move)
{
	D3DXMATRIX Translation;
	D3DXMATRIX Out = this->AsD3dMatrix();

	D3DXMatrixTranslation(&Translation, Move.x, Move.y, Move.z);

	return Out*Translation;
}

D3DXMATRIX TransformationMatrix::AsD3dMatrixWithMove(D3DXVECTOR3&& Move)
{
	D3DXVECTOR3 Mve = Move;
	return this->AsD3dMatrixWithMove(Mve);
}

D3DXMATRIX TransformationMatrix::GetTranslationMatrix(float Scale)
{
	//const FLOAT TranslationFactor = 1.0 / Scale;
	D3DXMATRIX Matrix;

	D3DXMatrixIdentity(&Matrix);
	Matrix.m[3][0] = this->Data[0][3] * Scale;
	Matrix.m[3][1] = -this->Data[1][3] * Scale;
	Matrix.m[3][2] = this->Data[2][3] * Scale;

	return Matrix;
}

D3DXMATRIX TransformationMatrix::GetScaleRotationMatrix()
{
	D3DXMATRIX Matrix;

	D3DXMatrixIdentity(&Matrix);

	Matrix.m[0][0] = this->Data[0][0];
	Matrix.m[0][1] = this->Data[1][0];
	Matrix.m[0][2] = this->Data[2][0];

	Matrix.m[1][0] = this->Data[0][1];
	Matrix.m[1][1] = this->Data[1][1];
	Matrix.m[1][2] = this->Data[2][1];

	Matrix.m[2][0] = this->Data[0][2];
	Matrix.m[2][1] = this->Data[1][2];
	Matrix.m[2][2] = this->Data[2][2];

	//Reversed
	return Medium * Matrix * Medium;
}

D3DXMATRIX TransformationMatrix::AsIntegrateMatrix(const D3DXVECTOR3& scale, const float det)
{
	D3DXMATRIX matrix;

	D3DXMatrixIdentity(&matrix);

	matrix.m[0][0] = this->Data[0][0];
	matrix.m[0][1] = this->Data[1][0];
	matrix.m[0][2] = this->Data[2][0];

	matrix.m[1][0] = this->Data[0][1];
	matrix.m[1][1] = this->Data[1][1];
	matrix.m[1][2] = this->Data[2][1];

	matrix.m[2][0] = this->Data[0][2];
	matrix.m[2][1] = this->Data[1][2];
	matrix.m[2][2] = this->Data[2][2];

	matrix.m[3][0] = this->Data[0][3] * det * scale.x;
	matrix.m[3][1] = this->Data[1][3] * det * scale.y;
	matrix.m[3][2] = this->Data[2][3] * det * scale.z;

	return matrix;
}

void TransformationMatrix::Print()
{
	for (int i = 0; i < 3; i++)
	{
		Logger::WriteLine(__FUNCTION__" : ""%.2f\t%.2f\t%.2f\t%.2f\n", Data[i][0], Data[i][1], Data[i][2], Data[i][3]);
	}
}

void TransformationMatrix::PrintMatrix(D3DXMATRIX Matrix)
{
	for (int i = 0; i < 4; i++)
	{
		Logger::WriteLine(__FUNCTION__" : ""%.2f\t%.2f\t%.2f\t%.2f\n", Matrix.m[i][0], Matrix.m[i][1], Matrix.m[i][2], Matrix.m[i][3]);
	}
}

D3DXVECTOR3 & operator*(D3DXVECTOR3 & Left, D3DXMATRIX & Right)
{
	D3DXVECTOR4 Result;
	D3DXVec3Transform(&Result, &Left, &Right);
	
	Left = { Result.x,Result.y,Result.z };
	return Left;
}

D3DXVECTOR3 & operator*=(D3DXVECTOR3 & Left, D3DXMATRIX & Right)
{
	return Left = Left * Right;
}

D3DVECTOR & operator*(D3DVECTOR & Left, D3DXMATRIX & Right)
{
	auto& rLeft = static_cast<D3DXVECTOR3&>(Left);
	return rLeft * Right;
}

D3DVECTOR & operator*=(D3DVECTOR & Left, D3DXMATRIX & Right)
{
	return Left = Left * Right;
}

double operator*(D3DXVECTOR3 & Left, D3DXVECTOR3 & Right)
{
	return D3DXVec3Dot(&Left, &Right);
}

//最大值nMax - 1，适合生成序列号
int Randomizer::RandomRanged(int nMin, int nMax)
{
	return nMin + rand() % (nMax - nMin);
}

void Randomizer::ResetRandomizer()
{
	time_t Time;

	time(&Time);
	srand(Time);
}
