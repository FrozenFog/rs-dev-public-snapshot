#pragma once

#include "CncImageAPI.h"

#include <Windows.h>
#include <memory>

enum class CellCrossType : int{
	Up, Down, None, Other
};

class CellClass
{
public:
	static std::unordered_map<DWORD, std::unique_ptr<CellClass>>MapCells;

	static CellClass* FindCellByCoords(D3DXVECTOR3 Coords);
	static void RemoveCellFromScene(D3DXVECTOR3 Coords);
	static bool CreateCellAt(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex);
	static void MarkCellByMousePosition(POINT MousePosition);
	static DWORD TransformCoords(D3DXVECTOR3 Position);

	CellClass();
	~CellClass();
	CellClass(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex);

	bool SpawnAtMapCoords(D3DXVECTOR3 Position, int nTileFileID, int nTileIndex);
	void Mark();
	void Unmark();
	CellCrossType TestCrossType(D3DXVECTOR3 MousePosition);
	float GetCellHeight();

private:
	static CellClass* LastMaskedCell;

	D3DXVECTOR3 Position;
	int nImageId, nExtraId;
};