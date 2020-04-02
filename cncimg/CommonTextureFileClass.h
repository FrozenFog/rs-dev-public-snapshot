#pragma once

#include "DrawObject.h"

class CommonTextureFileClass : public DrawObject
{
public:
	static std::unordered_map<int, std::unique_ptr<CommonTextureFileClass>> FileObjectTable;
	static void ClearAllObjectForAllFile();

	CommonTextureFileClass();
	CommonTextureFileClass(LPDIRECT3DDEVICE9 pDevice, const char* pFileName);
	~CommonTextureFileClass();

	bool LoadFromFile(LPDIRECT3DDEVICE9 pDevice, const char* pFileNmae);
	void RemoveTexture();
	int DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position);
	bool IsLoaded();

private:
	LPDIRECT3DTEXTURE9 pTexture;
};