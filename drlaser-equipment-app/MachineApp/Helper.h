#pragma once
#include <vector>
#include <fstream>
#include <Windows.h> // For file handling
#include <atlbase.h> // For _variant_t
#include <comutil.h> // For SafeArrayAccessData, SafeArrayUnaccessData
#include <filesystem> // For directory iteration
#include <future>
#include <random>

using namespace std;
namespace fs = std::filesystem;

vector<unsigned char> ReadImageAsByteArray(const string& filename);
SAFEARRAY* VectorToSafeArray(const vector<unsigned char>& vec);
int GenerateRandom();
std::string ConvertBstrToString(const _bstr_t& bstrValue);
_bstr_t GenerateGUIDString();