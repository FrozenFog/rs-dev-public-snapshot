#include "VertexFormats.h"
#include "normals.h"
#include "VxlFile.h"
#include "ColorScheme.h"

#include "SceneClass.h"

#define SAFE_DELETE_ARRAY(arr) if(arr)delete[] arr, arr=nullptr;

Palette VxlFile::RANormals("Normals\\RANormals.pal");
Palette VxlFile::TSNormals("Normals\\TSNormals.pal");

D3DXVECTOR3 VxlFile::LightReversed = { 0.0,1.0,0.0 };

std::unordered_map<int, std::unique_ptr<VxlFile>> VxlFile::FileObjectTable;

SpanData::SpanData() : nVoxels(0)
{
}

//加载一条像素条 SpanData::pVoxels需要已经分配nZSize个像素
void SpanData::LoadSpanDataFromBuffer(PBYTE pBuffer, PBYTE pBufferEnd)
{
	auto pVoxels = this->Voxels.data();
	BYTE nSkipCount, nVoxels, nVoxelsEnd;

	do
	{
		nSkipCount = *pBuffer++;
		nVoxels = *pBuffer++;
		pVoxels += nSkipCount;

		if (nVoxels)
		{
			memcpy_s(pVoxels, nVoxels * sizeof Voxel, pBuffer, nVoxels * sizeof Voxel);
			pBuffer += nVoxels * sizeof Voxel;
		}

		pVoxels += nVoxels;
		nVoxelsEnd = *pBuffer++;

		if (nVoxels != nVoxelsEnd)
		{
			printf_s("the file format may be wrong.\n");
		}

	} while (pBuffer <= pBufferEnd);
}

VxlLimbBody::VxlLimbBody()
{
}

VxlLimbBody::~VxlLimbBody()
{
	this->Clear();
}

void VxlLimbBody::Clear()
{
	this->SpanData.clear();
	this->SpanEnds.clear();
	this->SpanStarts.clear();
}

//pBuffer pointed to the body
void VxlLimbBody::LoadFromBuffer(PBYTE pBuffer, VxlLimbTailer& TailerInfo)
{
	this->Clear();

	auto nSpans = TailerInfo.nXSize*TailerInfo.nYSize;
	this->SpanStarts.resize(nSpans);
	this->SpanEnds.resize(nSpans);
	this->SpanData.resize(nSpans);

	auto pSpanDataStart = pBuffer + TailerInfo.nSpanDataOffset;
	auto pSpanStartData = pBuffer + TailerInfo.nSpanStartOffset;
	auto pSpanEndData = pBuffer + TailerInfo.nSpanEndOffset;

	memcpy_s(this->SpanStarts.data(), nSpans * sizeof INT, pSpanStartData, nSpans * sizeof INT);
	memcpy_s(this->SpanEnds.data(), nSpans * sizeof INT, pSpanEndData, nSpans * sizeof INT);

	for (int i = 0; i < nSpans; i++)
	{
		auto& CurrentSpan = this->SpanData[i];
		CurrentSpan.nVoxels = 0;
		CurrentSpan.Voxels.resize(TailerInfo.nZSize);

		if (this->SpanStarts[i] == -1 || this->SpanEnds[i] == -1)
			continue;

		auto pCurrentSpanStart = pSpanDataStart + this->SpanStarts[i];
		auto pCurrentSpanEnd = pSpanDataStart + this->SpanEnds[i];
		CurrentSpan.LoadSpanDataFromBuffer(pCurrentSpanStart, pCurrentSpanEnd);
		for (int z = 0; z < TailerInfo.nZSize; z++)
		{
			CurrentSpan.nVoxels += CurrentSpan.Voxels[z].nColor ? 1 : 0;
		}
	}
}


/*
* The FixedRead passed in MUST be currently sought to the start of all the
* limb body data
*/

Voxel::Voxel() :nColor(0), nNormal(0)
{
}

void VxlFileHeader::LoadFromBuffer(PBYTE pBuffer)
{
	memcpy_s(this, sizeof *this, pBuffer, sizeof *this);
}

void VxlLimbHeader::LoadFromBuffer(PBYTE pBuffer)
{
	memcpy_s(this, sizeof *this, pBuffer, sizeof *this);
}

void VxlLimbTailer::LoadFromBuffer(PBYTE pBuffer)
{
	memcpy_s(this, sizeof *this, pBuffer, sizeof *this);
}

//clear all objects on scene
void VxlFile::ClearAllObjectForAllFile()
{
	for (auto& file : FileObjectTable) {
		if (file.second)
			file.second->ClearAllObjects();
	}
}

VxlFile::VxlFile() :pFileBuffer(nullptr), AssociatedHVA()
{
}

VxlFile::VxlFile(const char * pFileName) : VxlFile()
{
	this->LoadFromFile(pFileName);
}

VxlFile::VxlFile(const char * pFileName, const char * pPaletteFileName) : VxlFile(pFileName)
{
	//this->LoadFromFile(pFileName);
	this->LoadPalette(pPaletteFileName);
}

VxlFile::VxlFile(LPVOID pFileBuffer, ULONG nSize, LPVOID pHVABuffer, ULONG nHVASize, bool bCopy, bool bHVACopy) :VxlFile()
{
	this->LoadFromFileInBuffer(pFileBuffer, nSize, pHVABuffer, nHVASize, bCopy, bHVACopy);
}

VxlFile::~VxlFile()
{
	this->Clear();
}

void VxlFile::Clear()
{
	if (!this->IsLoaded())
		return;

	this->BodyData.clear();
	this->LimbHeaders.clear();
	this->LimbTailers.clear();

	SAFE_DELETE_ARRAY(this->pFileBuffer);
}

