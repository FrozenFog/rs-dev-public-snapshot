#include "CncImageAPI.h"

EXPORT_START

int WINAPI CreatePaletteFile(const char * pFileName)
{
	//return reinterpret_cast<int>(new Palette(pFileName));
	auto pal = std::make_unique<Palette>(pFileName);
	if (pal)
	{
		auto id = reinterpret_cast<int>(pal.get());

		Palette::PaletteTable[id] = std::move(pal);
		return id;
	}
	return 0;
}

int WINAPI CreatePaletteFromFileInBuffer(LPVOID pFileBuffer)
{
	auto pal = std::make_unique<Palette>(pFileBuffer);
	if (pal)
	{
		auto id = reinterpret_cast<int>(pal.get());

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
		auto id = reinterpret_cast<int>(pFile.get());

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
		auto id = reinterpret_cast<int>(pFile.get());

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
		auto id = reinterpret_cast<int>(pFile.get());

		TmpFileClass::FileObjectTable[id] = std::move(pFile);
		return id;
	}
	return 0;
}

int WINAPI CreateTmpFileFromFilenMemory(LPVOID pFileBuffer, ULONG nSize)
{
	auto pFile = std::make_unique<TmpFileClass>(pFileBuffer, nSize, true);
	if (pFile && pFile->IsLoaded())
	{
		auto id = reinterpret_cast<int>(pFile.get());

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

bool WINAPI LoadTmpTextures(int nFileId,int nPaletteID)
{
	auto find = TmpFileClass::FileObjectTable.find(nFileId);
	if (find == TmpFileClass::FileObjectTable.end())
		return false;

	if(auto palette = Palette::FindPaletteByID(nPaletteID))
		return find->second->MakeTextures(SceneClass::Instance.GetDevice(), *palette);
	return false;
}

int WINAPI CreateShpFile(const char * pFileName)
{
	auto pFile = std::make_unique<ShpFileClass>(pFileName);
	if (pFile && pFile->IsLoaded())
	{
		auto id = reinterpret_cast<int>(pFile.get());

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
		auto id = reinterpret_cast<int>(pFile.get());

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

bool WINAPI LoadShpTextures(int nFileId, int nPaletteId, DWORD dwRemapColor)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileId);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;

	return find->second->MakeTextures(SceneClass::Instance.GetDevice(), nPaletteId, dwRemapColor);
}

int WINAPI CreateVxlObjectAtScene(int nFileId, D3DXVECTOR3 Position, float RotationX, float RotationY, float RotationZ, int nColorSchemeID, DWORD dwRemap)
{
	auto find = VxlFile::FileObjectTable.find(nFileId);
	if (find == VxlFile::FileObjectTable.end())
		return 0;

	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, RotationX, RotationY, RotationZ, nColorSchemeID, dwRemap);
}

bool WINAPI CreateTmpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int nTileIndex, int& OutTileIndex, int& OutExtraIndex)
{
	auto find = TmpFileClass::FileObjectTable.find(nFileId);
	if (find == TmpFileClass::FileObjectTable.end())
		return false;

	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, nTileIndex, OutTileIndex, OutExtraIndex);
}

int WINAPI CreateShpObjectAtScene(int nFileId, D3DXVECTOR3 Position, int idxFrame, int nPaletteId, DWORD dwRemapColor, bool bFlat)
{
	auto find = ShpFileClass::FileObjectTable.find(nFileId);
	if (find == ShpFileClass::FileObjectTable.end())
		return false;
	return find->second->DrawAtScene(SceneClass::Instance.GetDevice(), Position, idxFrame, bFlat, nPaletteId, dwRemapColor);
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
}

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

void WINAPI SetObjectColorCoefficient(int nID, D3DXVECTOR4 Coefficient)
{
	DrawObject::SetObjectColorCoefficient(nID, Coefficient);
}

bool WINAPI SetUpScene(HWND hWnd)
{
	return SceneClass::Instance.SetUpScene(hWnd);
}

void WINAPI SetBackgroundColor(BYTE R, BYTE G, BYTE B)
{
	SceneClass::Instance.SetBackgroundColor(D3DCOLOR_XRGB(R, G, B));
}

bool WINAPI ResetSceneView()
{
	return SceneClass::Instance.ResetDevice();
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

void MakeShots(const char * VxlFileName, int nTurretOffset, int nPaletteID, bool bUnion, int nDirections, DWORD dwRemapColor, int TurretOff)
{
	if (!VxlFileName || nDirections % 8)
		return;

	char turName[MAX_PATH], barlName[MAX_PATH], vxlBody[MAX_PATH];

	strcpy_s(turName, VxlFileName);
	strcpy(turName + strlen(VxlFileName) - 4, "tur.vxl");

	strcpy_s(barlName, VxlFileName);
	strcpy(barlName + strlen(VxlFileName) - 4, "barl.vxl");

	strcpy_s(vxlBody, VxlFileName);
	strtok(vxlBody, ".");

	auto pVxl = std::make_unique<VxlFile>(VxlFileName);
	auto pTur = std::make_unique<VxlFile>(turName);
	auto pBarl = std::make_unique<VxlFile>(barlName);

	char szTargetName[MAX_PATH]{ 0 };
	char szShadowName[MAX_PATH]{ 0 };
	int idxFile = 0;
	if (pVxl && pVxl->IsLoaded() && pVxl->GetFrameCount())
	{
		auto anglesec = 2.0*D3DX_PI / nDirections;
		auto angle = -0.75*D3DX_PI;

		auto shadowStart = pVxl->GetFrameCount()*nDirections;
		if (pTur->IsLoaded())
			shadowStart += nDirections;

		if (!bUnion)
			for (int i = 0; i < nDirections; i++)
			{
				for (int frame = 0; frame < pVxl->GetFrameCount(); frame++)
				{
					sprintf_s(szTargetName, "Output\\%s %04d.png", vxlBody, idxFile);
					sprintf_s(szShadowName, "Output\\%s %04d.png", vxlBody, idxFile++ + shadowStart);
					pVxl->MakeFrameScreenShot(SceneClass::Instance.GetDevice(), szTargetName, szShadowName, frame,
						0, 0, angle, nPaletteID, dwRemapColor);
				}
				angle += anglesec;
		}

		angle = -0.75*D3DX_PI;
		for (int i = 0; pTur->IsLoaded() && i < nDirections; i++)
		{
			sprintf_s(szTargetName, "Output\\%s %04d.png", vxlBody, idxFile);
			if (!bUnion)
			{
				sprintf_s(szShadowName, "Output\\%s %04d.png", vxlBody, idxFile++ + shadowStart);
				pTur->MakeBarlTurScreenShot(SceneClass::Instance.GetDevice(), pBarl.get(), nullptr, szTargetName, szShadowName,
					0, 0.0, 0.0, angle, nPaletteID, dwRemapColor, TurretOff);
			}
			else
			{
				sprintf_s(szShadowName, "Output\\%s %04d.png", vxlBody, idxFile++ + shadowStart - nDirections);
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