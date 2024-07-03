//#include <iostream>
//#include <iomanip>
//#include <string>
//#include <functional>
//#include <ATLComTime.h>
//#include <atlsafe.h>
//#include "RPCQueueHandler.h"
//#include <sstream>
//#include "def.h"
//#include <chrono>
//#include <thread>
//#include "LaserCallBack.h"
//#include "MotionCallBack.h"
//#include "VisionCallBack.h"
//#include "ConfigurationCallBack.h"
//#include "Helper.h"
//#include "AlarmCallBack.h"
//#include "AlarmStorage.h"
//#include "PingCallBack.h"
//#include "CriticalAction.h"
//#include "CriticalActionCallback.h"
//#include "ProcessTableConfigurationCallBack.h"
//#include "DonorLiftingModuleConfigurationCallBack.h"
//#include "MotionAxisControlConfigurationCallBack.h"
//#include "InspectionVisionConfigurationCallBack.h"
//
//#import "CommonLib.tlb"
//
//using namespace CommonLib;
//
//IMachineMessageHandlerPtr _machineAMQPHandler;
//IMasterPtr _master;
//
//int main()
//{
//    HRESULT hr = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
//    if (!SUCCEEDED(hr))
//    {
//        return hr;
//    }
//
//    hr = _machineAMQPHandler.CreateInstance(__uuidof(MachineMessageHandler));
//    if (!SUCCEEDED(hr))
//    {
//        return hr;
//    }
//    _machineAMQPHandler->Connect("localhost");
//    _machineAMQPHandler->InitChannel();
//
//    hr = _master.CreateInstance(__uuidof(Master));
//    if (!SUCCEEDED(hr))
//    {
//        return hr;
//    }
//
//    RPCQueueHandler* _RPCQueueHandler;
//    _RPCQueueHandler = new RPCQueueHandler;
//
//    AlarmStorage* alarmStoragePtr;
//    alarmStoragePtr->GenerateOneHundredAlarms();
//
//    ProcessTableConfigurationCallBack processTableConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    MotionAxisControlConfigurationCallBack motionAxisControlConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    DonorLiftingModuleConfigurationCallBack donorLiftingModuleConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    InspectionVisionConfigurationCallBack inspectionVisionConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    LaserCallBack laserObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    VisionCallBack visionObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    ConfigurationCallBack configurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    AlarmCallBack alarmObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    PingCallBack aliveObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//    CriticalActionCallback criticalActionCallback(_RPCQueueHandler, _master, _machineAMQPHandler);
//    MotionCallBack motionObject(_RPCQueueHandler, _master, _machineAMQPHandler);
//
//    if (SUCCEEDED(hr))
//    {
//        _machineAMQPHandler->Subscribe(_RPCQueueHandler);
//
//        std::cout << "Press \n2: Send LaserBasicDTO\n3: Send Output true\n4: Send Output false\n5: Send LaserInfoDTO\n---------------------\n";
//        std::cout << "6: Send LaserBurstDTO\n7: Send Powerlock true\n---------------------\n";
//        std::cout << "8: Stream images\n9: Publish camera Exposure\n10: Publish camera Light\n---------------------\n";
//        std::cout << "11: Publish Alarm\n---------------------\n";
//        std::cout << "12: Publish Circle ROIs\n---------------------\n";
//        std::cout << "13: Publish Rectangle ROIs\n---------------------\n";
//        std::cout << "14: Publish Ellipse ROIs\n---------------------\n";
//        std::cout << "15: Publish Critical true\n---------------------\n";
//        std::cout << "16: Publish Critical false\n---------------------\n";
//        std::cout << "17: Publish Job Camera\n---------------------\n";
//        std::cout << "18: Publish Motion Kinematics\n---------------------\n";
//        std::cout << "19: Publish Motion Test state\n---------------------\n";
//        std::cout << "20: Publish Message to Client\n---------------------\n";
//        std::cout << "21: Publish Motion Moving finished\n---------------------\n";
//
//        std::string str;
//        while (std::getline(std::cin, str))
//        {
//            if (str == "exit")
//            {
//                return 0;
//            }
//
//            if (str == "2")
//            {
//                ILaserApplyDTOPtr laserBasicDTO(__uuidof(LaserApplyDTO));
//                laserBasicDTO->AttenuatorPercentage = 4.12;
//                laserBasicDTO->_PresetControl = PresetControl_Two;
//                laserBasicDTO->PPDivider = 5.12;
//                laserBasicDTO->PulseDuration = 6.12;
//
//                _variant_t mData(laserBasicDTO, true);
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Laser->LaserBasic->Content);
//            }
//            if (str == "3")
//            {
//                _machineAMQPHandler->PublishToGeneral(true, _master->System->Laser->LaserBasic->Output);
//            }
//            if (str == "4")
//            {
//                _machineAMQPHandler->PublishToGeneral(false, _master->System->Laser->LaserBasic->Output);
//            }
//            if (str == "5")
//            {
//                ILaserInfoDTOPtr laserInfoDTO(__uuidof(LaserInfoDTO));
//                laserInfoDTO->IsConnected = true;
//                laserInfoDTO->Operation = LaserOperation_Operation;
//                laserInfoDTO->Emission = false;
//                laserInfoDTO->LaserPower = 79.7682;
//                laserInfoDTO->Energy = 934.342;
//                laserInfoDTO->LaserPowerRange = LaserStatusRange_Unknown;
//                laserInfoDTO->Frequency = 50.7682;
//                laserInfoDTO->PulseDivider = 9.342;
//                laserInfoDTO->FrequencyRange = LaserStatusRange_WithinRange;
//                laserInfoDTO->WaveLength = 999.532;
//                laserInfoDTO->WaveLengthRange = LaserStatusRange_Warning;
//
//                _variant_t mData(laserInfoDTO, true);
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Laser->Info);
//            }
//            if (str == "6")
//            {
//                ILaserBurstDTOPtr laserBurstDTO(__uuidof(LaserBurstDTO));
//                laserBurstDTO->IsPowerlockEnabled = true;
//                laserBurstDTO->P = 2.22;
//                laserBurstDTO->N = 5.32;
//                laserBurstDTO->EnvelopeControlP = 1.41;
//                laserBurstDTO->EnvelopeControlN = 2.00;
//
//                _variant_t mData(laserBurstDTO, true);
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Laser->LaserBurst->Content);
//            }
//            if (str == "7")
//            {
//                _machineAMQPHandler->PublishToGeneral(true, _master->System->Laser->LaserBurst->Powerlock);
//            }
//            if (str == "8") {
//                std::async(std::launch::async, []() {
//                    std::cout << "\n Vision Camera on live" << std::endl;
//                    std::vector<SAFEARRAY*> safeArrays;
//
//                    string folderPath = "C:\\Sioux Release Code\\20240607-7a485b6-testing\\mono8";
//                    std::cout << "\n Reading images from file" << "\n";
//
//                    for (const auto& entry : fs::directory_iterator(folderPath)) {
//                        if (entry.is_regular_file()) {
//                            // Get the path of the image file
//                            string filename = entry.path().string();
//                            // Read image file into a byte array
//                            vector<unsigned char> imageData = ReadImageAsByteArray(filename);
//                            safeArrays.push_back(VectorToSafeArray(imageData));
//                        }
//                    }
//
//                    std::vector<IImageStreamDTOPtr> imageStreamDTOPtrArray;
//                    while (true)
//                    {
//                        for (size_t i = 0; i < safeArrays.size(); ++i) {
//                            std::chrono::system_clock::time_point now = std::chrono::system_clock::now();
//                            auto duration_ms = std::chrono::duration_cast<std::chrono::milliseconds>(now.time_since_epoch());
//                            double timestamp = duration_ms.count();
//
//                            IImageStreamDTOPtr imageStreamDTO(__uuidof(ImageStreamDTO));
//                            imageStreamDTO->FullImageWidth = 7000;
//                            imageStreamDTO->FullImageHeight = 9000;
//                            imageStreamDTO->ScrollX = 300;
//                            imageStreamDTO->ScrollY = 500;
//                            imageStreamDTO->Ratio = 2;
//                            imageStreamDTO->ImageBytes = safeArrays[i];
//                            imageStreamDTO->Timestamp = timestamp;
//
//                            _variant_t mData(imageStreamDTO, true);
//                            _machineAMQPHandler->PublishToStream(mData);
//
//                            std::cout << "\n Send image no " << i + 1 << std::endl;
//                            //std::this_thread::sleep_for(std::chrono::milliseconds(100));
//                        }
//                    }
//                    });
//            }
//            if (str == "9") {
//                std::cout << "\n Publish Exposure" << std::endl;
//
//                _machineAMQPHandler->PublishToGeneral(2.3, _master->System->Vision->Exposure);
//            }
//            if (str == "10") {
//                std::cout << "\n Publish Light" << std::endl;
//
//                _machineAMQPHandler->PublishToGeneral(false, _master->System->Vision->Light);
//            }
//            if (str == "11") {
//                std::cout << "\n Generate new alarm" << std::endl;
//
//                auto newAlarm = alarmStoragePtr->GenerateNewAlarm();
//
//                std::cout
//                    << "\n New created alarm: "
//                    << "\n -Id = " << newAlarm->ErrorId
//                    << "\n -Time Span = " << std::fixed << newAlarm->TimeSpan
//                    << "\n -Severity = " << newAlarm->Severity
//                    << "\n -ErrorCode = " << newAlarm->ErrorCode
//                    << "\n -WaitResp = " << newAlarm->WaitResp
//                    << "\n -Ack = " << newAlarm->Ack
//                    << "\n -Retry = " << newAlarm->Retry
//                    << "\n -Cancel = " << newAlarm->Cancel
//                    << std::endl;
//
//                auto addAlarmResult = alarmStoragePtr->AddAlarm(newAlarm);
//
//                if (addAlarmResult) {
//                    std::cout << "\n Push alarm to storage list success, start publish alarm to clients" << std::endl;
//
//                    _variant_t mData(newAlarm, true);
//                    _machineAMQPHandler->PublishToGeneral(mData, _master->Alarm->New);
//                }
//                else
//                {
//                    std::cerr << "\n Failed to insert new alarm to list, please retry" << std::endl;
//                }
//            }
//            if (str == "12") {
//                std::cout << "\n Publish Circle ROIs" << std::endl;
//
//                std::vector<ICircleROIDTOPtr> circleROIDTOPtrArray;
//
//                for (int i = 0; i < 5; ++i) {
//                    ICircleROIDTOPtr circleROIDTOPtr(__uuidof(CircleROIDTO));
//                    circleROIDTOPtr->ColumnCenter = 100;
//                    circleROIDTOPtr->RowCenter = 100 + i * 110;
//                    circleROIDTOPtr->Radius = 50;
//                    circleROIDTOPtr->Score = 60 + i * 10;
//                    circleROIDTOPtrArray.push_back(circleROIDTOPtr);
//                }
//
//                SAFEARRAYBOUND bound;
//                bound.lLbound = 0;
//                bound.cElements = circleROIDTOPtrArray.size();
//                SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, circleROIDTOPtrArray.size());
//
//                for (size_t i = 0; i < circleROIDTOPtrArray.size(); ++i) {
//                    IUnknown* pUnknown = nullptr;
//                    circleROIDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
//                    SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
//                    pUnknown->Release();
//                }
//
//                _variant_t mData;
//                mData.vt = VT_ARRAY | VT_UNKNOWN;
//                mData.parray = safeArray;
//
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Vision->CircleROIResultId);
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionTeaching, false), _master->CriticalControl->Update);
//            }
//            if (str == "13") {
//                std::cout << "\n Publish Rectangle ROIs" << std::endl;
//
//                std::vector<IRectangleROIDTOPtr> rectangleROIDTOPtrArray;
//
//                for (int i = 0; i < 5; ++i) {
//                    IRectangleROIDTOPtr rectangleROIDTOPtr(__uuidof(RectangleROIDTO));
//                    rectangleROIDTOPtr->Row1 = 100;
//                    rectangleROIDTOPtr->Column1 = 100 + i * 110;
//                    rectangleROIDTOPtr->Row2 = 150;
//                    rectangleROIDTOPtr->Column2 = 100 + i * 110 + 50;
//                    rectangleROIDTOPtr->Score = 60 + i * 10;
//                    rectangleROIDTOPtr->Phi = -36.25 * i;
//                    rectangleROIDTOPtrArray.push_back(rectangleROIDTOPtr);
//                }
//
//                SAFEARRAYBOUND bound;
//                bound.lLbound = 0;
//                bound.cElements = rectangleROIDTOPtrArray.size();
//                SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, rectangleROIDTOPtrArray.size());
//
//                for (size_t i = 0; i < rectangleROIDTOPtrArray.size(); ++i) {
//                    IUnknown* pUnknown = nullptr;
//                    rectangleROIDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
//                    SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
//                    pUnknown->Release();
//                }
//
//                _variant_t mData;
//                mData.vt = VT_ARRAY | VT_UNKNOWN;
//                mData.parray = safeArray;
//
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Vision->RectangleROIResultId);
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionTeaching, false), _master->CriticalControl->Update);
//            }
//            if (str == "14") {
//                std::cout << "\n Publish Ellipse ROIs" << std::endl;
//
//                std::vector<IEllipseROIDTOPtr> ellipseROIDTOPtrArray;
//
//                for (int i = 0; i < 5; ++i) {
//                    IEllipseROIDTOPtr ellipseROIDTOPtr(__uuidof(EllipseROIDTO));
//                    ellipseROIDTOPtr->Row1 = 100;
//                    ellipseROIDTOPtr->Column1 = 10 + i * 110;
//                    ellipseROIDTOPtr->Radius1 = 150;
//                    ellipseROIDTOPtr->Radius2 = 100;
//                    ellipseROIDTOPtr->Score = 60 + i * 10;
//                    ellipseROIDTOPtr->Phi = -36.25 * i;
//                    ellipseROIDTOPtrArray.push_back(ellipseROIDTOPtr);
//                }
//
//                SAFEARRAYBOUND bound;
//                bound.lLbound = 0;
//                bound.cElements = ellipseROIDTOPtrArray.size();
//                SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, ellipseROIDTOPtrArray.size());
//
//                for (size_t i = 0; i < ellipseROIDTOPtrArray.size(); ++i) {
//                    IUnknown* pUnknown = nullptr;
//                    ellipseROIDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
//                    SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
//                    pUnknown->Release();
//                }
//
//                _variant_t mData;
//                mData.vt = VT_ARRAY | VT_UNKNOWN;
//                mData.parray = safeArray;
//
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Vision->EllipseROIResultId);
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType::CriticalType_MotionTeaching, false), _master->CriticalControl->Update);
//            }
//
//            if (str == "15") {
//                std::cout << "\n Publish Critical MotionAxisGo true" << std::endl;
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionAxisGo, true), _master->CriticalControl->Update);
//            }
//
//            if (str == "16") {
//                std::cout << "\n Publish Critical MotionAxisGo false" << std::endl;
//
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionAxisGo, false), _master->CriticalControl->Update);
//            }
//            if (str == "17") {
//                std::cout << "\n Job Camera on live" << std::endl;
//                std::vector<SAFEARRAY*> safeArrays;
//
//                string folderPath = "C:\\Sioux Release Code\\20240607-7a485b6-testing\\mono8";
//                std::cout << "\n Reading images from file" << "\n";
//
//                for (const auto& entry : fs::directory_iterator(folderPath)) {
//                    if (entry.is_regular_file()) {
//                        // Get the path of the image file
//                        string filename = entry.path().string();
//                        // Read image file into a byte array
//                        vector<unsigned char> imageData = ReadImageAsByteArray(filename);
//                        safeArrays.push_back(VectorToSafeArray(imageData));
//                    }
//                }
//
//                std::thread t1([safeArrays]() {
//                    while (true)
//                    {
//                        for (size_t i = 0; i < safeArrays.size(); ++i) {
//                            _machineAMQPHandler->PublishToCamera1Stream(safeArrays[i]);
//
//                            std::cout << "\n Send image no " << i + 1 << std::endl;
//                            std::this_thread::sleep_for(std::chrono::nanoseconds(1500));
//                        }
//                    }
//                    });
//
//                std::thread t2([safeArrays]() {
//                    while (true)
//                    {
//                        for (size_t i = 0; i < safeArrays.size(); ++i) {
//                            _machineAMQPHandler->PublishToCamera2Stream(safeArrays[i]);
//
//                            std::cout << "\n Send image no " << i + 1 << std::endl;
//                            std::this_thread::sleep_for(std::chrono::nanoseconds(1500));
//                        }
//                    }
//                    });
//
//                std::thread t3([safeArrays]() {
//                    while (true)
//                    {
//                        for (size_t i = 0; i < safeArrays.size(); ++i) {
//                            _machineAMQPHandler->PublishToCamera3Stream(safeArrays[i]);
//
//                            std::cout << "\n Send image no " << i + 1 << std::endl;
//                            std::this_thread::sleep_for(std::chrono::nanoseconds(1500));
//                        }
//                    }
//                    });
//
//                t1.join();
//                t2.join();
//                t3.join();
//            }
//
//            if (str == "18") {
//                std::cout << "\n Publish Motion kinematics" << std::endl;
//
//                IMotionKinematicsDTOPtr motionKinematicsDTO(__uuidof(MotionKinematicsDTO));
//                motionKinematicsDTO->Velocity = 3.0;
//                motionKinematicsDTO->Acceleration = 4.0;
//                motionKinematicsDTO->Jerk = 5.0;
//                std::cout
//                    << "\Motion kinematics data: "
//                    << "\n-Velocity = " << motionKinematicsDTO->Velocity
//                    << "\n-Acceleration = " << motionKinematicsDTO->Acceleration
//                    << "\n-Jerk = " << motionKinematicsDTO->Jerk
//                    << std::endl;
//
//                _variant_t mData(motionKinematicsDTO, true);
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionControl->KinematicsRoute);
//            }
//
//            if (str == "19") {
//                std::cout << "\n Publish Motion Test state" << std::endl;
//
//                IMotionTestStateDTOPtr motionTestStateDTO(__uuidof(MotionTestStateDTO));
//                motionTestStateDTO->CurrentCycles = 1.0;
//                motionTestStateDTO->TotalCycles = 2.0;
//                motionTestStateDTO->CommandPosition = 1.5;
//                motionTestStateDTO->EncoderPosition = 3.2;
//                motionTestStateDTO->MissingSteps = 1;
//
//                std::cout
//                    << "\Motion Test state data: "
//                    << "\n-Current cycles = " << motionTestStateDTO->CurrentCycles
//                    << "\n-Total cycels = " << motionTestStateDTO->TotalCycles
//                    << "\n-Command position = " << motionTestStateDTO->CommandPosition
//                    << "\n-Encoder position = " << motionTestStateDTO->EncoderPosition
//                    << "\n-Missing steps = " << motionTestStateDTO->MissingSteps
//                    << std::endl;
//
//                _variant_t mData(motionTestStateDTO, true);
//                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionControl->TestState);
//            }
//            if (str == "20") {
//                std::cout << "\n Publish Message to Client" << std::endl;
//
//                _machineAMQPHandler->PublishToGeneral("Motion is moving...", _master->Message);
//            }
//            if (str == "21") {
//                std::cout << "\n Publish Critical Motion Moving finished" << std::endl;
//
//                _machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionMoving, false), _master->CriticalControl->Update);
//            }
//        }
//    }
//}





