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

vector<unsigned char> ReadImageAsByteArray(const string& filename) {
    // Open the image file in binary mode
    ifstream file(filename, ios::binary);

    // Check if the file is opened successfully
    if (!file.is_open()) {
        return {};
    }

    // Determine the size of the file
    file.seekg(0, ios::end);
    streampos fileSize = file.tellg();
    file.seekg(0, ios::beg);

    // Allocate a buffer to hold the file contents
    vector<unsigned char> byteArray(fileSize);

    // Read the file contents into the buffer
    file.read(reinterpret_cast<char*>(byteArray.data()), fileSize);

    // Close the file
    file.close();

    return byteArray;
}

SAFEARRAY* VectorToSafeArray(const vector<unsigned char>& vec) {
    // Create a one-dimensional SAFEARRAY with the same size as the vector
    SAFEARRAYBOUND bound;
    bound.lLbound = 0;
    bound.cElements = static_cast<ULONG>(vec.size());
    SAFEARRAY* psa = SafeArrayCreateVector(VT_UI1, 0, bound.cElements);

    // Access the data of the SAFEARRAY
    unsigned char* pData = nullptr;
    HRESULT hr = SafeArrayAccessData(psa, reinterpret_cast<void**>(&pData));
    if (SUCCEEDED(hr)) {
        // Copy the vector data into the SAFEARRAY
        std::copy(vec.begin(), vec.end(), pData);

        // Release the data
        SafeArrayUnaccessData(psa);
    }
    else {
        SafeArrayDestroy(psa); // Destroy the allocated SAFEARRAY
        return nullptr;
    }

    return psa;
}

int GenerateRandom() {
    // Generate a random double between 0 and 1
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> dis(0, 1);

    return dis(gen);
}


std::string ConvertBstrToString(const _bstr_t& bstrValue) {
    // Convert _bstr_t to wchar_t*
    const wchar_t* wideStr = bstrValue;

    // Convert wchar_t* to std::wstring
    std::wstring wideString(wideStr);

    // Remove quotes if present
    std::wstring::size_type firstQuotePos = wideString.find_first_of(L'"');
    std::wstring::size_type lastQuotePos = wideString.find_last_of(L'"');
    if (firstQuotePos != std::wstring::npos && lastQuotePos != std::wstring::npos) {
        wideString = wideString.substr(firstQuotePos + 1, lastQuotePos - firstQuotePos - 1);
    }

    // Convert std::wstring to std::string
    std::string stringValue(wideString.begin(), wideString.end());

    return stringValue;
}

_bstr_t GenerateGUIDString() {
    GUID guid;
    CoCreateGuid(&guid);
    wchar_t guidString[40]; // Size 40 is enough for a GUID string
    if (StringFromGUID2(guid, guidString, sizeof(guidString) / sizeof(guidString[0])) == 0) {
        // Conversion failed
        return L"";
    }
    return _bstr_t(guidString);
}