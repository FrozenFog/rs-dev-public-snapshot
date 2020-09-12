#pragma once
#include <Windows.h>

#include<d3d9.h>
#include <d3dx9.h>

#include <unordered_map>
#include <memory>

#include "Palette.h"

#define SAFE_RELEASE(obj)if(obj)obj->Release(),obj=nullptr;

#define SPECIAL_NORMAL	0
#define SPECIAL_SHADOW	1
#define SPECIAL_ALPHA	2

static const RECT EmptyRect = { LONG_MIN,LONG_MIN,LONG_MAX,LONG_MAX };

struct Voxel
{
	BYTE nColor;
	BYTE nNormal;

	Voxel();
};

struct PaintingStruct
{
	//the vectors passed here will all be copied, so you can put a pointer to a local variable.
	static void InitializePaintingStruct(PaintingStruct& Object,
		LPDIRECT3DVERTEXBUFFER9 pVertexBuffer,
		D3DXVECTOR3 Position,
		LPDIRECT3DTEXTURE9 pTexture = nullptr,
		char cSpecialDrawType = SPECIAL_NORMAL,
		std::vector<Voxel>* BufferedVoxels = nullptr,
		std::vector<D3DXVECTOR3>* BufferedNormals = nullptr,
		int nPalettID = -1,
		DWORD dwRemapColor = INVALID_COLOR_VALUE,
		std::string String = std::string()
	);

	//static ID counter;
	static long ID;

	//general
	float zAdjust;
	RECT VisualRect;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	D3DXVECTOR3 Position, CompareOffset;

	//for shps & tmps and should be released by class
	LPDIRECT3DTEXTURE9 pZTexture;
	LPDIRECT3DTEXTURE9 pTexture;
	LPDIRECT3DTEXTURE9 pPaletteTexture;
	D3DXVECTOR4 ShaderRemapColor;
	char cSpecialDrawType;

	//for vxl colorbuffer only
	std::vector<Voxel> BufferedVoxels;
	//for vxl normal vector only
	std::vector<D3DXVECTOR3> BufferedNormals;
	//for vxl color regeneration
	int nPaletteID;
	DWORD dwRemapColor;
	D3DXVECTOR4 ColorCoefficient;

	//for fonts
	std::string String;

	//should BeginScene() at first
	bool Draw(LPDIRECT3DDEVICE9 pDevice);
	void SetZTexture(LPDIRECT3DTEXTURE9 pTexture = nullptr);
	void InitializeVisualRect();
	bool IsWithinSight();
	void SetCompareOffset(D3DXVECTOR3 Offset);
	void SetColorCoefficient(D3DXVECTOR4 Coefficient);
	void SetPlainArtAttributes(LPDIRECT3DTEXTURE9 pPaletteTexture, D3DXVECTOR4 ShaderRemap = D3DXVECTOR4(1.0, 1.0, 1.0, 1.0));
	void SetZAdjust(float zAdjust);

	~PaintingStruct()
	{
#ifndef ISOLATED_THREAD
		SAFE_RELEASE(pVertexBuffer);
		SAFE_RELEASE(pTexture);
		SAFE_RELEASE(pZTexture);
#endif
	}
};

class DrawObject
{
public:
	//can be used as reference
	static std::unordered_map<int, PaintingStruct*> GlobalTransperantObjects;
	static std::unordered_map<int, PaintingStruct*> GlobalOpaqueObjects;
	static std::unordered_map<int, PaintingStruct*> GlobalTopObjects;

	static std::vector<LPDIRECT3DTEXTURE9> IsolatedTextures;
	static DWORD idTextureManagementThread;
	static HANDLE hTextureManagementThread;

