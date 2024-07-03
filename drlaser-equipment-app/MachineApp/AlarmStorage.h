#pragma once
#include <vector>
#include <memory>
#include <functional>
#include <queue>
#include <iostream>
#include <chrono>
#include <ctime>

#import "CommonLib.tlb"

using namespace CommonLib;

class AlarmStorage {
private:
    static std::vector<IAlarmDTOPtr> _alarms;
public:
    // Functions for demo
    static IAlarmDTOPtr GenerateNewAlarm();
    static void GenerateOneHundredAlarms();

    // Function to process real alarm data
    static void UpdateAlarmList();
    static const std::vector<IAlarmDTOPtr>& GetAlarms();
    static void AddAlarmList(const std::vector<IAlarmDTOPtr>& alarmList);
    static bool AddAlarm(IAlarmDTOPtr alarm);
    static bool RemoveAlarm(_bstr_t alarmIdStr);
    static _variant_t ToVariant();
};