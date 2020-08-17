#pragma once

#include <Windows.h>

#include <string>
#include <unordered_map>
#include <fstream>
#include <algorithm>
#include <vector>

class Logger
{
public:
	static Logger Instance;

	Logger() :pFile(nullptr) {}
	~Logger() { this->CloseLogFile(); }
	
	template<typename... TArgs>
	static void Log(const char* pFormat, TArgs&&... args)
	{
		Instance.AddLog(pFormat, std::forward<TArgs>(args)...);
	}

	static void WriteLine(const char* pString)
	{
		Instance << pString;
	}

	bool IsLogFileOpened();
	bool OpenLogFile(const char* pFileName);
	void CloseLogFile();
	void AddLog(const char* pFormat, ...);
	Logger& operator<<(const char* pString);
	
private:
	FILE* pFile;
};