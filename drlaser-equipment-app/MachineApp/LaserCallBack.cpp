#include "LaserCallBack.h"
#include <atlcomcli.h>

LaserCallBack::LaserCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
     : _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler) 
 {
    m_handler.AddCallback(_master->System->Laser->Fetch,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserFetchCallback(replyQueue, correlationId, deliveryTag, data); });
    
    m_handler.AddCallback(_master->System->Laser->LaserBasic->Fetch,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBasicFetchCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBasic->Apply,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBasicApplyCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBasic->Output,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBasicOutputCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBasic->Connect,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBasicConnectCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBasic->StandBy,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBasicStandByCallback(replyQueue, correlationId, deliveryTag, data); });
    
    m_handler.AddCallback(_master->System->Laser->LaserBurst->Apply,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBurstApplyCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBurst->Powerlock,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBurstPowerlockCallback(replyQueue, correlationId, deliveryTag, data); });
    m_handler.AddCallback(_master->System->Laser->LaserBurst->Fetch,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { LaserBurstFetchCallback(replyQueue, correlationId, deliveryTag, data); });
}

void LaserCallBack::LaserFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "\n LaserInfoDTO Fetch received" << std::endl;

    ILaserInfoDTOPtr laserInfoDTO(__uuidof(LaserInfoDTO));
    laserInfoDTO->IsConnected = false;
    laserInfoDTO->Operation = LaserOperation_Operation;
    laserInfoDTO->Emission = true;
    laserInfoDTO->LaserPower = 80.7682;
    laserInfoDTO->Energy = 2006.342;
    laserInfoDTO->LaserPowerRange = LaserStatusRange_OutOfRange;
    laserInfoDTO->Frequency = 40.7682;
    laserInfoDTO->PulseDivider = 6.342;
    laserInfoDTO->FrequencyRange = LaserStatusRange_Warning;
    laserInfoDTO->WaveLength = 1024.532;
    laserInfoDTO->WaveLengthRange = LaserStatusRange_Unknown;

    _variant_t mData(laserInfoDTO, true);
    _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Laser->Fetch, mData);
}

 void LaserCallBack::LaserBasicFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     std::cout << "\n laserBasicDTO Fetch received" << std::endl;

     ILaserBasicDTOPtr laserBasicDTO(__uuidof(LaserBasicDTO));
     laserBasicDTO->IsOutputEnabled = true;
     laserBasicDTO->_PresetControl = PresetControl_One;
     laserBasicDTO->AttenuatorPercentage = 2.22;
     laserBasicDTO->PPDivider = 3.32;
     laserBasicDTO->PulseDuration = 4.42;

     _variant_t mData(laserBasicDTO, true);
     _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Laser->LaserBasic->Fetch, mData);
 }

 void LaserCallBack::LaserBasicApplyCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
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

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBasicOutputCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     CComBSTR temp(data.bstrVal);
     _bstr_t restr = temp.Detach();

     std::cout << "Output received: " << restr;
     std::cout << "\n";

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBasicConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     std::cout << "Connect command received";
     std::cout << "\n";

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBasicStandByCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     std::cout << "StandBy command received";
     std::cout << "\n";

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBurstApplyCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     ILaserBurstDTOPtr laserBurstDTO(__uuidof(LaserBurstDTO));
     laserBurstDTO->LoadDataFromJson(data.bstrVal);
     std::cout
         << "\nApply received: "
         << "\n-P = " << laserBurstDTO->P
         << "\n-N = " << laserBurstDTO->N
         << "\n-EnvelopeControlP = " << laserBurstDTO->EnvelopeControlP
         << "\n-EnvelopeControlN = " << laserBurstDTO->EnvelopeControlN
         << std::endl;

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBurstPowerlockCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     CComBSTR temp(data.bstrVal);
     _bstr_t restr = temp.Detach();

     std::cout << "Powerlock received: " << restr;
     std::cout << "\n";

     _machineAMQPHandler->BasicAck(deliveryTag, false);
 }

 void LaserCallBack::LaserBurstFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
 {
     std::cout << "\n LaserBurstFetchDTO Fetch received" << std::endl;

     ILaserBurstDTOPtr laserBurstDTO(__uuidof(LaserBurstDTO));
     laserBurstDTO->IsPowerlockEnabled = true;
     laserBurstDTO->P = 2.22;
     laserBurstDTO->N = 3.32;
     laserBurstDTO->EnvelopeControlP = 4.42;
     laserBurstDTO->EnvelopeControlN = 1.42;

     _variant_t mData(laserBurstDTO, true);
     _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Laser->LaserBurst->Content, mData);
 }