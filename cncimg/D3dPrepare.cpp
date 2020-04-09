#include "D3dPrepare.h"

#include "CncImageAPI.h"
#include "DemoCellClass.h"

namespace Graphic
{
	std::vector<int> VxlFiles;
	std::vector<int> TmpFiles;
	std::vector<int> SlopeFilesSW;

	std::vector<int> SceneObjects;

	int MouseObject;
	int CliffObject, CliffExtraObject;
	int UnitPalette, TmpPalette, SnoPalette, DesPalette;
	int ShpFile;
	int roadTileFile;
	int roadObject[3];
}

bool Graphic::Direct3DInitialize(HWND hWnd, const char* pShotFileName, bool bUnion, int nDirections, int TurretOff)
{
	return SetUpScene(hWnd) && PrepareVertexBuffer(pShotFileName, bUnion, nDirections, TurretOff);
}

void Graphic::Direct3DUninitialize()
{
	return ClearSceneObjects();
}

bool Graphic::PrepareVertexBuffer(const char* pShotFileName, bool bUnion, int nDirections, int TurretOff)
{
	const float TileLength = 30.0f*sqrt(2.0);
	const float CellHeight = 10.0f * sqrt(3.0);

	UnitPalette = CreatePaletteFile("palettes\\unittem.pal");
	TmpPalette = CreatePaletteFile("palettes\\isotem.pal");
	SnoPalette = CreatePaletteFile("palettes\\isosno.pal");
	DesPalette = CreatePaletteFile("palettes\\isodes.pal");

	SetBackgroundColor(0, 0, 0);

	if (!UnitPalette || !TmpPalette || !SnoPalette || !DesPalette)
		return false;

	//MakeVxlFrameShot(VxlFiles[0], "Shot.png", 0, 0.0, 0.0, 0.0, UnitPalette, INVALID_COLOR_VALUE);
	if (pShotFileName) {
		MakeShots(pShotFileName, 0.0, UnitPalette, bUnion, nDirections, INVALID_COLOR_VALUE, TurretOff);
	}

	if (roadTileFile = CreateTmpFile("tile\\proad01a.tem")) {
		LoadTmpTextures(roadTileFile, TmpPalette);
	}

	if (auto id = CreateVxlFile("flata.vxl")) {
		VxlFiles.push_back(id);
	}

	if (auto id = CreateVxlFile("flata___.vxl")) {
		VxlFiles.push_back(id);
	}
/*
	if (auto id = CreateCircularCommonTextureFile(280.0,4.0,D3DCOLOR_XRGB(242,0,242))) {
		CreateCommonTextureObjectAtScene(id, { 0.0,0.0,0.0 });
	}
	*/
	if (ShpFile = CreateShpFile("images\\ggcnst.shp")) {
		if (LoadShpTextures(ShpFile, UnitPalette, RGB(0, 252, 252))) {
			MouseObject = CreateShpObjectAtScene(ShpFile, { 0.0,0.0,0.1f }, 0, UnitPalette, RGB(0, 252, 252), 2, 4, 4, 8, false);
			CreateShpObjectAtScene(ShpFile, { 0.0,0.0,0.1f }, 4, UnitPalette, RGB(0, 252, 252), 1, 4, 4, 8, true);
		}
	}

	D3DXVECTOR3 Position{ 100.0f,0.0f,0.0f };
	int baseid, turid, barlid;

	char cIndex = 'a';
	char szFileName[MAX_PATH];
	for (int i = 0; i < 8; i++) {
		sprintf_s(szFileName, "Tile\\clear01%c.tem", cIndex + i);

		if (auto id = CreateTmpFile(szFileName)) {
			TmpFiles.push_back(id);
			LoadTmpTextures(id, TmpPalette);
			//LoadTmpTextures(id, TmpPalette);
		}
	}

	if (auto vid = CreateShpFile("images\\ygggun.shp")) {
		LoadShpTextures(vid, UnitPalette, INVALID_COLOR_VALUE);
		if (auto tid = CreateVxlFile("images\\yaggun.vxl")) {
			float turretY = 15.0f;
			float delta = turretY*2.0f / sqrt(3.0);
			CreateShpObjectAtScene(vid, Position, 0, UnitPalette, INVALID_COLOR_VALUE, 1, 1, 1, 4, false);
			Position.z -= delta;
			CreateVxlObjectAtScene(tid, Position, 0, 0, D3DX_PI, UnitPalette, INVALID_COLOR_VALUE);
			
		}
	}

	for (int i = 0; i < 4; i++) {
		sprintf_s(szFileName, "Tile\\rmpfx12%c.tem", cIndex + i);

		if (auto id = CreateTmpFile(szFileName)) {
			SlopeFilesSW.push_back(id);
			LoadTmpTextures(id, TmpPalette);
		}
	}
/*
	if (auto id = CreateTmpFile("cliff05.tem")) {
		LoadTmpTextures(id, TmpPalette);
		CreateTmpObjectAtScene(id, { 0.0,3.0f*TileLength,0.0 }, 2, CliffObject, CliffExtraObject);
		SetObjectColorCoefficient(CliffExtraObject, { 1.0f,0.6f,1.0f,0.3f });
	}
*/
	//SetColorScheme(GetCurrentTheater(), 8848, RGB(0, 252, 0));
	if (VxlFiles.size() >= 2)
	{
		if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette, RGB(0, 252, 0))) {
			SceneObjects.push_back(vxlid);
			SetObjectColorCoefficient(vxlid, { 1.0f,0.6f,0.6f,1.0f });
		}

		if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[1], { 0.0,0.0,0.0 }, 0.0, 0.0, D3DX_PI / 2.0f, UnitPalette, RGB(0, 0, 252))) {
			SceneObjects.push_back(vxlid);
			SetObjectColorCoefficient(vxlid, { 0.6f,1.0f,0.6f,0.2f });
		}
	}

