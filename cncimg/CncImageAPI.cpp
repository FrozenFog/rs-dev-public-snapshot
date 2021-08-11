#include "CncImageAPI.h"

EXPORT_START

int WINAPI CreatePaletteFile(const char * pFileName)
{
	//return reinterpret_cast<int>(new Palette(pFileName));
	auto pal = std::make_unique<Palette>(pFileName, SceneClass::Instance.GetDevice());
	if (pal)
	{
		auto id = GlobalID::AllocatedGlobalId++;

		Palette::PaletteTable[id] = std::move(pal);
		return id;
	}
	return 0;
}

int WINAPI CreatePaletteFromFileInBuffer(LPVOID pFileBuffer)
{
	auto pal = std::make_unique<Palette>(pFileBuffer, SceneClass::Instance.GetDevice());
	if (pal)
	{
		auto id = GlobalID::AllocatedGlobalId++;

		Palette::PaletteTable[id] = std::move(pal);
		return id;
	}
	return 0;
}

void WINAPI RemovePalette(int nID)
{
	Palette::PaletteTable.erase(nID);
}

//file api
int WINAPI CreateVxlFile(const char * pFileName)
{
	auto pFile = std::make_unique<VxlFile>(pFileName);
	if (pFile && pFile->IsLoaded()) 
	{
		auto id = GlobalID::AllocatedGlobalId++;

		VxlFile::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateVxlFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize, LPVOID pHvaBuffer, ULONG nHvaSize)
{
	auto pFile = std::make_unique<VxlFile>(pFileBuffer, nSize, pHvaBuffer, nHvaSize, true, true);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		VxlFile::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

bool WINAPI RemoveVxlFile(int nFileId)
{
	auto find = VxlFile::FileObjectTable.find(nFileId);
	if (find == VxlFile::FileObjectTable.end())
		return false;

	//find->second->ClearAllObjects();
	VxlFile::FileObjectTable.erase(find);
	return true;
}

int WINAPI CreateTmpFile(const char * pFileName)
{
	auto pFile = std::make_unique<TmpFileClass>(pFileName);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		TmpFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateTmpFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize)
{
	auto pFile = std::make_unique<TmpFileClass>(pFileBuffer, nSize, true);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		TmpFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

bool WINAPI RemoveTmpFile(int nFileId)
{
	auto find = TmpFileClass::FileObjectTable.find(nFileId);
	if (find == TmpFileClass::FileObjectTable.end())
		return false;

	//find->second->ClearAllObjects();
	TmpFileClass::FileObjectTable.erase(find);
	return true;
}

bool WINAPI LoadTmpTextures(int nFileId)
{
	auto find = TmpFileClass::FileObjectTable.find(nFileId);
	if (find == TmpFileClass::FileObjectTable.end())
		return false;

	return find->second->MakeTextures(SceneClass::Instance.GetDevice());
}

int WINAPI CreateShpFile(const char * pFileName)
{
	auto pFile = std::make_unique<ShpFileClass>(pFileName);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		ShpFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateShpFileFromFileInMemory(LPVOID pFileBuffer, ULONG nSize)
{
	auto pFile = std::make_unique<ShpFileClass>(pFileBuffer, nSize, true);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		ShpFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

bool WINAPI RemoveShpFile(int nFileId)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileId);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;

	//find->second->ClearAllObjects();
	ShpFileClass::FileObjectTable.erase(find);
	return true;
}

bool WINAPI LoadShpTextures(int nFileId, int idxFrame)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileId);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;

	return find->second->MakeTextures(SceneClass::Instance.GetDevice(), idxFrame);
}

bool WINAPI IsShpFrameLoadedAsTexture(int nFileID, int idxFrame)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileID);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;

	return find->second->IsFrameTextureLoaded(idxFrame);
}