void VxlFile::LoadFromFile(const char * pFileName)
{
	auto hFile = CreateFile(pFileName, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return;

	auto nFileSize = GetFileSize(hFile, nullptr);

	if (nFileSize == NULL)
		goto Failure;

	this->pFileBuffer = new BYTE[nFileSize];
	ULONG nReadBytes;
	if (!ReadFile(hFile, this->pFileBuffer, nFileSize, &nReadBytes, nullptr) || nReadBytes != nFileSize)
		goto Failure;

	auto pBuffer = this->pFileBuffer;

	char szHvaFile[MAX_PATH];
	strcpy_s(szHvaFile, pFileName);
	auto pExtension = szHvaFile + strnlen_s(szHvaFile, sizeof szHvaFile) - sizeof ".VXL" + 1;
	strcpy(pExtension, ".HVA");

	auto hHvaFile = CreateFile(szHvaFile, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hHvaFile == INVALID_HANDLE_VALUE)
		goto Failure;

	auto nHvaSize = GetFileSize(hHvaFile, nullptr);
	auto pHvaBuffer = malloc(nHvaSize);

	if (!pHvaBuffer)
		goto Failure;

	if (!ReadFile(hHvaFile, pHvaBuffer, nHvaSize, &nReadBytes, nullptr) || nReadBytes != nHvaSize)
		goto Failure;

	this->LoadFromFileInBuffer(pBuffer, nFileSize, pHvaBuffer, nHvaSize, false, true);
	free(pHvaBuffer);
/*
	if (!this->AssociatedHVA.LoadFromFile(szHvaFile))
		goto Failure;
*/
	CloseHandle(hFile);
	CloseHandle(hHvaFile);
	return;

Failure:
	SAFE_DELETE_ARRAY(this->pFileBuffer);

	if (pHvaBuffer)
		free(pHvaBuffer);

	CloseHandle(hFile);
	CloseHandle(hHvaFile);
}

void VxlFile::LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, LPVOID pHVABuffer, ULONG nHVASize, bool bCopy, bool bHVACopy)
{
	if (!pFileBuffer || !pHVABuffer || !nSize || !nHVASize)
		return;

	if (bCopy)
	{
		this->pFileBuffer = reinterpret_cast<PBYTE>(malloc(nSize));
		if (this->pFileBuffer)
			memcpy_s(this->pFileBuffer, nSize, pFileBuffer, nSize);
		else
			return;
	}
	else
	{
		this->pFileBuffer = reinterpret_cast<PBYTE>(pFileBuffer);
	}

	auto pBuffer = reinterpret_cast<PBYTE>(pFileBuffer);

	memcpy_s(&this->FileHeader, sizeof this->FileHeader, pBuffer, sizeof this->FileHeader);

	this->FileHeader.ContainedPalette.ShiftColors();
	auto nLimbs = this->FileHeader.nNumberOfLimbs;
	this->LimbHeaders.resize(nLimbs);
	this->LimbTailers.resize(nLimbs);
	this->BodyData.resize(nLimbs);

	//load limb headers;
	pBuffer += sizeof this->FileHeader;
	memcpy_s(this->LimbHeaders.data(), nLimbs * sizeof VxlLimbHeader, pBuffer, nLimbs * sizeof VxlLimbHeader);
	//load limb tailers;
	pBuffer += nLimbs * sizeof VxlLimbHeader;
	pBuffer += this->FileHeader.nBodySize;
	memcpy_s(this->LimbTailers.data(), nLimbs * sizeof VxlLimbTailer, pBuffer, nLimbs * sizeof VxlLimbTailer);

	//load limb bodies;
	pBuffer -= this->FileHeader.nBodySize;
	for (int n = 0; n < nLimbs; n++)
	{
		this->BodyData[n].LoadFromBuffer(pBuffer, this->LimbTailers[n]);
	}

	this->AssociatedHVA.LoadFromFileInBuffer(pHVABuffer, nHVASize, bHVACopy);
}

void VxlFile::PrintInfo()
{
	printf_s("contains NumberOfLimbs : %d.\n", this->FileHeader.nNumberOfLimbs);
	for (int i = 0; i < this->FileHeader.nNumberOfLimbs; i++)
	{
		auto& Tailer = this->LimbTailers[i];
		auto& Body = this->BodyData[i];

		printf_s("Span Start off = 0x%X, End off = 0x%X, Data off = 0x%X.\n",
			Tailer.nSpanStartOffset, Tailer.nSpanEndOffset, Tailer.nSpanDataOffset);

		printf_s("Dimension = %d, %d, %d.\n", Tailer.nXSize, Tailer.nYSize, Tailer.nZSize);
		for (int x = 0; x < Tailer.nXSize; x++)
		{
			for (int y = 0; y < Tailer.nYSize; y++)
			{
				auto& Span = Body.SpanData[y*Tailer.nXSize + x];
				printf_s("Span coords x = %d, y = %d, Voxels = %d.\n", x, y, Span.Voxels.size());
			}
		}
		printf_s("About to print info for next limb:\n Press any key to continue.\n");
		getchar();
	}
}

bool VxlFile::IsLoaded()
{
	return this->pFileBuffer != nullptr && this->AssociatedHVA.IsLoaded();
}

int VxlFile::GetFrameCount()
{
	if (!this->IsLoaded() || !this->AssociatedHVA.IsLoaded())
		return 0;

	return this->AssociatedHVA.GetFrameCount();
}

bool VxlFile::GetVoxelRH(int nLimb, int x, int y, int z, Voxel & Voxel)
{
	if (!this->IsLoaded())
		return false;

	if (nLimb >= this->FileHeader.nNumberOfLimbs)
		return false;

	auto& Tailer = this->LimbTailers[nLimb];
	auto& Body = this->BodyData[nLimb];

	if (x >= Tailer.nXSize || y >= Tailer.nYSize || z >= Tailer.nZSize)
		return false;

	Voxel = Body.SpanData[y*Tailer.nXSize + x].Voxels[z];
	return true;
}