/*
	if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette,RGB(252,0,0))) {
		MouseObject = vxlid;
	}
*/
	int idxTile, idxExtra;

	if (!TmpFiles.empty()) {
		for (int x = -4; x < 6; x++) {
			for (int y = -4; y < 6; y++) {
				auto RamdomIndex = Randomizer::RandomRanged(0, TmpFiles.size());
				auto Position = D3DXVECTOR3((-0.5 + x)*TileLength, (-0.5 + y)*TileLength, 0.0f);

				if (y >= 0 && y < 3) {
					roadObject[y] = CellClass::CreateCellAt(Position, roadTileFile, y);
					continue;
				}

				if (CellClass::CreateCellAt(Position, TmpFiles[RamdomIndex], 0)) {
				}
				else {
					printf_s("failed to draw.\n");
				}
			}
		}
	}
/*
	for (auto tiles : TmpFiles) {
		LoadTmpTextures(tiles, TmpPalette);
	}
*/
	int out;
	if (auto fid = CreateTmpFile("tile\\grdrl10.des"))
	{
		LoadTmpTextures(fid, DesPalette);
		CreateTmpObjectAtScene(fid, { 0.0f,7.5f*TileLength,0.0f }, 0, out, out);
	}

	if (auto fid = CreateTmpFile("tile\\cliffz04.sno"))
	{
		LoadTmpTextures(fid, SnoPalette);
		CreateTmpObjectAtScene(fid, { 0.0f,8.5f*TileLength,0.0f }, 2, out, out);
	}

	if (!TmpFiles.empty() && !SlopeFilesSW.empty())
	for (int x = 0; x < 10; x++) {
		if (CellClass::CreateCellAt(
			{ (-4.5f + x)*TileLength,-6.5f*TileLength,CellHeight },
			SlopeFilesSW[Randomizer::RandomRanged(0, SlopeFilesSW.size())],
			0)) {
		}
		if (CellClass::CreateCellAt({ (-4.5f + x)*TileLength,-5.5f*TileLength,0.0 },
			SlopeFilesSW[Randomizer::RandomRanged(0, SlopeFilesSW.size())],
			0)) {
		}
	}

	//RemoveObjectFromScene(SceneObjects[1]);
	//SceneObjects.erase(SceneObjects.begin() + 1);
	return true;
}

void Graphic::DrawScene()
{
	PresentAllObject();
}

void Graphic::WorldRotation()
{
	if (SceneObjects.size() >= 2) {
		RotateObject(SceneObjects[0], 0.0, 0.0, 0.01);
		MoveObject(SceneObjects[1], { 0.0,0.05f,0.0 });
	}
}

bool Graphic::PrepareTileGround()
{
	return false;
}

bool Graphic::DrawTileGround()
{
	return false;
}

void Graphic::ClearScene()
{
	return ClearSceneObjects();
}

bool Graphic::HandleDeviceLost()
{
	return SceneClass::Instance.HandleDeviceLost();
}

void Graphic::InitliazeDeviceState()
{
	SceneClass::Instance.InitializeDeviceState();
}

void Graphic::ResetDevice()
{
	if (!ResetSceneView())
		printf_s("failed to handle.\n");
}

void Graphic::SetCamera()
{
	SceneClass::Instance.SetUpCamera();
}

void Graphic::MoveFocus(float x, float y)
{
	MoveFocusOnScreen(x, y);
}

void Graphic::PlaceVXL(POINT Position)
{
	static float StaticAngle = 0.0;
	const float TileLength = 30.0f*sqrt(2.0);
	const float CellHeight = 30.0f / sqrt(3.0);

	if (VxlFiles.size()) {
		D3DXVECTOR3 ScenePositon;

		ClientPositionToScenePosition(Position, ScenePositon);

		if (auto id = CreateVxlObjectAtScene(VxlFiles[0], ScenePositon, 0.0, 0.0, StaticAngle, UnitPalette,RGB(252,0,252))) {
			//RotateObject(id, -atan(CellHeight / TileLength), 0.0, 0.0);
			SceneObjects.push_back(id);
		}

		StaticAngle += 0.2;
	}
}

void Graphic::MouseMove(POINT Position)
{
	if (!MouseObject)
		return;

	D3DXVECTOR3 TargetPosition;

	ClientPositionToScenePosition(Position, TargetPosition);
	//TargetPosition.z += 0.1f;

	//SetObjectLocation(MouseObject, TargetPosition);

	CellClass::MarkCellByMousePosition(Position);
}

void Graphic::RemoveLastTmp()
{
	RemoveTmpFile(TmpFiles.back());
	TmpFiles.erase(TmpFiles.end() - 1);
	RemoveShpFile(ShpFile);
	RemoveVxlFile(VxlFiles[1]);
	//VxlFiles.erase(VxlFiles.begin() + 1);
}

void Graphic::SceneRotation()
{
	RotateWorld(0.05f);
}
