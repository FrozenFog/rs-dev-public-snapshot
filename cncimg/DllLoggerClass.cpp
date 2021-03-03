#include "DllLoggerClass.h"

Logger Logger::Instance;

void _cdecl Logger::WriteLine(const char* pFormat, ...)
{
    if (!Instance.IsLogFileOpened())
        return;

    va_list pStack;

    va_start(pStack, pFormat);
    vfprintf_s(Instance.pFile, pFormat, pStack);
    fflush(Instance.pFile);
    va_end(pStack);
}

bool Logger::IsLogFileOpened()
{
    return pFile != nullptr;
}

bool Logger::OpenLogFile(const char* pFileName)
{
    fopen_s(&pFile, pFileName, "a");

    return this->IsLogFileOpened();
}

void Logger::CloseLogFile()
{
    fclose(pFile);
    pFile = nullptr;
}

void Logger::AddLog(const char* pFormat, ...)
{
//#ifdef _DEBUG
    if (!IsLogFileOpened())
        return;

    va_list pArgs;

    va_start(pArgs, pFormat);
    vfprintf_s(pFile, pFormat, pArgs);
    fflush(pFile);
    va_end(pArgs);
//#endif
}


Logger& Logger::operator<<(const char* pString)
{
    this->AddLog("%s\n", pString);
    return *this;
}
