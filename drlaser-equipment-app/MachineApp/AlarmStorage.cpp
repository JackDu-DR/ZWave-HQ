#include "AlarmStorage.h"
#include <stdexcept>
#include <random>
#include "Helper.h"
#include <string>
#include <comutil.h> 

std::vector<IAlarmDTOPtr> AlarmStorage::_alarms;

AlarmSeverity GetRandomSeverity() {
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> dis(1, 3);

    // Based on the generated integer, return low, medium, or high
    switch (dis(gen)) {
    case 1:
        return AlarmSeverity_High;
    case 2:
        return AlarmSeverity_Medium;
    default:
        return AlarmSeverity_Low;
    }
}

void AlarmStorage::GenerateOneHundredAlarms() {
    auto now = std::chrono::system_clock::now();
    auto epoch_time = std::chrono::system_clock::to_time_t(now);

    for (int i = 0; i < 100; i++) {
        IAlarmDTOPtr alarm(__uuidof(AlarmDTO));
        alarm->ErrorId = GenerateGUIDString();
        alarm->TimeSpan = epoch_time - i * 1234;
        auto severity = GetRandomSeverity();
        alarm->Severity = severity;
        if (severity == AlarmSeverity_High) {
            alarm->ErrorCode = 1022;
        } else if (severity == AlarmSeverity_Medium) {
            alarm->ErrorCode = 1422;
        } else if (severity == AlarmSeverity_Low) {
            alarm->ErrorCode = 1234;
        }
        alarm->WaitResp = GenerateRandom();
        alarm->Ack = GenerateRandom();
        alarm->Retry = GenerateRandom();
        alarm->Cancel = GenerateRandom();
        _alarms.push_back(alarm);
    }

    UpdateAlarmList();
}

IAlarmDTOPtr AlarmStorage::GenerateNewAlarm() {
    auto now = std::chrono::system_clock::now();
    auto epoch_time = std::chrono::system_clock::to_time_t(now);

    IAlarmDTOPtr alarm(__uuidof(AlarmDTO));
    alarm->ErrorId = GenerateGUIDString();
    alarm->TimeSpan = epoch_time;
    auto severity = GetRandomSeverity();
    alarm->Severity = severity;
    if (severity == AlarmSeverity_High) {
        alarm->ErrorCode = 1022;
    }
    else if (severity == AlarmSeverity_Medium) {
        alarm->ErrorCode = 1422;
    }
    else if (severity == AlarmSeverity_Low) {
        alarm->ErrorCode = 1234;
    }
    alarm->WaitResp = GenerateRandom();
    alarm->Ack = GenerateRandom();
    alarm->Retry = GenerateRandom();
    alarm->Cancel = GenerateRandom();

    return alarm;
}

void AlarmStorage::UpdateAlarmList() {
    std::sort(_alarms.begin(), _alarms.end(), [](const IAlarmDTOPtr& a, const IAlarmDTOPtr& b) {
        return a->TimeSpan > b->TimeSpan;
        });

    // Remove excess alarms if more than 100
    while (_alarms.size() > 100) {
        _alarms.pop_back();
    }
}

const std::vector<IAlarmDTOPtr>& AlarmStorage::GetAlarms() {
    return _alarms;
}

void AlarmStorage::AddAlarmList(const std::vector<IAlarmDTOPtr>& alarmList) {
    _alarms.reserve(_alarms.size() + alarmList.size());
    for (const auto& alarm : alarmList) {
        _alarms.push_back(alarm);
    }
    UpdateAlarmList();
}

bool AlarmStorage::AddAlarm(IAlarmDTOPtr alarm) {
    try {
        _alarms.push_back(std::move(alarm));
        UpdateAlarmList();
    }
    catch (const std::runtime_error& e) {
        return false;
    }

    return true;
}

bool AlarmStorage::RemoveAlarm(_bstr_t alarmIdStr) {
    try {
        _alarms.erase(std::remove_if(_alarms.begin(), _alarms.end(),
            [alarmIdStr](const IAlarmDTOPtr& alarm) {
                return ConvertBstrToString(alarm->ErrorId) == ConvertBstrToString(alarmIdStr);
            }),
            _alarms.end());
        UpdateAlarmList();
    }
    catch (const std::runtime_error& e) {
        return false;
    }

    return true;
}

_variant_t AlarmStorage::ToVariant() {
    SAFEARRAYBOUND bound;
    bound.lLbound = 0;
    bound.cElements = _alarms.size();
    SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, _alarms.size());

    for (size_t i = 0; i < _alarms.size(); ++i) {
        IUnknown* pUnknown = nullptr;
        _alarms[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
        SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
        pUnknown->Release();
    }

    _variant_t mData;
    mData.vt = VT_ARRAY | VT_UNKNOWN;
    mData.parray = safeArray;

    return mData;
}