int WINAPI CreateCommonTextureFile(const char * pFileName)
{
	if (!SceneClass::Instance.IsDeviceLoaded())
		return 0;

	auto pFile = std::make_unique<CommonTextureFileClass>(SceneClass::Instance.GetDevice(), pFileName);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		CommonTextureFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateCommonARGB32TextureFromColorBuffer(const void* lpFileBuffer, ULONG ulWidth, ULONG ulHeight)
{
	if (!SceneClass::Instance.IsDeviceLoaded())
		return 0;

	auto pFile = std::make_unique<CommonTextureFileClass>(SceneClass::Instance.GetDevice(), lpFileBuffer, ulWidth, ulHeight);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		CommonTextureFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateCircularCommonTextureFile(float Radius, float Thickness, DWORD dwD3DColor)
{
	if (!SceneClass::Instance.IsDeviceLoaded())
		return 0;

	auto pFile = std::make_unique<CommonTextureFileClass>(SceneClass::Instance.GetDevice(), Radius, Thickness, dwD3DColor);
	if (pFile && pFile->IsLoaded())
	{
		auto id = GlobalID::AllocatedGlobalId++;

		CommonTextureFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

bool WINAPI RemoveCommonTextureFile(int nFileId)
{
	auto find = CommonTextureFileClass::FileObjectTable.find(nFileId);
	if (find == CommonTextureFileClass::FileObjectTable.end())
		return false;

	//find->second->ClearAllObjects();
	CommonTextureFileClass::FileObjectTable.erase(find);
	return true;
}

int WINAPI CreateVxlObjectAtScene(int nFileId, D3DXVECTOR3 Position, float RotationX, float RotationY, float RotationZ, int nColorSchemeID, DWORD dwRemap, int offset)
{
	auto find = VxlFile::FileObjectTable.find(nFileId);
	if (find == VxlFile::FileObjectTable.end())
		return 0;

	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, RotationX, RotationY, RotationZ, nColorSchemeID, dwRemap, offset);
}

bool WINAPI CreateVxlObjectCached(int nFileID, D3DXVECTOR3 Position, D3DXVECTOR3 ShadowPosition, float RotationZ, 
	int nPaletteID, DWORD dwRemapColor, int offset, int& nID, int& nShadowID)
{
	auto find = VxlFile::FileObjectTable.find(nFileID);
	if (find == VxlFile::FileObjectTable.end())
		return false;

	auto& File = find->second;
	File->DrawCached(SceneClass::Instance.GetDevice(), Position, ShadowPosition, RotationZ, nPaletteID, dwRemapColor, offset, nID, nShadowID);

	return nID && nShadowID;
}

bool WINAPI CreateTmpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int nPaletteID, int nTileIndex, int& OutTileIndex, int& OutExtraIndex)
{
	auto find = TmpFileClass::FileObjectTable.find(nFileId);
	if (find == TmpFileClass::FileObjectTable.end())
		return false;

	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, nPaletteID, nTileIndex, OutTileIndex, OutExtraIndex);
}

int WINAPI CreateShpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int idxFrame, int nPaletteId, DWORD dwRemapColor, char bFlat,
	int nFoundationX, int nFoundationY, int nHeight, char cSpecialDrawType)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileId);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;
	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, idxFrame, bFlat, nPaletteId, dwRemapColor,
		nFoundationX, nFoundationY, nHeight, cSpecialDrawType);
}

int WINAPI CreateCommonTextureObjectAtScene(int nFileId, D3DXVECTOR3 Position, bool bFlat)
{
	auto find = CommonTextureFileClass::FileObjectTable.find(nFileId);
	if (find == CommonTextureFileClass::FileObjectTable.end())
		return false;
	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, bFlat);
}

void WINAPI MakeVxlFrameShot(int nFileId, LPCSTR pFile, int idxFrame, float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor)
{
	auto find = VxlFile::FileObjectTable.find(nFileId);
	if (find == VxlFile::FileObjectTable.end())
		return;

	auto pDevice = SceneClass::Instance.GetDevice();
	find->second->MakeFrameScreenShot(pDevice, pFile,"shadow.png", idxFrame,RotationX,RotationY,RotationZ,nPaletteID,dwRemapColor);
}

void WINAPI RemoveObjectFromScene(int nID)
{
	DrawObject::RemoveTmpObject(nID);
	DrawObject::RemoveVxlObject(nID);
	DrawObject::RemoveShpObject(nID);
	DrawObject::RemoveCommonObject(nID);
	DrawObject::RemoveCommonTextureObject(nID);
}

