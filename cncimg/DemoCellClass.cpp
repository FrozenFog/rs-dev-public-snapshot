#include "DemoCellClass.h"

std::unordered_map<DWORD, std::unique_ptr<CellClass>> CellClass::MapCells;
CellClass* CellClass::LastMaskedCell = nullptr;

CellClass* CellClass::FindCellByCoords(D3DXVECTOR3 Coords)
{
	auto Find = MapCells.find(TransformCoords(Coords));
	if (Find != MapCells.end())
		return Find->second.get();
	return nullptr;
}

void CellClass::RemoveCellFromScene(D3DXVECTOR3 Coords)
{
	MapCells.erase(TransformCoords(Coords));
}

bool CellClass::CreateCellAt(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex)
{
	auto pCell = std::make_unique<CellClass>(Position, nTileFileID, nTileIndex);
	if (pCell) {
		MapCells[TransformCoords(Position)] = std::move(pCell);
		return true;
	}
	return false;
}

void CellClass::MarkCellByMousePosition(POINT MousePosition)
{
	const int nMaxHeight = 14;
	const float Len = 30.0*sqrt(2.0);
	const float Hi = 20.0*sqrt(3.0);

	CellClass* pCellToTest = nullptr;
	D3DXVECTOR3 Position;

	ClientPositionToScenePosition(MousePosition, Position);
	for (int i = 0; i < nMaxHeight; i++)
	{
		auto pTargetCell = FindCellByCoords(Position);
		auto pTarget2 = FindCellByCoords(Position + D3DXVECTOR3(-Len, 0.0, 0.0));
		auto pTarget3 = FindCellByCoords(Position + D3DXVECTOR3(0.0, -Len, 0.0));

		if (pTarget2 && pTarget2->TestCrossType(Position) != CellCrossType::None)
			pCellToTest = pTarget2;

		if (pTarget3 && pTarget3->TestCrossType(Position) != CellCrossType::None)
			pCellToTest = pTarget3;

		if (pTargetCell && pTargetCell->TestCrossType(Position) != CellCrossType::None)
			pCellToTest = pTargetCell;

		Position += D3DXVECTOR3(Len, Len, Hi);
	}

	if (pCellToTest)
	{
		if (LastMaskedCell)
			LastMaskedCell->Unmark();

		pCellToTest->Mark();
		LastMaskedCell = pCellToTest;
	}
}

DWORD CellClass::TransformCoords(D3DXVECTOR3 Position)
{
	const FLOAT TileLength = 30 * sqrt(2.0);

	short xCoords = std::floor((Position.x + 1.0) / TileLength);
	short yCoords = std::floor((Position.y + 1.0) / TileLength);

	return MAKELONG(xCoords, yCoords);
}

CellClass::CellClass() :Position({ 0.0f,0.0f,0.0f }),
	nImageId(0),
	nExtraId(0)
{}

CellClass::~CellClass()
{
	RemoveObjectFromScene(nImageId);
	RemoveObjectFromScene(nExtraId);
	printf_s("Cell at %f, %f, %f has been erased.\n", Position.x, Position.y, Position.z);
}

CellClass::CellClass(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex) : CellClass()
{
	SpawnAtMapCoords(Position, nTileFileID, nTileIndex);
}

bool CellClass::SpawnAtMapCoords(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex)
{
	this->Position = Position;
	CreateTmpObjectAtScene(nTileFileID, Position, nTileIndex, this->nImageId, this->nExtraId);

	if (this->nImageId || this->nExtraId) {
		return true;
	}
	return false;
}

void CellClass::Mark()
{
	D3DXVECTOR4 Coefficient = D3DXVECTOR4(1.0, 0.0, 0.0, 1.0);
	SetObjectColorCoefficient(nImageId, Coefficient);
	SetObjectColorCoefficient(nExtraId, Coefficient);
}

void CellClass::Unmark()
{
	D3DXVECTOR4 Coefficient = D3DXVECTOR4(1.0, 1.0, 1.0, 1.0);
	SetObjectColorCoefficient(nImageId, Coefficient);
	SetObjectColorCoefficient(nExtraId, Coefficient);
}

CellCrossType CellClass::TestCrossType(D3DXVECTOR3 MousePosition)
{
	const float Half = 15 * sqrt(2.0);

	float dHeight = GetCellHeight() - MousePosition.z;
	float dPosition = dHeight*sqrt(3.0 / 2.0);

	MousePosition.z = GetCellHeight();
	MousePosition.x += dPosition;
	MousePosition.y += dPosition;

	if ((MousePosition.x < Position.x - Half || MousePosition.x >= Position.x + Half) ||
		(MousePosition.y < Position.y - Half || MousePosition.y >= Position.y + Half))
		return CellCrossType::None;
	else if (MousePosition.x >= Position.x &&  MousePosition.y >= Position.y)
		return CellCrossType::Down;
	else if (MousePosition.x < Position.x && MousePosition.y < Position.y)
		return CellCrossType::Up;
	return CellCrossType::Other;
}

float CellClass::GetCellHeight()
{
	return Position.z;
}