#include <iostream>
#include <iomanip>
#include <string>
#include <functional>
#include <ATLComTime.h>
#include <atlsafe.h>
#include "RPCQueueHandler.h"
#include <sstream>
#include "def.h"
#include <chrono>
#include <thread>
#include "LaserCallBack.h"
#include "VisionCallBack.h"
#include "InspectionVisionCallBack.h"
#include "ConfigurationCallBack.h"
#include "ProcessTableConfigurationCallBack.h"
#include "DonorLiftingModuleConfigurationCallBack.h"
#include "MotionAxisControlConfigurationCallBack.h"
#include "InspectionVisionConfigurationCallBack.h"
#include "Helper.h"
//#include "AlarmCallBack.h"
//#include "AlarmStorage.h"

#import "CommonLib.tlb"


using namespace CommonLib;

IMachineMessageHandlerPtr _machineAMQPHandler;
IMasterPtr _master;

static std::string getCurrentDateTime()
{
    //Get Current Time
    time_t now = time(0);
    char ptime_str[50];
    strftime(ptime_str, sizeof(ptime_str), "%Y%m%d %H:%M:%S", localtime(&now));

    std::string dateTime_str = ptime_str;
    SYSTEMTIME SystemTime;
    GetSystemTime(&SystemTime);
    memset((void*)ptime_str, 0, 50);

    std::string millisec = "";
    if (SystemTime.wMilliseconds < 10) {
        millisec = ".00" + to_string(SystemTime.wMilliseconds);
    }
    else if (SystemTime.wMilliseconds < 100) {
        millisec = ".0" + to_string(SystemTime.wMilliseconds);
    }
    else {
        millisec = "." + std::to_string(SystemTime.wMilliseconds);
    }

    dateTime_str += millisec;
    return dateTime_str;
}

void LaserBasicFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "\n LaserBasicDTO Fetch received" << std::endl;

    ILaserBasicDTOPtr laserBasicDTO(__uuidof(LaserBasicDTO));
    laserBasicDTO->IsOutputEnabled = true;
    laserBasicDTO->_PresetControl = PresetControl_One;
    laserBasicDTO->AttenuatorPercentage = 2.22123;
    laserBasicDTO->PPDivider = 3.32123;
    laserBasicDTO->PulseDuration = 4.42123;

    _variant_t mData(laserBasicDTO, true);
    _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Laser->LaserBasic->Content, mData);
}

void LaserBasicApplyCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    ILaserBasicDTOPtr laserBasicDTO(__uuidof(LaserBasicDTO));
    laserBasicDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nApply received: "
        << "\n-Preset = " << laserBasicDTO->_PresetControl
        << "\n-Attenuator Percentage = " << laserBasicDTO->AttenuatorPercentage
        << "\n-PPDivider = " << laserBasicDTO->PPDivider
        << "\n-Pulse Duration = " << laserBasicDTO->PulseDuration
        << std::endl;
}

void LaserBasicOutputCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    CComBSTR temp(data.bstrVal);
    _bstr_t restr = temp.Detach();

    std::cout << "Output received: " << restr;
    std::cout << "\n";
}

void LaserBasicConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "Connect command received";
    std::cout << "\n";
}

void LaserBasicStandByCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "StandBy command received";
    std::cout << "\n";
}

void  DonorLiftingFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "\n DonorLiftingDTO Fetch received" << std::endl;

    IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
    donorLiftingModuleDTO->XAxisPosition = 112233;
    donorLiftingModuleDTO->YAxisPosition = 45.126;
    donorLiftingModuleDTO->ZAxisPosition = 778899;
    donorLiftingModuleDTO->IsDonorChuckVacuumEnabled = false;

    _variant_t mData(donorLiftingModuleDTO, true);
    _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->CausewaySystem->DonorLiftingModule->Content, mData);
}

void DonorLiftingMoveCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
    donorLiftingModuleDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nDonor Move received: "
        << "\n-UI = " << donorLiftingModuleDTO->_DonorLifterUIElement
        << "\n-Direction = " << donorLiftingModuleDTO->_MoveDirection
        << "\n-MotionCmd = " << donorLiftingModuleDTO->_MotionCmd
        << "\n-MoveRel = " << donorLiftingModuleDTO->MoveRel
        << std::endl;
}

void DonorLiftingActionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
    donorLiftingModuleDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\n Donor Home received: "
        << "\n-UI = " << donorLiftingModuleDTO->_DonorLifterUIElement
        << "\n-Direction = " << donorLiftingModuleDTO->_MoveDirection
        << "\n-MotionCmd = " << donorLiftingModuleDTO->_MotionCmd
        << "\n-MoveRel = " << donorLiftingModuleDTO->MoveRel
        << std::endl;
}

void DonorLiftingCameraChangeCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
    donorLiftingModuleDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\n Donor Camera Change received: "
        << "\n-UI = " << donorLiftingModuleDTO->_DonorLifterUIElement
        << "\n-CameraSelect = " << donorLiftingModuleDTO->_CameraSelect
        << std::endl;
}

void DonorLiftingDonorChuckVacuumCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
    donorLiftingModuleDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nDonor Chuck Vacuum updated received: "
        << "\n-Donor Chuck Vacuum = " << donorLiftingModuleDTO->IsDonorChuckVacuumEnabled
        << std::endl;
}

void ProcessTableFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "\n ProcessTableDTO Fetch received";

    IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
    processTableDTO->XAxisPosition = 121111113;
    processTableDTO->YAxisPosition = 332;
    processTableDTO->TXAxisPosition = 442;
    processTableDTO->TYAxisPosition = 666;
    processTableDTO->ZAxisPosition = 771;
    processTableDTO->IsSubstrateChuckVacuumEnabled = true;

    _variant_t mData(processTableDTO, true);
    _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->ProcessSystem->ProcessTable->Content, mData);
}

void ProcessTableMoveCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
    processTableDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nPro_Table Move received: "
        << "\n-UI = " << processTableDTO->_ProTableUIElement
        << "\n-Direction = " << processTableDTO->_MoveDirection
        << "\n-MotionCmd = " << processTableDTO->_MotionCmd
        << "\n-MoveRel = " << processTableDTO->MoveRel
        << std::endl;
}

void ProcessTableActionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
    processTableDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nPro_Table Home received: "
        << "\n-UI = " << processTableDTO->_ProTableUIElement
        << "\n-Direction = " << processTableDTO->_MoveDirection
        << "\n-MotionCmd = " << processTableDTO->_MotionCmd
        << "\n-MoveRel = " << processTableDTO->MoveRel
        << std::endl;
}

void ProcessTableCameraChangeCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
    processTableDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\n Pro_Table Camera Change received: "
        << "\n-UI = " << processTableDTO->_ProTableUIElement
        << "\n-CameraSelect = " << processTableDTO->_CameraSelect
        << std::endl;
}

void SubstrateChuckVacuumCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
    processTableDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Substrate Chuck Vacuum updated received: "
        << "\n-Donor Chuck Vacuum = " << processTableDTO->IsSubstrateChuckVacuumEnabled
        << std::endl;
}

void MotionSelectedAxisChangedCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
    motionAxisControlDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\nAxis update received: "
        << "\n-new axis name = " << motionAxisControlDTO->_AxisSelection
        << std::endl;

    /*if ((int)motionAxisControlDTO->_AxisSelection == 1) {
        motionAxisControlDTO->MovePos = 1;
        motionAxisControlDTO->VelPos = 2;
        motionAxisControlDTO->AcclPos = 3;
        motionAxisControlDTO->JerkPos = 4;
        motionAxisControlDTO->ActualMovePos = 5;
        motionAxisControlDTO->ActualVelPos = 6;
        motionAxisControlDTO->ActualAcclPos = 7;
        motionAxisControlDTO->ActualJerkPos = 8;
        motionAxisControlDTO->EnableBtnIsActive = true;
        motionAxisControlDTO->IsRelPos = true;
    }
    else {
        motionAxisControlDTO->MovePos = 11;
        motionAxisControlDTO->VelPos = 22;
        motionAxisControlDTO->AcclPos = 33;
        motionAxisControlDTO->JerkPos = 44;
        motionAxisControlDTO->ActualMovePos = 55;
        motionAxisControlDTO->ActualVelPos = 66;
        motionAxisControlDTO->ActualAcclPos = 77;
        motionAxisControlDTO->ActualJerkPos = 88;
        motionAxisControlDTO->EnableBtnIsActive = false;
        motionAxisControlDTO->IsRelPos = false;
    }*/

    /*_variant_t mData(motionAxisControlDTO, true);
    _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->Content);*/

}

void MotionAxisControlActionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
    motionAxisControlDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Motion Axis Control received: "
        << "\n-Axis = " << motionAxisControlDTO->_AxisSelection
        << "\n-UI = " << motionAxisControlDTO->_MotionUIElement
        << "\n-MotionCmd = " << motionAxisControlDTO->_MotionCmd
        << "\n-MovePos = " << motionAxisControlDTO->MovePos
        << "\n-VelPos = " << motionAxisControlDTO->VelPos
        << "\n-AcclPos = " << motionAxisControlDTO->AcclPos
        << "\n-JerkPos = " << motionAxisControlDTO->JerkPos
        << std::endl;
}

void InspectionVisionConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-Connected = " << inspectionVisionDTO->Connected
        << std::endl;
}

void InspectionVisionLiveCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-Acquisition = " << inspectionVisionDTO->Acquisition
        << std::endl;
}

void InspectionVisionTriggerCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-GrabImage = " << inspectionVisionDTO->GrabImage
        << std::endl;
}

void InspectionVisionSaveCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << std::endl;
}

void InspectionVisionExposureTimeCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ExposureTime = " << inspectionVisionDTO->ExposureTime
        << std::endl;
}

void InspectionVisionGainCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-Gain = " << inspectionVisionDTO->Gain
        << std::endl;
}

void InspectionVisionGammaCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-Gamma = " << inspectionVisionDTO->Gamma
        << std::endl;
}

void InspectionVisionGammaEnableCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-GammaEnable = " << inspectionVisionDTO->GammaEnable
        << std::endl;
}

void InspectionVisionBlackLevelCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-BlackLevel = " << inspectionVisionDTO->BlackLevel
        << std::endl;
}

void InspectionVisionSharpnessCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-Sharpness = " << inspectionVisionDTO->Sharpness
        << std::endl;
}

void InspectionVisionZoomValueCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ZoomValue = " << inspectionVisionDTO->ZoomValue
        << std::endl;

    if (inspectionVisionDTO->ZoomValue != 0) {
        IInspectionVisionDTOPtr inspectionVisionDTO2(__uuidof(InspectionVisionDTO));
        inspectionVisionDTO2->LoadDataFromJson(data.bstrVal);
        inspectionVisionDTO2->ZoomValue = inspectionVisionDTO->ZoomValue;

        _variant_t mData2(inspectionVisionDTO2, true);
        _machineAMQPHandler->PublishToGeneral(mData2, _master->System->ProcessSystem->InspectionVision->ZoomValue);
    }
}

void InspectionVisionScrollVerticalCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ScrollVertical = " << inspectionVisionDTO->ScrollVertical
        << std::endl;
}

void InspectionVisionScrollHorizontalCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision received: "
        << "\n-UI = " << inspectionVisionDTO->_ProSystemUpLookInspecVisionUIElement
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ScrollHorizontal = " << inspectionVisionDTO->ScrollHorizontal
        << std::endl;
}

void InspectionVisionTeachCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision Teach received: "
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ModelSelect = " << inspectionVisionDTO->_ModelSelect
        << "\n-MetricSelect = " << inspectionVisionDTO->_MetricSelect
        << "\n-NumLevelAutoNcc = " << inspectionVisionDTO->NumLevelAutoNcc
        << "\n-AngStepAuto = " << inspectionVisionDTO->AngStepAuto
        << "\n-NumLevel = " << inspectionVisionDTO->NumLevel
        << "\n-AngleStart = " << inspectionVisionDTO->AngleStart
        << "\n-AngleExtent = " << inspectionVisionDTO->AngleExtent
        << "\n-AngleStep = " << inspectionVisionDTO->AngleStep
        << std::endl;
}

void InspectionVisionCalibrationActionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
    inspectionVisionDTO->LoadDataFromJson(data.bstrVal);
    std::cout
        << "\Inspection Vision CalibrationAction received: "
        << "\n-CamSelect = " << inspectionVisionDTO->_CameraSelect
        << "\n-InspecVisionPage = " << inspectionVisionDTO->_InspectionVisionPage
        << "\n-ProSystemVCalibartionUIElement = " << inspectionVisionDTO->_ProSystemVCalibartionUIElement
        << "\n-CalibrationModelId = " << inspectionVisionDTO->CalibrationModelId
        << "\n-StepSize = " << inspectionVisionDTO->StepSize
        << "\n-StepCount = " << inspectionVisionDTO->StepCount
        << std::endl;
}