#define EXPORT_REMOVE_FUNCTION(cls)	\
	void WINAPI Remove ## cls ## FromScene(int nID)	\
	{DrawObject::Remove ## cls ## Object(nID);}

EXPORT_REMOVE_FUNCTION(Tmp)
EXPORT_REMOVE_FUNCTION(Vxl);
EXPORT_REMOVE_FUNCTION(Shp);
EXPORT_REMOVE_FUNCTION(Common);
EXPORT_REMOVE_FUNCTION(CommonTexture);

void WINAPI RotateObject(int nID, float RotationX, float RotationY, float RotationZ)
{
	DrawObject::ObjectRotation(nID, RotationX, RotationY, RotationZ);
}

void WINAPI MoveObject(int nID, D3DXVECTOR3 Displacement)
{
	DrawObject::ObjectDisplacement(nID, Displacement);
}

void WINAPI SetObjectLocation(int nID, D3DXVECTOR3 Position)
{
	DrawObject::ObjectMove(nID, Position);
}

void WINAPI GetObjectLocation(int nID, D3DXVECTOR3& ReturnedLocation)
{
	ReturnedLocation = DrawObject::ObjectLocation(nID);
}

void WINAPI SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient)
{
	DrawObject::SetObjectColorCoefficient(nID, Coefficient);
}

void WINAPI SetObjectZAdjust(int nID, float zAdjust)
{
	DrawObject::ObjectZAdjust(nID, zAdjust);
}

bool WINAPI SetUpScene(int nWidth, int nHeight)
{
	return SceneClass::Instance.SetUpScene(GetForegroundWindow(), nWidth, nHeight);//this will only return the back buffer
	// set render target via SetSceneSize method
}

LPDIRECT3DSURFACE9 WINAPI SetSceneSize(int nWidth, int nHeight)
{
	return SceneClass::Instance.SetSceneSize(nWidth, nHeight);
}

void WINAPI SetBackgroundColor(BYTE R, BYTE G, BYTE B)
{
	SceneClass::Instance.SetBackgroundColor(D3DCOLOR_XRGB(R, G, B));
}

bool WINAPI ResetSceneView()
{
	return SceneClass::Instance.ResetDevice();
}

void WINAPI EnableZWrite()
{
	SceneClass::Instance.EnableZWrite();
}

void WINAPI DisableZWrite()
{
	SceneClass::Instance.DisableZWrite();
}

void WINAPI PresentAllObject()
{
	DrawObject::UpdateScene(SceneClass::Instance.GetDevice(), SceneClass::Instance.GetBackgroundColor());
}

void WINAPI MoveFocusOnScreen(float x, float y)
{
	SceneClass::Instance.MoveFocus(x, y);
}

void WINAPI MoveFocusOnScene(D3DXVECTOR3 Displacement)
{
	SceneClass::Instance.MoveFocus(Displacement);
}

void WINAPI SetFocusOnScene(D3DXVECTOR3 Position)
{
	SceneClass::Instance.SetFocus(Position);
}

void WINAPI ScenePositionToClientPosition(D3DXVECTOR3 Position, POINT & Out)
{
	Out = SceneClass::Instance.CoordsToClient(Position);
}

void WINAPI ClientPositionToScenePosition(POINT Position, D3DXVECTOR3 & Out)
{
	Out = SceneClass::Instance.ClientToCoords(Position);
}

void WINAPI ClearSceneObjects()
{
	SceneClass::Instance.ClearScene();
}

int WINAPI CreateLineObjectAtScene(D3DXVECTOR3 Start, D3DXVECTOR3 End, DWORD dwStartColor, DWORD dwEndColor)
{
	auto pDevice = SceneClass::Instance.GetDevice();
	if (!pDevice)
		return false;

	return LineClass::GlobalLineGenerator.DrawAtScene(pDevice, Start, End, dwStartColor, dwEndColor);
}

int WINAPI CreateRectangleObjectAtScene(D3DXVECTOR3 Position, float XLength, float YLength, DWORD dwColor)
{
	auto pDevice = SceneClass::Instance.GetDevice();
	if (!pDevice)
		return false;

	return RectangleClass::GlobalRectangleGenerator.DrawAtScene(pDevice, Position, XLength, YLength, dwColor);
}

bool WINAPI SetSceneFont(const char * pFontName, int nSize)
{
	auto pBackSurface = SceneClass::Instance.GetBackSurface();
	HDC hDC;

	if (!pBackSurface)
		return false;

	if (FAILED(pBackSurface->GetDC(&hDC)))
		return false;

	auto Result = FontClass::GlobalFont.LoadFont(hDC, pFontName, nSize);

	pBackSurface->ReleaseDC(hDC);
	return Result;
}

int WINAPI CreateStringObjectAtScene(D3DXVECTOR3 Position, DWORD dwColor, const char * pString)
{
	return FontClass::GlobalFont.DrawAtScene(Position, dwColor, pString);
}