	static void UpdateScene(LPDIRECT3DDEVICE9 pDevice, DWORD dwBackground);
	static void CommitIsotatedTexture(LPDIRECT3DTEXTURE9 pTexture);
	static bool IsTextureIsolated(LPDIRECT3DTEXTURE9 pTexture);
	static bool CanIsolatedTextureUnloadNow(LPDIRECT3DTEXTURE9 pTexture);
	static void UnloadIsolatedTexture(LPDIRECT3DTEXTURE9 pTexture);
	static DWORD WINAPI TextureManagementThreadProc(LPVOID pNothing = nullptr);

	DrawObject() = default;
	~DrawObject();
	
	//retained methods
	void ClearObjects();
	LPDIRECT3DVERTEXBUFFER9 FindDrawnObject(D3DXVECTOR3 Position);
	LPDIRECT3DVERTEXBUFFER9 FindDrawnExtraObject(D3DXVECTOR3 Position);
	void AddDrawnObject(LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, D3DXVECTOR3 Position);
	void AddDrawnExtraObject(LPDIRECT3DVERTEXBUFFER9 pVertexBuffer, D3DXVECTOR3 Position);
	void RemoveFromScene(D3DXVECTOR3 Position);
	void TransformObject(D3DXVECTOR3 Position, D3DXMATRIX& Matirx);
	void MoveObject(D3DXVECTOR3 Position, D3DXVECTOR3 ToPosition);
	void RotateObject(D3DXVECTOR3 Position, float RotationX, float RotationY, float RotationZ);
	void AddTextureAtPosition(D3DXVECTOR3 Position, LPDIRECT3DTEXTURE9 pTexture);
	void AddExtraTextureAtPosition(D3DXVECTOR3 Position, LPDIRECT3DTEXTURE9 pTexture);
	void RemoveTextures(D3DXVECTOR3 Position);
	LPDIRECT3DTEXTURE9 FindTexture(D3DXVECTOR3 Position);
	LPDIRECT3DTEXTURE9 FindExrtaTexture(D3DXVECTOR3 Position);
	void UpdateObject(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position);
	void UpdateExtraObject(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position);
	void UpdateAllObject(LPDIRECT3DDEVICE9 pDevice);

	//will be used
	int CommitTransperantObject(PaintingStruct& Object);
	int CommitOpaqueObject(PaintingStruct& Object);
	int CommitTopObject(PaintingStruct& Object);
	void ClearAllObjects();
	void RemoveTransperantObject(int nID);
	void RemoveOpaqueObject(int nID);
	void RemoveTopObject(int nID);

	//static methods for object transformation
	static PaintingStruct* FindObjectById(int nID);
	static void ObjectZAdjust(int nID, float zAdjust);
	static void ObjectTransformation(int nID, D3DXMATRIX& Matrix);
	static void ObjectDisplacement(int nID, D3DXVECTOR3 Displacement);
	static void ObjectMove(int nID, D3DXVECTOR3 Target);
	static D3DXVECTOR3 ObjectLocation(int nID);
	static void ObjectRotation(int nID, float RotationX, float RotationY, float RotationZ);
	static void SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient);
	static void RemoveTmpObject(int nID);
	static void RemoveVxlObject(int nID);
	static void RemoveShpObject(int nID);
	static void RemoveCommonObject(int nID);
	static void RemoveCommonTextureObject(int nID);

private:
	//the texture here cannot be released
	//will be obsolete
	std::unordered_map<LPDIRECT3DVERTEXBUFFER9, D3DXVECTOR3> CreatedVertex;
	std::unordered_map<LPDIRECT3DVERTEXBUFFER9, D3DXVECTOR3> CreatedExtraVertex;

	std::vector<std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>> PositionVertextable;
	std::vector<std::pair<D3DXVECTOR3, LPDIRECT3DTEXTURE9>> PositionExtraVertextable;

	//nImageID - PaintingStruct
	//vxls & tmp cells
	std::unordered_map<int, PaintingStruct> OpaqueImageTable;
	//shps & tmp extras
	std::unordered_map<int, PaintingStruct> TransperantImageTable;
	std::unordered_map<int, PaintingStruct> TopObjectTable;
};