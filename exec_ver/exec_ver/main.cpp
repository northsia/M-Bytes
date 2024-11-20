#include "crow_all.h"
#include <windows.h>  // لاستخراج معلومات الإصدار
#include "direct.h"
#include <iostream>
#include <string>
#include <filesystem>
#include <vector>

using namespace std;

// دالة لتحويل std::string إلى LPCWSTR باستخدام MultiByteToWideChar
wstring stringToWString(const string& str) {
    int size_needed = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), NULL, 0);
    wstring wstr(size_needed, 0);
    MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), &wstr[0], size_needed);
    return wstr;
}

string EXTRACT_APP_VER(const string& app_path) {
    wstring wAppPath = stringToWString(app_path); // تحويل المسار إلى LPCWSTR

    DWORD dummy;
    DWORD size = GetFileVersionInfoSize(wAppPath.c_str(), &dummy);  // استخدام LPCWSTR

    if (size == 0) {
        return "";  // إذا فشلنا في الحصول على حجم البيانات
    }

    // تخصيص الذاكرة
    vector<BYTE> buffer(size);

    if (!GetFileVersionInfo(wAppPath.c_str(), 0, size, buffer.data())) {
        return "";  // إذا فشلنا في استخراج المعلومات
    }

    // استخراج بيانات الإصدار
    VS_FIXEDFILEINFO* fileInfo;
    UINT fileInfoSize;
    if (!VerQueryValue(buffer.data(), L"\\", (LPVOID*)&fileInfo, &fileInfoSize)) {
        return "";  // إذا فشلنا في استخراج البيانات
    }

    // الحصول على الإصدار بصيغة "major.minor.build.revision"
    string version = to_string(HIWORD(fileInfo->dwFileVersionMS)) + "." +
        to_string(LOWORD(fileInfo->dwFileVersionMS)) + "." +
        to_string(HIWORD(fileInfo->dwFileVersionLS)) + "." +
        to_string(LOWORD(fileInfo->dwFileVersionLS));

    return version;
}

string CURR_DIR() {
    char* cwd = _getcwd(0, 0); // Microsoft-specific
    string working_directory(cwd);
    free(cwd);
    return working_directory;
}

int main() {
    crow::SimpleApp app;

    CROW_ROUTE(app, "/")([]() {
        return "M-Bytes Call-Func Engine API!";
        });

    CROW_ROUTE(app, "/api/v1.0/update")([]() {
        string currentPathStr = CURR_DIR();

        string appPath = currentPathStr + "/M-Bytes.exe";
        string appVersion = EXTRACT_APP_VER("C:\\Users\\Meed\\Desktop\\M-Bytes\\M-Bytes\\bin\\Debug\\M-Bytes.exe");

        if (!appVersion.empty()) {
            return crow::response(200, "{\"Version\": \"" + appVersion + "\"}");
        }
        else {
            return crow::response(400, "{\"error\": \"Version not found\"}");
        }
        });

    app.port(1010).multithreaded().run();

    return 0;
}
