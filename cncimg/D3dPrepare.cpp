#include "D3dPrepare.h"

#include "CncImageAPI.h"

namespace Graphic
{
	std::vector<int> VxlFiles;
	std::vector<int> TmpFiles;
	std::vector<int> SlopeFiles;

	std::vector<int> SceneObjects;

	int MouseObject;
	int CliffObject, CliffExtraObject;
	int UnitPalette, TmpPalette;
	int ShpFile;
}

bool Graphic::Direct3DInitialize(HWND hWnd)
{
	return SetUpScene(hWnd) && PrepareVertexBuffer();
}

void Graphic::Direct3DUninitialize()
{
	return ClearSceneObjects();
}

bool Graphic::PrepareVertexBuffer()
{
	const float TileLength = 30.0f*sqrt(2.0);
	const float CellHeight = 30.0f / sqrt(3.0);

	UnitPalette = CreatePaletteFile("palettes\\unittem.pal");
	TmpPalette = CreatePaletteFile("palettes\\isotem.pal");

	if (!UnitPalette || !TmpPalette)
		return false;

	if (auto id = CreateVxlFile("flata.vxl")) {
		VxlFiles.push_back(id);
	}

	if (auto id = CreateVxlFile("flata___.vxl")) {
		VxlFiles.push_back(id);
	}

	if (ShpFile = CreateShpFile("images\\ggcnst.shp")) {
		if (LoadShpTextures(ShpFile, UnitPalette, RGB(0, 252, 252)))
			MouseObject = CreateShpObjectAtScene(ShpFile, { 0.0,0.0,0.0 }, 0, UnitPalette, RGB(0, 252, 252), false);
	}

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

	for (int i = 0; i < 4; i++) {
		sprintf_s(szFileName, "Tile\\rmpfx12%c.tem", cIndex + i);

		if (auto id = CreateTmpFile(szFileName)) {
			SlopeFiles.push_back(id);
			LoadTmpTextures(id, TmpPalette);
		}
	}

	if (auto id = CreateTmpFile("cliff05.tem")) {
		LoadTmpTextures(id, TmpPalette);
		CreateTmpObjectAtScene(id, { 0.0,3.0f*TileLength,0.0 }, 2, CliffObject, CliffExtraObject);
	}

	//SetColorScheme(GetCurrentTheater(), 8848, RGB(0, 252, 0));
	if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette,RGB(0,252,0))) {
		SceneObjects.push_back(vxlid);
	}

	if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[1], { 0.0,0.0,0.0 }, 0.0, 0.0, D3DX_PI / 2.0f, UnitPalette,RGB(0,0,252))) {
		SceneObjects.push_back(vxlid);
	}

	MakeVxlFrameShot(VxlFiles[0], 0, 0.0, 0.0, 0.0, UnitPalette, RGB(0, 252, 252));
/*
	if (auto vxlid = CreateVxlObjectAtScene(VxlFiles[0], { 0.0,0.0,0.0 }, 0.0, 0.0, 0.0, UnitPalette,RGB(252,0,0))) {
		MouseObject = vxlid;
	}
*/
	int idxTile, idxExtra;

	if (!TmpFiles.empty()) {
		for (int x = -100; x < 100; x++) {
			for (int y = -100; y < 100; y++) {
				auto RamdomIndex = Randomizer::RandomRanged(0, TmpFiles.size());
				if (CreateTmpObjectAtScene(TmpFiles[RamdomIndex],
					D3DXVECTOR3((-0.5 + x)*TileLength, (-0.5 + y)*TileLength, 0.0f), 0, idxTile, idxExtra)) {
					if (idxTile)
						SceneObjects.push_back(idxTile);
					if (idxExtra)
						SceneObjects.push_back(idxExtra);
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
	if (!TmpFiles.empty() && !SlopeFiles.empty())
	for (int x = 0; x < 10; x++) {
		if (CreateTmpObjectAtScene(SlopeFiles[Randomizer::RandomRanged(0, SlopeFiles.size())],
		{ (-4.5f + x)*TileLength,-5.5f*TileLength,0.0 }, 0, idxTile, idxExtra)) {
			if (idxTile)
				SceneObjects.push_back(idxTile);
			if (idxExtra)
				SceneObjects.push_back(idxExtra);
		}
		if (CreateTmpObjectAtScene(TmpFiles[Randomizer::RandomRanged(0, TmpFiles.size())],
		{ (-4.5f + x)*TileLength,-6.5f*TileLength,CellHeight }, 0, idxTile, idxExtra)) {
			if (idxTile)
				SceneObjects.push_back(idxTile);
			if (idxExtra)
				SceneObjects.push_back(idxExtra);
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
	SetObjectLocation(MouseObject, TargetPosition);
}

void Graphic::RemoveLastTmp()
{
	RemoveTmpFile(TmpFiles.back());
	TmpFiles.erase(TmpFiles.end() - 1);
	RemoveShpFile(ShpFile);
	RemoveVxlFile(VxlFiles[1]);
	//VxlFiles.erase(VxlFiles.begin() + 1);
}
