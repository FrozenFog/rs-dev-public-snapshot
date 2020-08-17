#include "DllLoggerClass.h"

Logger Logger::Instance;

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