void MakeShots(const char * VxlFileName, int nTurretOffset, int nPaletteID, 
	bool bUnion, int nDirections, DWORD dwRemapColor, int TurretOff, 
	const char* pOutputPath, double dStartDirection, int bSkipAnim)
{
	if (!VxlFileName || nDirections % 8)
		return;

	char turName[MAX_PATH], barlName[MAX_PATH], vxlBody[MAX_PATH];

	strcpy_s(turName, VxlFileName);
	strcpy(turName + strlen(VxlFileName) - 4, "tur.vxl");

	strcpy_s(barlName, VxlFileName);
	strcpy(barlName + strlen(VxlFileName) - 4, "barl.vxl");

	strcpy_s(vxlBody, VxlFileName);

	char szDrive[4], szDir[MAX_PATH], szFileName[0x20], szExt[4];

	_splitpath(vxlBody, szDrive, szDir, szFileName, szExt);

	//strtok(vxlBody, ".");
	strcpy_s(vxlBody, szFileName);

	auto pVxl = std::make_unique<VxlFile>(VxlFileName);
	auto pTur = std::make_unique<VxlFile>(turName);
	auto pBarl = std::make_unique<VxlFile>(barlName);

	char szTargetName[MAX_PATH]{ 0 };
	char szShadowName[MAX_PATH]{ 0 };
	int idxFile = 0;
	if (pVxl && pVxl->IsLoaded() && pVxl->GetFrameCount())
	{
		auto anglesec = 2.0*D3DX_PI / nDirections;
		auto angle = -0.75*D3DX_PI + dStartDirection;

		auto shadowStart = pVxl->GetFrameCount()*nDirections;
		if (pTur->IsLoaded())
			shadowStart += nDirections;

		if (!bUnion || !pTur->IsLoaded())
			for (int i = 0; i < nDirections; i++)
			{
				for (int frame = 0; frame < pVxl->GetFrameCount(); frame++)
				{
					sprintf_s(szTargetName, "%s\\%s %04d.png", pOutputPath, vxlBody, idxFile);
					sprintf_s(szShadowName, "%s\\%s %04d.png", pOutputPath, vxlBody, idxFile++ + shadowStart);
					pVxl->MakeFrameScreenShot(SceneClass::Instance.GetDevice(), szTargetName, szShadowName, frame,
						0, 0, angle, nPaletteID, dwRemapColor);

					if (bSkipAnim) {
						break;
					}
				}
				angle += anglesec;
		}

		angle = -0.75*D3DX_PI + dStartDirection;
		for (int i = 0; pTur->IsLoaded() && i < nDirections; i++)
		{
			sprintf_s(szTargetName, "%s\\%s %04d.png", pOutputPath, vxlBody, idxFile);
			if (!bUnion)
			{
				sprintf_s(szShadowName, "%s\\%s %04d.png", pOutputPath, vxlBody, idxFile++ + shadowStart);
				pTur->MakeBarlTurScreenShot(SceneClass::Instance.GetDevice(), pBarl.get(), nullptr, szTargetName, szShadowName,
					0, 0.0, 0.0, angle, nPaletteID, dwRemapColor, TurretOff);
			}
			else
			{
				sprintf_s(szShadowName, "%s\\%s %04d.png", pOutputPath, vxlBody, idxFile++ + shadowStart - nDirections);
				pTur->MakeBarlTurScreenShot(SceneClass::Instance.GetDevice(), pBarl.get(), pVxl.get(), szTargetName, szShadowName,
					0, 0.0, 0.0, angle, nPaletteID, dwRemapColor, TurretOff);
			}
			angle += anglesec;
		}
	}
}

TheaterType GetCurrentTheater()
{
	return SceneClass::Instance.GetTheater();
}

void SetCurrentTheater(TheaterType Theater)
{
	SceneClass::Instance.SetTheater(Theater);
}

void SetColorScheme(TheaterType Theater, int nColorSchemeID, COLORREF RemapColor)
{
	ColorScheme::GlobalColorScheme.SetColorScheme(Theater, nColorSchemeID, RemapColor);
}

bool IsColorSchemeInitialized()
{
	return ColorScheme::GlobalColorScheme.IsLoaded();
}

void RotateWorld(float Angle)
{
	SceneClass::Instance.RotateScene(Angle);
}


EXPORT_END