int main()
{

    string fileName;

    HRESULT hr = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
    if (!SUCCEEDED(hr))
    {
        return hr;
    }

    hr = _machineAMQPHandler.CreateInstance(__uuidof(MachineMessageHandler));
    if (!SUCCEEDED(hr))
    {
        return hr;
    }
    _machineAMQPHandler->Connect("localhost");// , "guest", "guest");
    _machineAMQPHandler->InitChannel();

    hr = _master.CreateInstance(__uuidof(Master));
    if (!SUCCEEDED(hr))
    {
        return hr;
    }

    RPCQueueHandler* _RPCQueueHandler;
    _RPCQueueHandler = new RPCQueueHandler;

    //AlarmStorage* alarmStoragePtr;
    //alarmStoragePtr->GenerateOneHundredAlarms();

    //VisionCallBack visionObject(_RPCQueueHandler, _master, _machineAMQPHandler, _invLog.get());
    InspectionVisionCallBack inspectionVisionObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    ConfigurationCallBack configurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    ProcessTableConfigurationCallBack processTableConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    MotionAxisControlConfigurationCallBack motionAxisControlConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    DonorLiftingModuleConfigurationCallBack donorLiftingModuleConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    InspectionVisionConfigurationCallBack inspectionVisionConfigurationObject(_RPCQueueHandler, _master, _machineAMQPHandler);
    //AlarmCallBack alarmObject(_RPCQueueHandler, _master, _machineAMQPHandler);

    _RPCQueueHandler->AddCallback(_master->System->Laser->LaserBasic->Fetch, LaserBasicFetchCallback);
    _RPCQueueHandler->AddCallback(_master->System->Laser->LaserBasic->Apply, LaserBasicApplyCallback);
    _RPCQueueHandler->AddCallback(_master->System->Laser->LaserBasic->Output, LaserBasicOutputCallback);
    _RPCQueueHandler->AddCallback(_master->System->Laser->LaserBasic->Connect, LaserBasicConnectCallback);
    _RPCQueueHandler->AddCallback(_master->System->Laser->LaserBasic->StandBy, LaserBasicStandByCallback);

    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->ProcessTable->Fetch, ProcessTableFetchCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->ProcessTable->Move, ProcessTableMoveCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->ProcessTable->Action, ProcessTableActionCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->ProcessTable->CameraChange, ProcessTableCameraChangeCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->ProcessTable->SubstrateChuckVacuumOutput, SubstrateChuckVacuumCallback);

    _RPCQueueHandler->AddCallback(_master->System->CausewaySystem->DonorLiftingModule->Fetch, DonorLiftingFetchCallback);
    _RPCQueueHandler->AddCallback(_master->System->CausewaySystem->DonorLiftingModule->Move, DonorLiftingMoveCallback);
    _RPCQueueHandler->AddCallback(_master->System->CausewaySystem->DonorLiftingModule->Action, DonorLiftingActionCallback);
    _RPCQueueHandler->AddCallback(_master->System->CausewaySystem->DonorLiftingModule->CameraChange, DonorLiftingCameraChangeCallback);
    _RPCQueueHandler->AddCallback(_master->System->CausewaySystem->DonorLiftingModule->DonorChuckVacuumOutput, DonorLiftingDonorChuckVacuumCallback);

    _RPCQueueHandler->AddCallback(_master->System->Motion->MotionAxisControl->ChangeAxis, MotionSelectedAxisChangedCallback);
    _RPCQueueHandler->AddCallback(_master->System->Motion->MotionAxisControl->Action, MotionAxisControlActionCallback);

    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Connect, InspectionVisionConnectCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Live, InspectionVisionLiveCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Trigger, InspectionVisionTriggerCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Save, InspectionVisionSaveCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->ExposureTime, InspectionVisionExposureTimeCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Gain, InspectionVisionGainCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Gamma, InspectionVisionGammaCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->GammaEnable, InspectionVisionGammaEnableCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->BlackLevel, InspectionVisionBlackLevelCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Sharpness, InspectionVisionSharpnessCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->ZoomValue, InspectionVisionZoomValueCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->ScrollVertical, InspectionVisionScrollVerticalCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->ScrollHorizontal, InspectionVisionScrollHorizontalCallback);

    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->Teach, InspectionVisionTeachCallback);
    _RPCQueueHandler->AddCallback(_master->System->ProcessSystem->InspectionVision->CalibrationAction, InspectionVisionCalibrationActionCallback);


    if (SUCCEEDED(hr))
    {
        _machineAMQPHandler->Subscribe(_RPCQueueHandler);

        /*std::cout << "Press \n2: Broadcast LaserBasicDTO\n3: Broadcast Output true\n4: Broadcast Output false\n5: Donor Chuck Vacuum TRUE\n6: Donor Chuck Vacuum FALSE\n7: Test Pass Data to Donor Lifting Module\n8: Substrate Chuck Vacuum TRUE\n9: Substrate Chuck Vacuum FALSE\n10: Test Pass Data to Process Table\n11: Send something to Motion Axis Control\n---------------------\n";

        std::string str;
        while (std::getline(std::cin, str))
        {
            if (str == "exit")
            {
                return 0;
            }

            if (str == "2")
            {
                ILaserApplyDTOPtr laserBasicDTO(__uuidof(LaserApplyDTO));
                laserBasicDTO->AttenuatorPercentage = 4.12;
                laserBasicDTO->_PresetControl = PresetControl_Two;
                laserBasicDTO->PPDivider = 5.12;
                laserBasicDTO->PulseDuration = 6.12;

                _variant_t mData(laserBasicDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Laser->LaserBasic->Content);
            }
            else if (str == "3")
            {
                _machineAMQPHandler->PublishToGeneral(true, _master->System->Laser->LaserBasic->Output);
            }
            else if (str == "4")
            {
                _machineAMQPHandler->PublishToGeneral(false, _master->System->Laser->LaserBasic->Output);
            }
            else if (str == "5")
            {
                _machineAMQPHandler->PublishToGeneral(true, _master->System->CausewaySystem->DonorLiftingModule->DonorChuckVacuumOutput);
            }
            else if (str == "6")
            {
                _machineAMQPHandler->PublishToGeneral(false, _master->System->CausewaySystem->DonorLiftingModule->DonorChuckVacuumOutput);
            }
            else if (str == "7")
            {
                IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
                donorLiftingModuleDTO->XAxisPosition = 11.22;
                donorLiftingModuleDTO->YAxisPosition = 4.56;
                donorLiftingModuleDTO->ZAxisPosition = 77.8899;
                donorLiftingModuleDTO->IsDonorChuckVacuumEnabled = true;

                _variant_t mData(donorLiftingModuleDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->CausewaySystem->DonorLiftingModule->Content);

            }
            else if (str == "8")
            {
                _machineAMQPHandler->PublishToGeneral(true, _master->System->ProcessSystem->ProcessTable->SubstrateChuckVacuumOutput);
            }
            else if (str == "9")
            {
                _machineAMQPHandler->PublishToGeneral(false, _master->System->ProcessSystem->ProcessTable->SubstrateChuckVacuumOutput);
            }
            else if (str == "10")
            {
                IProcessTableDTOPtr processTableDTO(__uuidof(ProcessTableDTO));
                processTableDTO->XAxisPosition = 55;
                processTableDTO->YAxisPosition = 66;
                processTableDTO->TXAxisPosition = 77;
                processTableDTO->TYAxisPosition = 8111111118;
                processTableDTO->ZAxisPosition = 99;
                processTableDTO->IsSubstrateChuckVacuumEnabled = true;

                _variant_t mData(processTableDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->ProcessTable->Content);

            }
            else if (str == "11")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->ActualMovePos = 567;
                motionAxisControlDTO->ActualVelPos = 678;
                motionAxisControlDTO->ActualAcclPos = 7;
                motionAxisControlDTO->ActualJerkPos = 8;
                motionAxisControlDTO->IsRelPos = true;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->Content);

            }
        }*/

        std::cout << "Press \n2:Camera Connected\n3:Camera Disconnected\n4:Camera Live\n5:Camera Offlive\n6, 7, 8:Control Motion Axis page button background color\n9, 10:Control Motion Axis page Enabled button background color\n11:Show Calibration page result\n---------------------\n";
        std::cout << "Press \n12:Pass data to model id selection\n13:Draw shape on Inspection Vision Image\n14:Show machine status to message box\n15:Live Streaming sample\n16:Pass data to Motion Axis Control\n---------------------\n";

        std::string str;
        while (std::getline(std::cin, str))
        {
            if (str == "exit")
            {
                return 0;
            }

            if (str == "2")
            {
                IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO->Connected = true;

                _variant_t mData(inspectionVisionDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->Connect);
            }
            else if (str == "3")
            {
                IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO->Connected = false;

                _variant_t mData(inspectionVisionDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->Connect);
            }
            else if (str == "4")
            {
                IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO->Acquisition = true;

                _variant_t mData(inspectionVisionDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->Live);

                IInspectionVisionDTOPtr inspectionVisionDTO2(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO2->ExposureTime = 8;
                inspectionVisionDTO2->Gain = 2;
                inspectionVisionDTO2->Gamma = 1;
                inspectionVisionDTO2->BlackLevel = 3;
                inspectionVisionDTO2->Sharpness = 4;
                inspectionVisionDTO2->GammaEnable = false;
                inspectionVisionDTO2->ZoomValue = 1;

                _variant_t mData2(inspectionVisionDTO2, true);
                _machineAMQPHandler->PublishToGeneral(mData2, _master->System->ProcessSystem->InspectionVision->Content);
            }
            else if (str == "5")
            {
                IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO->Acquisition = false;

                _variant_t mData(inspectionVisionDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->Live);
            }
            else if (str == "6")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->ExecutingBtn = (MotionUIElement)1;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->ExecutingBtnStatus);
            }
            else if (str == "7")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->ExecutingBtn = (MotionUIElement)2;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->ExecutingBtnStatus);
            }
            else if (str == "8")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->ExecutingBtn = (MotionUIElement)0;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->ExecutingBtnStatus);
            }
            else if (str == "9")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->EnableBtnIsActive = true;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->EnableBtnStatus);
            }
            else if (str == "10")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->EnableBtnIsActive = false;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->EnableBtnStatus);
            }
            else if (str == "11")
            {
                IInspectionVisionDTOPtr inspectionVisionDTO(__uuidof(InspectionVisionDTO));
                inspectionVisionDTO->TransactionX = 1;
                inspectionVisionDTO->TransactionY = 2;
                inspectionVisionDTO->Rotation = 3;
                inspectionVisionDTO->ScaleX = 4;
                inspectionVisionDTO->ScaleY = 5;
                inspectionVisionDTO->FovX = 6;
                inspectionVisionDTO->FovY = 7;

                _variant_t mData(inspectionVisionDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->CalibrationResult);
            }
            else if (str == "12")
            {
                std::vector<IInspectionVisionDTOPtr> inspectionVisionDTOPtrArray;

                IInspectionVisionDTOPtr inspectionVisionDTOPtr(__uuidof(InspectionVisionDTO));
                inspectionVisionDTOPtr->CalibrationModelIdSelection = "String1";
                IInspectionVisionDTOPtr inspectionVisionDTOPtr1(__uuidof(InspectionVisionDTO));
                inspectionVisionDTOPtr1->CalibrationModelIdSelection = "String222";
                IInspectionVisionDTOPtr inspectionVisionDTOPtr2(__uuidof(InspectionVisionDTO));
                inspectionVisionDTOPtr2->CalibrationModelIdSelection = "String333";

                inspectionVisionDTOPtrArray.push_back(inspectionVisionDTOPtr);
                inspectionVisionDTOPtrArray.push_back(inspectionVisionDTOPtr1);
                inspectionVisionDTOPtrArray.push_back(inspectionVisionDTOPtr2);

                SAFEARRAYBOUND bound;
                bound.lLbound = 0;
                bound.cElements = inspectionVisionDTOPtrArray.size();
                SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, inspectionVisionDTOPtrArray.size());

                for (size_t i = 0; i < inspectionVisionDTOPtrArray.size(); ++i) {
                    IUnknown* pUnknown = nullptr;
                    inspectionVisionDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
                    SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
                    pUnknown->Release();
                }

                _variant_t mData;
                mData.vt = VT_ARRAY | VT_UNKNOWN;
                mData.parray = safeArray;

                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->CalibrationModelIdSelection);
            }
            else if (str == "13") {
                std::cout << "\n Publish Ellipse ROIs" << std::endl;

                std::vector<IEllipseROIDTOPtr> ellipseROIDTOPtrArray;

                for (int i = 0; i < 5; ++i) {
                    IEllipseROIDTOPtr ellipseROIDTOPtr(__uuidof(EllipseROIDTO));
                    ellipseROIDTOPtr->RowCenter = 267.5 + (i*20);
                    ellipseROIDTOPtr->ColumnCenter = 317.0 + (i * 20);
                    ellipseROIDTOPtr->Row1 = 202.0;
                    ellipseROIDTOPtr->Column1 = 267.0;
                    ellipseROIDTOPtr->Radius1 = 65.5,
                    ellipseROIDTOPtr->Radius2 = 50.0;
                    ellipseROIDTOPtr->Score = 10.0;
                    ellipseROIDTOPtrArray.push_back(ellipseROIDTOPtr);
                }

                SAFEARRAYBOUND bound;
                bound.lLbound = 0;
                bound.cElements = ellipseROIDTOPtrArray.size();
                SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, ellipseROIDTOPtrArray.size());

                for (size_t i = 0; i < ellipseROIDTOPtrArray.size(); ++i) {
                    IUnknown* pUnknown = nullptr;
                    ellipseROIDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
                    SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
                    pUnknown->Release();
                }

                _variant_t mData;
                mData.vt = VT_ARRAY | VT_UNKNOWN;
                mData.parray = safeArray;

                _machineAMQPHandler->PublishToGeneral(mData, _master->System->ProcessSystem->InspectionVision->EllipseROIResultId);
            }
            else if (str == "14")
            {
                IMasterDTOPtr masterDTO(__uuidof(MasterDTO));
                masterDTO->MessageContent = "Test receiving the messages sent by MachineApp";

                _variant_t mData(masterDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->Content);
            }
            //else if (str == "15")
            //{
            //    bool _dipose = false;
            //    int i = 0;
            //    int imageID = 0;

            //    cv::Mat img;
            //    vector<unsigned char> imageData;
            //    vector<unsigned char> imageData1;
            //    vector<unsigned char> imageData2;
            //    vector<unsigned char> imageData3;

            //    try
            //    {
            //       // STAMP_LOG("load image1 imread");
            //        img = cv::imread("C:\\Images\\test123.bmp");
            //        //STAMP_LOG("load image1 imencode");
            //        cv::imencode(".bmp", img, imageData1);
            //       // STAMP_LOG("load image1 imencode after");

            //        //STAMP_LOG("load image2 imread");
            //        img = cv::imread("C:\\Images\\test234.bmp");
            //        //STAMP_LOG("load image2 imencode");
            //        cv::imencode(".bmp", img, imageData2);

            //        img = cv::imread("C:\\Images\\test345.bmp");
            //        cv::imencode(".bmp", img, imageData3);
            //       
            //    }
            //    catch (exception ex)
            //    {
            //        STAMP_LOG("exception " + std::string(ex.what()));
            //    }
            //    
            //    std::chrono::high_resolution_clock::time_point t1, t2, t3;

            //    while (!_dipose)
            //    {
            //        //::cout << "\n Vision Camera on live" << std::endl;
            //        //std::vector<SAFEARRAY*> safeArrays;

            //        //std::cout << "\n Reading image from files" << "\n";
            //        
            //        //std::cout << "\n Reading image from files" << "\n";
            //       
            //        if (i == 0)
            //        {
            //            imageData = imageData1;
            //            //safeArrays.push_back(VectorToSafeArray(imageData1));
            //        }
            //        else if (i == 1)
            //        {
            //            imageData = imageData2;
            //            //safeArrays.push_back(VectorToSafeArray(imageData2));
            //        }
            //        else if (i == 2)
            //        {
            //            imageData = imageData3;
            //            //safeArrays.push_back(VectorToSafeArray(imageData3));
            //        }

            //        /*  vector<unsigned char> imageData = ReadImageAsByteArray(folder);
            //          safeArrays.push_back(VectorToSafeArray(imageData));*/
            //        //STAMP_LOG("create DTO");
            //        IImageStreamDTOPtr imageStreamDTO(__uuidof(ImageStreamDTO)); 
            //        

            //        //for (size_t i = 0; i < safeArrays.size(); ++i)
            //        {
            //            t1 = std::chrono::high_resolution_clock::now();
            //            imageStreamDTO->StreamData = VectorToSafeArray(imageData);//safeArrays[0];
            //            imageStreamDTO->timeStamp = getCurrentDateTime().c_str();
            //            imageStreamDTO->ImageID = std::to_string(imageID++).c_str();
            //            _variant_t mData1(imageStreamDTO, true);
            //            t2 = std::chrono::high_resolution_clock::now();
            //            _machineAMQPHandler->PublishToStream_2(mData1, _machineAMQPHandler->StreamVisionRoutingKey2);

            //            //_machineAMQPHandler->PublishToStream(safeArrays[0], _machineAMQPHandler->StreamVisionRoutingKey);
            //             //std::cout << "\n Send Image no " << i + << std::endl;
            //            //std::this_thread::sleep_for(std::chrono::milliseconds(100));
            //            //safeArrays.clear();
            //        }
            //        t3 = std::chrono::high_resolution_clock::now();

            //        auto duration21 = std::chrono::duration_cast<std::chrono::microseconds> (t2 - t1);
            //        auto duration32 = std::chrono::duration_cast<std::chrono::microseconds> (t3 - t2);
            //        STAMP_LOG("duration: " + std::to_string(duration21.count()) + "us, " + std::to_string(duration32.count()) + "us");
            //        cout << "duration:" << duration21.count() << "us" <<duration32.count() << "us\n";
            //        i++;
            //        if (i == 3)
            //        {
            //            i = 0;
            //        }
            //    }
            //}
            else if (str == "15") {
                std::async(std::launch::async, []() {
                    std::cout << "\n Vision Camera on live" << std::endl;
                    std::vector<SAFEARRAY*> safeArrays;

                    string folderPath = "C:\\Sioux Release Code\\20240607-7a485b6-testing\\mono8";
                    std::cout << "\n Reading images from file" << "\n";

                    for (const auto& entry : fs::directory_iterator(folderPath)) {
                        if (entry.is_regular_file()) {
                            // Get the path of the image file
                            string filename = entry.path().string();
                            // Read image file into a byte array
                            vector<unsigned char> imageData = ReadImageAsByteArray(filename);
                            safeArrays.push_back(VectorToSafeArray(imageData));
                        }
                    }

                    std::vector<IImageStreamDTOPtr> imageStreamDTOPtrArray;
                    while (true)
                    {
                        for (size_t i = 0; i < safeArrays.size(); ++i) {
                            std::chrono::system_clock::time_point now = std::chrono::system_clock::now();
                            auto duration_ms = std::chrono::duration_cast<std::chrono::milliseconds>(now.time_since_epoch());
                            double timestamp = duration_ms.count();

                            IImageStreamDTOPtr imageStreamDTO(__uuidof(ImageStreamDTO));
                            /*imageStreamDTO->FullImageWidth = 7920;
                            imageStreamDTO->FullImageHeight = 6004;*/
                            imageStreamDTO->ScrollX = static_cast<int>(round(3710 / (i + 1)));
                            imageStreamDTO->ScrollY = static_cast<int>(round(2752 / (i+1)));
                            imageStreamDTO->Ratio = 1+i;
                           /* imageStreamDTO->ScrollX = 0;
                            imageStreamDTO->ScrollY = 0;*/
                            //imageStreamDTO->Ratio = 1;
                            imageStreamDTO->ImageBytes = safeArrays[i];
                            //imageStreamDTO->Timestamp = timestamp;

                            _variant_t mData(imageStreamDTO, true);
                            _machineAMQPHandler->PublishToStream(mData);

                            std::cout << "\n Send image no " << i + 1 << std::endl;
                            //std::this_thread::sleep_for(std::chrono::milliseconds(1000));
                        }
                    }
                    });
                    }
            else if (str == "16")
            {
                IMotionAxisControlDTOPtr motionAxisControlDTO(__uuidof(MotionAxisControlDTO));
                motionAxisControlDTO->ActualMovePos = 567;
                motionAxisControlDTO->ActualVelPos = 678;
                motionAxisControlDTO->ActualAcclPos = 7;
                motionAxisControlDTO->ActualJerkPos = 8;
                motionAxisControlDTO->IsRelPos = true;

                _variant_t mData(motionAxisControlDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->Motion->MotionAxisControl->MotionParamUpdates);

                }
            else if (str == "17")
            {
                IDonorLiftingModuleDTOPtr donorLiftingModuleDTO(__uuidof(DonorLiftingModuleDTO));
                donorLiftingModuleDTO->XAxisPosition = 11.22;
                donorLiftingModuleDTO->YAxisPosition = 4.56;
                donorLiftingModuleDTO->ZAxisPosition = 77.8899;
                donorLiftingModuleDTO->IsDonorChuckVacuumEnabled = true;

                _variant_t mData(donorLiftingModuleDTO, true);
                _machineAMQPHandler->PublishToGeneral(mData, _master->System->CausewaySystem->DonorLiftingModule->Content);

            }
        }
    }
}