bool VxlFile::GetVoxelLH(int nLimb, int x, int y, int z, Voxel & Voxel)
{
	return this->GetVoxelRH(nLimb, y, x, z, Voxel);
}

void VxlFile::LoadPalette(const char * pPaletteName)
{
	if (this->IsLoaded())
	{
		this->FileHeader.ContainedPalette.LoadFromFile(pPaletteName);
	}
}

int VxlFile::DrawAtScene(LPDIRECT3DDEVICE9 pDevice, D3DXVECTOR3 Position,
	float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor, VPLFile& Vpl)
{
	if (!this->IsLoaded() || !pDevice)
		return 0;

	//this->RemoveFromScene(Position);

	D3DXMATRIX Matrix, Scale, RotateX, RotateY, RotateZ, Translation, Identity, NormalMatrix, Origin, TranslationCenter;
	LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPDIRECT3DBASETEXTURE9 pLastTexture;
	LPVOID pVertexData;

	std::vector<Vertex> UsedVertecies;
	std::vector<Voxel> BufferedVoxels;
	std::vector<D3DXVECTOR3> BufferedNormals;
	Voxel Buffer;
	Palette Entries;

	if (auto palette = Palette::FindPaletteByID(nPaletteID)) {
		Entries = *palette;
	}
	else
		return 0;

	Entries.MakeRemapColor(dwRemapColor);
	auto& Scene = SceneClass::Instance;

	for (int i = 0; i < this->FileHeader.nNumberOfLimbs; i++)
	{
		auto&TailerInfo = this->LimbTailers[i];

		D3DXMatrixRotationX(&RotateX, RotationX);
		D3DXMatrixRotationY(&RotateY, RotationY);
		D3DXMatrixRotationZ(&RotateZ, RotationZ);
		D3DXMatrixTranslation(&Translation, Position.x, Position.y, Position.z);
		D3DXMatrixIdentity(&Identity);

		auto MinBounds = TailerInfo.MinBounds;
		auto MaxBounds = TailerInfo.MaxBounds;

		FLOAT xScale = (MaxBounds.X - MinBounds.X) / TailerInfo.nXSize;
		FLOAT yScale = (MaxBounds.Y - MinBounds.Y) / TailerInfo.nYSize;
		FLOAT zScale = (MaxBounds.Z - MinBounds.Z) / TailerInfo.nZSize;
		D3DXMatrixScaling(&Scale, xScale, yScale, zScale);

		//Origin is multiplied before Hva
		TranslationCenter = TailerInfo.MinBounds.AsTranslationMatrix();
		Origin = TailerInfo.Matrix.AsD3dMatrix(TailerInfo.fScale);
		Matrix = this->AssociatedHVA.GetTransformMatrix(0, i)->AsD3dMatrix(TailerInfo.fScale);
		Matrix = Identity*Scale*TranslationCenter*Origin*Matrix*RotateX*RotateY*RotateZ*Translation;
		NormalMatrix = Matrix;
		
		NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;

		//this->AssociatedHVA.GetTransformMatrix(0, i)->Print();

		for (int x = 0; x < TailerInfo.nXSize; x++)
		{
			for (int y = 0; y < TailerInfo.nYSize; y++)
			{
				for (int z = 0; z < TailerInfo.nZSize; z++)
				{
					if (!this->GetVoxelRH(i, x, y, z, Buffer))
						continue;

					if (Buffer.nColor)
					{
						auto pNormalTable = NormalTableDirectory[static_cast<int>(TailerInfo.nNormalType)];

						if (!pNormalTable)
							continue;

						D3DXVECTOR3 NormalVec = pNormalTable[Buffer.nNormal];//{ (float)Normal.R - 128.0f,(float)Normal.G - 128.0f,(float)Normal.B - 128.0f };
						D3DCOLOR dwColor;
						NormalVec *= NormalMatrix;

						auto fAngle = std::acos((VxlFile::LightReversed * NormalVec) /
							D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&NormalVec));

						if (fAngle >= D3DX_PI / 2)
						{
							auto& Color = Entries[Vpl[0].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}
						else
						{
							int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
							if (nIndex > 31 || nIndex < 0) nIndex = 31; // FzF: fix 0.0f = 0x80000000h
							auto& Color = Entries[Vpl[nIndex].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}

						UsedVertecies.push_back({
							(float)x,(float)y,(float)z,
							//(float)Normal.R,(float)Normal.G,(float)Normal.B,
							dwColor });

						UsedVertecies.back().Vector *= Matrix;
						BufferedNormals.push_back(NormalVec);
						BufferedVoxels.push_back(Buffer);
					}
				}
			}
		}
	}

	if (FAILED(pDevice->CreateVertexBuffer(UsedVertecies.size() * sizeof Vertex, D3DUSAGE_DYNAMIC, Vertex::dwFVFType,
		D3DPOOL_SYSTEMMEM, &pVertexBuffer, nullptr)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}

	if (FAILED(pVertexBuffer->Lock(0, UsedVertecies.size() * sizeof Vertex, &pVertexData, NULL)))
	{
		SAFE_RELEASE(pVertexBuffer);
		return 0;
	}

	memcpy_s(pVertexData, UsedVertecies.size() * sizeof Vertex, UsedVertecies.data(), UsedVertecies.size() * sizeof Vertex);
	pVertexBuffer->Unlock();

	PaintingStruct PaintObject;

	//this->AddDrawnObject(pVertexBuffer, Position);

	PaintingStruct::InitializePaintingStruct(PaintObject, pVertexBuffer, Position, nullptr, false, &BufferedVoxels, &BufferedNormals,
		nPaletteID, dwRemapColor);
	return this->CommitOpaqueObject(PaintObject);
}

void VxlFile::MakeFrameScreenShot(LPDIRECT3DDEVICE9 pDevice, const char* pDestFile, const char* pShadow, int idxFrame, 
	float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor, VPLFile & Vpl)
{
	if (!this->IsLoaded() || !pDevice)
		return;

	if (idxFrame >= this->AssociatedHVA.GetFrameCount())
		return;

	//this->RemoveFromScene(Position);

	D3DXMATRIX Matrix, Scale, RotateX, RotateY, RotateZ, Identity, NormalMatrix, Origin, TranslationCenter;
	//LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPDIRECT3DTEXTURE9 pTexture;
	LPVOID pVertexData;

	std::vector<Vertex> UsedVertecies;
	std::vector<Voxel> BufferedVoxels;
	std::vector<D3DXVECTOR3> BufferedNormals;
	Voxel Buffer;
	Palette Entries;

	if (auto palette = Palette::FindPaletteByID(nPaletteID)) {
		Entries = *palette;
	}
	else
		return;

	Entries.MakeRemapColor(dwRemapColor);
	auto& Scene = SceneClass::Instance;

	for (int i = 0; i < this->FileHeader.nNumberOfLimbs; i++)
	{
		auto&TailerInfo = this->LimbTailers[i];

		D3DXMatrixRotationX(&RotateX, RotationX);
		D3DXMatrixRotationY(&RotateY, RotationY);
		D3DXMatrixRotationZ(&RotateZ, RotationZ);
		D3DXMatrixIdentity(&Identity);

		auto MinBounds = TailerInfo.MinBounds;
		auto MaxBounds = TailerInfo.MaxBounds;

		FLOAT xScale = (MaxBounds.X - MinBounds.X) / TailerInfo.nXSize;
		FLOAT yScale = (MaxBounds.Y - MinBounds.Y) / TailerInfo.nYSize;
		FLOAT zScale = (MaxBounds.Z - MinBounds.Z) / TailerInfo.nZSize;
		D3DXMatrixScaling(&Scale, xScale, yScale, zScale);

		//Origin is multiplied before Hva
		TranslationCenter = TailerInfo.MinBounds.AsTranslationMatrix();
		Origin = TailerInfo.Matrix.AsD3dMatrix(TailerInfo.fScale);
		Matrix = this->AssociatedHVA.GetTransformMatrix(idxFrame, i)->AsD3dMatrix(TailerInfo.fScale);
		Matrix = Identity*Scale*TranslationCenter*Origin*Matrix*RotateX*RotateY*RotateZ;
		NormalMatrix = Matrix;

		NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;

		//this->AssociatedHVA.GetTransformMatrix(0, i)->Print();

		for (int x = 0; x < TailerInfo.nXSize; x++)
		{
			for (int y = 0; y < TailerInfo.nYSize; y++)
			{
				for (int z = 0; z < TailerInfo.nZSize; z++)
				{
					if (!this->GetVoxelRH(i, x, y, z, Buffer))
						continue;

					if (Buffer.nColor)
					{
						auto pNormalTable = NormalTableDirectory[static_cast<int>(TailerInfo.nNormalType)];

						if (!pNormalTable)
							continue;

						D3DXVECTOR3 NormalVec = pNormalTable[Buffer.nNormal];//{ (float)Normal.R - 128.0f,(float)Normal.G - 128.0f,(float)Normal.B - 128.0f };
						D3DCOLOR dwColor;
						NormalVec *= NormalMatrix;

						auto fAngle = std::acos((VxlFile::LightReversed * NormalVec) /
							D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&NormalVec));

						if (fAngle >= D3DX_PI / 2)
						{
							auto& Color = Entries[Vpl[0].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}
						else
						{
							int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
							auto& Color = Entries[Vpl[nIndex].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}

						UsedVertecies.push_back({
							(float)x,(float)y,(float)z,
							//(float)Normal.R,(float)Normal.G,(float)Normal.B,
							dwColor });

						UsedVertecies.back().Vector *= Matrix;
						BufferedNormals.push_back(NormalVec);
						BufferedVoxels.push_back(Buffer);
					}
				}
			}
		}
	}

	RECT FrameRect{ 0,0,256,256 };
	auto ZBuffer = new float[256][256];
	D3DLOCKED_RECT LockedRect;

	typedef union {
		RGBQUAD Color;
		DWORD dwColor;
	}ColorUnion;

	if (!ZBuffer)
		return;

	if (FAILED(pDevice->CreateTexture(256, 256, 1, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pTexture, nullptr)))
		return;

	if (FAILED(pTexture->LockRect(0, &LockedRect, &FrameRect, D3DLOCK_DISCARD)))
		return;

	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			ZBuffer[i][j] = 1.0;

	auto pTextureColors = reinterpret_cast<DWORD(*)[256]>(LockedRect.pBits);
	auto BaseColor = Entries[0];
	auto dwBaseColor = D3DCOLOR_XRGB(BaseColor.R, BaseColor.G, BaseColor.B);
	auto dwShadowColor = D3DCOLOR_XRGB(Entries[1].R, Entries[1].G, Entries[1].B);

	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			pTextureColors[i][j] = dwBaseColor;

	auto AlphaBlend = [](ColorUnion& src, ColorUnion& dst, double a) {
		dst.Color.rgbBlue = src.Color.rgbBlue*a + dst.Color.rgbBlue*(1.0 - a);
		dst.Color.rgbGreen = src.Color.rgbGreen*a + dst.Color.rgbGreen*(1.0 - a);
		dst.Color.rgbRed = src.Color.rgbRed*a + dst.Color.rgbRed*(1.0 - a);
	};

	for (auto vertex : UsedVertecies)
	{
		auto ScreenPos = SceneClass::FructumTransformation(FrameRect, vertex.Vector);

		int x = ScreenPos.x, y = ScreenPos.y;
		float sx = ScreenPos.x, sy = ScreenPos.y;

		if (ScreenPos.z < ZBuffer[y][x] && ScreenPos.z > 0.0)
		{
			ZBuffer[y][x] = ScreenPos.z;

			if (pTextureColors[y][x] == dwBaseColor) {
				pTextureColors[y][x] = vertex.dwColor;
				continue;
			}

			auto dx = sx - x, dy = sy - y;
			auto lefttop = (1.0 - dx)*(1.0 - dy);
			auto righttop = dx*(1.0 - dy);
			auto leftbottom = dy*(1.0 - dx);
			auto rightbottom = dx*dy;

			ColorUnion src, dst;
			src.dwColor = vertex.dwColor;

			dst.dwColor = pTextureColors[y][x];
			AlphaBlend(src, dst, lefttop);
			pTextureColors[y][x] = dst.dwColor;

			if (pTextureColors[y][x + 1] != dwBaseColor) {
				dst.dwColor = pTextureColors[y][x + 1];
				AlphaBlend(src, dst, righttop);
				pTextureColors[y][x + 1] = dst.dwColor;
			}

			if (pTextureColors[y + 1][x] != dwBaseColor) {
				dst.dwColor = pTextureColors[y + 1][x];
				AlphaBlend(src, dst, leftbottom);
				pTextureColors[y + 1][x] = dst.dwColor;
			}

			if (pTextureColors[y + 1][x + 1] != dwBaseColor) {
				dst.dwColor = pTextureColors[y + 1][x + 1];
				AlphaBlend(src, dst, rightbottom);
				pTextureColors[y + 1][x + 1] = dst.dwColor;
			}
		}
	}

	pTexture->UnlockRect(0);
	D3DXSaveTextureToFile(pDestFile, D3DXIFF_PNG, pTexture, nullptr);

	if (FAILED(pTexture->LockRect(0, &LockedRect, &FrameRect, D3DLOCK_DISCARD)))
		return;

	pTextureColors = reinterpret_cast<DWORD(*)[256]>(LockedRect.pBits);
	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			pTextureColors[i][j] = dwBaseColor;
	for (auto vertex : UsedVertecies)
	{
		D3DXVECTOR3 ShadowPos = vertex.Vector;
		ShadowPos.z = 0.0;

		auto ScreenPos = SceneClass::FructumTransformation(FrameRect, ShadowPos);
		int x = ScreenPos.x;
		int y = ScreenPos.y;

		pTextureColors[y][x] = dwShadowColor;
	}

	pTexture->UnlockRect(0);
	D3DXSaveTextureToFile(pShadow, D3DXIFF_PNG, pTexture, nullptr);

	pTexture->Release();
	delete[] ZBuffer;
}

void VxlFile::MakeBarlTurScreenShot(LPDIRECT3DDEVICE9 pDevice, VxlFile * Barl, VxlFile* Body, const char * pDestFile, const char* pShadow,
	int idxFrame, float RotationX, float RotationY, float RotationZ, int nPaletteID, DWORD dwRemapColor, int TurretOff, VPLFile & Vpl)
{
	if (!this->IsLoaded() || !pDevice)
		return;

	if (idxFrame >= this->AssociatedHVA.GetFrameCount())
		return;

	//this->RemoveFromScene(Position);

	D3DXMATRIX Matrix, Offset, Scale, RotateX, RotateY, RotateZ, Identity, NormalMatrix, Origin, TranslationCenter;
	//LPDIRECT3DVERTEXBUFFER9 pVertexBuffer;
	LPDIRECT3DTEXTURE9 pTexture;
	LPVOID pVertexData;

	std::vector<Vertex> UsedVertecies;
	std::vector<Voxel> BufferedVoxels;
	std::vector<D3DXVECTOR3> BufferedNormals;
	Voxel Buffer;
	Palette Entries;

	if (auto palette = Palette::FindPaletteByID(nPaletteID)) {
		Entries = *palette;
	}
	else
		return;

	Entries.MakeRemapColor(dwRemapColor);
	auto& Scene = SceneClass::Instance;

	for (int i = 0; i < this->FileHeader.nNumberOfLimbs; i++)
	{
		auto&TailerInfo = this->LimbTailers[i];

		D3DXMatrixTranslation(&Offset, TurretOff*30.0*sqrt(2.0) / 256.0, 0.0, 0.0);
		D3DXMatrixRotationX(&RotateX, RotationX);
		D3DXMatrixRotationY(&RotateY, RotationY);
		D3DXMatrixRotationZ(&RotateZ, RotationZ);
		D3DXMatrixIdentity(&Identity);

		auto MinBounds = TailerInfo.MinBounds;
		auto MaxBounds = TailerInfo.MaxBounds;

		FLOAT xScale = (MaxBounds.X - MinBounds.X) / TailerInfo.nXSize;
		FLOAT yScale = (MaxBounds.Y - MinBounds.Y) / TailerInfo.nYSize;
		FLOAT zScale = (MaxBounds.Z - MinBounds.Z) / TailerInfo.nZSize;
		D3DXMatrixScaling(&Scale, xScale, yScale, zScale);

		//Origin is multiplied before Hva
		TranslationCenter = TailerInfo.MinBounds.AsTranslationMatrix();
		Origin = TailerInfo.Matrix.AsD3dMatrix(TailerInfo.fScale);
		Matrix = this->AssociatedHVA.GetTransformMatrix(idxFrame, i)->AsD3dMatrix(TailerInfo.fScale);
		Matrix = Identity*Scale*TranslationCenter*Origin*Matrix*Offset*RotateX*RotateY*RotateZ;
		NormalMatrix = Matrix;

		NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;

		//this->AssociatedHVA.GetTransformMatrix(0, i)->Print();

		for (int x = 0; x < TailerInfo.nXSize; x++)
		{
			for (int y = 0; y < TailerInfo.nYSize; y++)
			{
				for (int z = 0; z < TailerInfo.nZSize; z++)
				{
					if (!this->GetVoxelRH(i, x, y, z, Buffer))
						continue;

					if (Buffer.nColor)
					{
						auto pNormalTable = NormalTableDirectory[static_cast<int>(TailerInfo.nNormalType)];

						if (!pNormalTable)
							continue;

						D3DXVECTOR3 NormalVec = pNormalTable[Buffer.nNormal];//{ (float)Normal.R - 128.0f,(float)Normal.G - 128.0f,(float)Normal.B - 128.0f };
						D3DCOLOR dwColor;
						NormalVec *= NormalMatrix;

						auto fAngle = std::acos((VxlFile::LightReversed * NormalVec) /
							D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&NormalVec));

						if (fAngle >= D3DX_PI / 2)
						{
							auto& Color = Entries[Vpl[0].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}
						else
						{
							int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
							if (nIndex > 31 || nIndex < 0) nIndex = 31; // FzF: fix 0.0f = 0x80000000h
							auto& Color = Entries[Vpl[nIndex].Table[Buffer.nColor]];
							dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
						}

						UsedVertecies.push_back({
							(float)x,(float)y,(float)z,
							//(float)Normal.R,(float)Normal.G,(float)Normal.B,
							dwColor });

						UsedVertecies.back().Vector *= Matrix;
						BufferedNormals.push_back(NormalVec);
						BufferedVoxels.push_back(Buffer);
					}
				}
			}
		}
	}

	if (Barl && Barl->IsLoaded())
	{
		for (int i = 0; i < Barl->FileHeader.nNumberOfLimbs; i++)
		{
			auto&TailerInfo = Barl->LimbTailers[i];

			D3DXMatrixTranslation(&Offset, TurretOff*30.0*sqrt(2.0) / 256.0, 0.0, 0.0);
			D3DXMatrixRotationX(&RotateX, RotationX);
			D3DXMatrixRotationY(&RotateY, RotationY);
			D3DXMatrixRotationZ(&RotateZ, RotationZ);
			D3DXMatrixIdentity(&Identity);

			auto MinBounds = TailerInfo.MinBounds;
			auto MaxBounds = TailerInfo.MaxBounds;

			FLOAT xScale = (MaxBounds.X - MinBounds.X) / TailerInfo.nXSize;
			FLOAT yScale = (MaxBounds.Y - MinBounds.Y) / TailerInfo.nYSize;
			FLOAT zScale = (MaxBounds.Z - MinBounds.Z) / TailerInfo.nZSize;
			D3DXMatrixScaling(&Scale, xScale, yScale, zScale);

			//Origin is multiplied before Hva
			TranslationCenter = TailerInfo.MinBounds.AsTranslationMatrix();
			Origin = TailerInfo.Matrix.AsD3dMatrix(TailerInfo.fScale);
			Matrix = Barl->AssociatedHVA.GetTransformMatrix(0, i)->AsD3dMatrix(TailerInfo.fScale);
			Matrix = Identity*Scale*TranslationCenter*Origin*Matrix*Offset*RotateX*RotateY*RotateZ;
			NormalMatrix = Matrix;

			NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;

			//this->AssociatedHVA.GetTransformMatrix(0, i)->Print();

			for (int x = 0; x < TailerInfo.nXSize; x++)
			{
				for (int y = 0; y < TailerInfo.nYSize; y++)
				{
					for (int z = 0; z < TailerInfo.nZSize; z++)
					{
						if (!Barl->GetVoxelRH(i, x, y, z, Buffer))
							continue;

						if (Buffer.nColor)
						{
							auto pNormalTable = NormalTableDirectory[static_cast<int>(TailerInfo.nNormalType)];

							if (!pNormalTable)
								continue;

							D3DXVECTOR3 NormalVec = pNormalTable[Buffer.nNormal];//{ (float)Normal.R - 128.0f,(float)Normal.G - 128.0f,(float)Normal.B - 128.0f };
							D3DCOLOR dwColor;
							NormalVec *= NormalMatrix;

							auto fAngle = std::acos((VxlFile::LightReversed * NormalVec) /
								D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&NormalVec));

							if (fAngle >= D3DX_PI / 2)
							{
								auto& Color = Entries[Vpl[0].Table[Buffer.nColor]];
								dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
							}
							else
							{
								int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
								auto& Color = Entries[Vpl[nIndex].Table[Buffer.nColor]];
								dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
							}

							UsedVertecies.push_back({
								(float)x,(float)y,(float)z,
								//(float)Normal.R,(float)Normal.G,(float)Normal.B,
								dwColor });

							UsedVertecies.back().Vector *= Matrix;
							BufferedNormals.push_back(NormalVec);
							BufferedVoxels.push_back(Buffer);
						}
					}
				}
			}
		}
	}

	if (Body && Body->IsLoaded())
	{
		for (int i = 0; i < Body->FileHeader.nNumberOfLimbs; i++)
		{
			auto&TailerInfo = Body->LimbTailers[i];

			D3DXMatrixTranslation(&Offset, TurretOff*30.0*sqrt(2.0) / 256.0, 0.0, 0.0);
			D3DXMatrixRotationX(&RotateX, RotationX);
			D3DXMatrixRotationY(&RotateY, RotationY);
			D3DXMatrixRotationZ(&RotateZ, RotationZ);
			D3DXMatrixIdentity(&Identity);

			auto MinBounds = TailerInfo.MinBounds;
			auto MaxBounds = TailerInfo.MaxBounds;

			FLOAT xScale = (MaxBounds.X - MinBounds.X) / TailerInfo.nXSize;
			FLOAT yScale = (MaxBounds.Y - MinBounds.Y) / TailerInfo.nYSize;
			FLOAT zScale = (MaxBounds.Z - MinBounds.Z) / TailerInfo.nZSize;
			D3DXMatrixScaling(&Scale, xScale, yScale, zScale);

			//Origin is multiplied before Hva
			TranslationCenter = TailerInfo.MinBounds.AsTranslationMatrix();
			Origin = TailerInfo.Matrix.AsD3dMatrix(TailerInfo.fScale);
			Matrix = Body->AssociatedHVA.GetTransformMatrix(0, i)->AsD3dMatrix(TailerInfo.fScale);
			Matrix = Identity*Scale*TranslationCenter*Origin*Matrix*Offset*RotateX*RotateY*RotateZ;
			NormalMatrix = Matrix;

			NormalMatrix.m[3][0] = NormalMatrix.m[3][1] = NormalMatrix.m[3][2] = 0.0;

			//this->AssociatedHVA.GetTransformMatrix(0, i)->Print();

			for (int x = 0; x < TailerInfo.nXSize; x++)
			{
				for (int y = 0; y < TailerInfo.nYSize; y++)
				{
					for (int z = 0; z < TailerInfo.nZSize; z++)
					{
						if (!Body->GetVoxelRH(i, x, y, z, Buffer))
							continue;

						if (Buffer.nColor)
						{
							auto pNormalTable = NormalTableDirectory[static_cast<int>(TailerInfo.nNormalType)];

							if (!pNormalTable)
								continue;

							D3DXVECTOR3 NormalVec = pNormalTable[Buffer.nNormal];//{ (float)Normal.R - 128.0f,(float)Normal.G - 128.0f,(float)Normal.B - 128.0f };
							D3DCOLOR dwColor;
							NormalVec *= NormalMatrix;

							auto fAngle = std::acos((VxlFile::LightReversed * NormalVec) /
								D3DXVec3Length(&VxlFile::LightReversed) / D3DXVec3Length(&NormalVec));

							if (fAngle >= D3DX_PI / 2)
							{
								auto& Color = Entries[Vpl[0].Table[Buffer.nColor]];
								dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
							}
							else
							{
								int nIndex = 31 - int(fAngle / (D3DX_PI / 2)*32.0);
								auto& Color = Entries[Vpl[nIndex].Table[Buffer.nColor]];
								dwColor = D3DCOLOR_XRGB(Color.R, Color.G, Color.B);
							}

							UsedVertecies.push_back({
								(float)x,(float)y,(float)z,
								//(float)Normal.R,(float)Normal.G,(float)Normal.B,
								dwColor });

							UsedVertecies.back().Vector *= Matrix;
							BufferedNormals.push_back(NormalVec);
							BufferedVoxels.push_back(Buffer);
						}
					}
				}
			}
		}
	}

	RECT FrameRect{ 0,0,256,256 };
	auto ZBuffer = new float[256][256];
	D3DLOCKED_RECT LockedRect;

	typedef union {
		RGBQUAD Color;
		DWORD dwColor;
	}ColorUnion;

	if (!ZBuffer)
		return;

	if (FAILED(pDevice->CreateTexture(256, 256, 1, NULL, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, &pTexture, nullptr)))
		return;

	if (FAILED(pTexture->LockRect(0, &LockedRect, &FrameRect, D3DLOCK_DISCARD)))
		return;

	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			ZBuffer[i][j] = 1.0;

	auto pTextureColors = reinterpret_cast<DWORD(*)[256]>(LockedRect.pBits);
	auto BaseColor = Entries[0];
	auto dwBaseColor = D3DCOLOR_XRGB(BaseColor.R, BaseColor.G, BaseColor.B);
	auto dwShadowColor = D3DCOLOR_XRGB(Entries[1].R, Entries[1].G, Entries[1].B);

	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			pTextureColors[i][j] = dwBaseColor;

	auto AlphaBlend = [](ColorUnion& src, ColorUnion& dst, double a) {
		dst.Color.rgbBlue = src.Color.rgbBlue*a + dst.Color.rgbBlue*(1.0 - a);
		dst.Color.rgbGreen = src.Color.rgbGreen*a + dst.Color.rgbGreen*(1.0 - a);
		dst.Color.rgbRed = src.Color.rgbRed*a + dst.Color.rgbRed*(1.0 - a);
	};

	for (auto vertex : UsedVertecies)
	{
		auto ScreenPos = SceneClass::FructumTransformation(FrameRect, vertex.Vector);

		int x = ScreenPos.x, y = ScreenPos.y;
		float sx = ScreenPos.x, sy = ScreenPos.y;

		if (ScreenPos.z < ZBuffer[y][x] && ScreenPos.z > 0.0)
		{
			ZBuffer[y][x] = ScreenPos.z;

			if (pTextureColors[y][x] == dwBaseColor) {
				pTextureColors[y][x] = vertex.dwColor;
				continue;
			}

			auto dx = sx - x, dy = sy - y;
			auto lefttop = (1.0 - dx)*(1.0 - dy);
			auto righttop = dx*(1.0 - dy);
			auto leftbottom = dy*(1.0 - dx);
			auto rightbottom = dx*dy;

			ColorUnion src, dst;
			src.dwColor = vertex.dwColor;

			dst.dwColor = pTextureColors[y][x];
			AlphaBlend(src, dst, lefttop);
			pTextureColors[y][x] = dst.dwColor;

			if (pTextureColors[y][x + 1] != dwBaseColor) {
				dst.dwColor = pTextureColors[y][x + 1];
				AlphaBlend(src, dst, righttop);
				pTextureColors[y][x + 1] = dst.dwColor;
			}

			if (pTextureColors[y + 1][x] != dwBaseColor) {
				dst.dwColor = pTextureColors[y + 1][x];
				AlphaBlend(src, dst, leftbottom);
				pTextureColors[y + 1][x] = dst.dwColor;
			}

			if (pTextureColors[y + 1][x + 1] != dwBaseColor) {
				dst.dwColor = pTextureColors[y + 1][x + 1];
				AlphaBlend(src, dst, rightbottom);
				pTextureColors[y + 1][x + 1] = dst.dwColor;
			}
		}
	}

	pTexture->UnlockRect(0);
	D3DXSaveTextureToFile(pDestFile, D3DXIFF_PNG, pTexture, nullptr);

	if (FAILED(pTexture->LockRect(0, &LockedRect, &FrameRect, D3DLOCK_DISCARD)))
		return;

	pTextureColors = reinterpret_cast<DWORD(*)[256]>(LockedRect.pBits);
	for (int i = 0; i < 256; i++)
		for (int j = 0; j < 256; j++)
			pTextureColors[i][j] = dwBaseColor;
	for (auto vertex : UsedVertecies)
	{
		D3DXVECTOR3 ShadowPos = vertex.Vector;
		ShadowPos.z = 0.0;

		auto ScreenPos = SceneClass::FructumTransformation(FrameRect, ShadowPos);
		int x = ScreenPos.x;
		int y = ScreenPos.y;

		pTextureColors[y][x] = dwShadowColor;
	}

	pTexture->UnlockRect(0);
	D3DXSaveTextureToFile(pShadow, D3DXIFF_PNG, pTexture, nullptr);

	pTexture->Release();
	delete[] ZBuffer;
}

HVAStruct::HVAStruct() : FrameMatrices(nullptr),
	nSectionCount(0),
	nFrameCount(0)
{
}

HVAStruct::HVAStruct(const char * pFileName) : HVAStruct()
{
	this->LoadFromFile(pFileName);
}

HVAStruct::HVAStruct(LPVOID pFileBuffer, ULONG nSize, bool bCopy) : HVAStruct()
{
	this->LoadFromFileInBuffer(pFileBuffer, nSize, bCopy);
}

HVAStruct::~HVAStruct()
{
	this->Clear();
}

void HVAStruct::Clear()
{
	if (this->FrameMatrices)
		free(this->FrameMatrices);
}

bool HVAStruct::LoadFromFile(const char * pFileName)
{
	auto hFile = CreateFile(pFileName, FILE_READ_ACCESS, FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr,
		OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);

	if (hFile == INVALID_HANDLE_VALUE)
		return nullptr;

	auto nSize = GetFileSize(hFile, nullptr);
	ULONG nReadBytes;

	auto pFileBuffer = malloc(nSize);
	if (!pFileBuffer)
		return false;

	if (!ReadFile(hFile, pFileBuffer, nSize, &nReadBytes, nullptr) || nReadBytes != nSize) {
		free(pFileBuffer);
		CloseHandle(hFile);
		return false;
	}

	CloseHandle(hFile);
	return this->LoadFromFileInBuffer(pFileBuffer, nSize, false);
}

bool HVAStruct::LoadFromFileInBuffer(LPVOID pFileBuffer, ULONG nSize, bool bCopy)
{
	if (!pFileBuffer || !nSize)
		return false;

	auto nCopySize = sizeof this->nFrameCount + sizeof this->nSectionCount + sizeof this->FileSignature;
	memcpy_s(&this->FileSignature, nCopySize, pFileBuffer, nCopySize);
	
	auto nLoadSize = sizeof TransformationMatrix*this->nFrameCount*this->nSectionCount;
	auto pFileMatrices = reinterpret_cast<PBYTE>(pFileBuffer) + nCopySize + sizeof this->FileSignature*this->nSectionCount;

	if (bCopy) {
		this->FrameMatrices = new TransformationMatrix[this->nFrameCount*this->nSectionCount];
		//this->FrameMatrices = reinterpret_cast<TransformationMatrix*>(malloc(this->nFrameCount*this->nSectionCount * sizeof TransformationMatrix));
		if (this->FrameMatrices){
			memcpy_s(this->FrameMatrices, nLoadSize, pFileMatrices, nLoadSize);
			return true;
		}
	}
	else {
		//the pointer is not pointed to the head so may fail
		this->FrameMatrices = reinterpret_cast<TransformationMatrix*>(pFileMatrices);
		return true;
	}
	return false;
}

bool HVAStruct::IsLoaded()
{
	return this->FrameMatrices != nullptr;
}

int HVAStruct::GetFrameCount()
{
	return this->nFrameCount;
}

int HVAStruct::GetSectionCount()
{
	return this->nSectionCount;
}

TransformationMatrix * HVAStruct::GetTransformMatrix(int idxFrame, int idxSection)
{
	if (!this->IsLoaded())
		return nullptr;

	if (idxFrame >= this->nFrameCount || idxSection >= this->nSectionCount ||
		idxFrame < 0 || idxSection < 0)
		return false;

	return &this->FrameMatrices[idxFrame*this->nSectionCount + idxSection];
}

void DrawObject::RemoveVxlObject(int nID)
{
	for (auto& file : VxlFile::FileObjectTable) {
		if (!file.second)
			continue;
		//try find and erase
		file.second->RemoveOpaqueObject(nID);
	}
}