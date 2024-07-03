#include "MotionCallBack.h"
#include <atlcomcli.h>
#include "Helper.h"
#include "CriticalAction.h"

MotionCallBack::MotionCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->System->Motion->MotionControl->Profiles,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlFetchProfilesCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Motion->MotionControl->Logs,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlFetchLogsCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Motion->MotionControl->Kinematics,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlFetchKinematicsCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Motion->MotionControl->Calculate,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlCalculateCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Motion->MotionControl->Start,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlStartCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Motion->MotionControl->Stop,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionControlStopCallback(replyQueue, correlationId, deliveryTag, data); });
}

void MotionCallBack::MotionControlFetchProfilesCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control fetch profiles" << std::endl;

    std::vector<IMotionProfileDTOPtr> motionProfileDTOPtrArray;

    for (int i = 0; i < 5; ++i) {
        IMotionProfileDTOPtr motionProfileDTOPtr(__uuidof(MotionProfileDTO));
		motionProfileDTOPtr->Name = "_axis_no_";
		motionProfileDTOPtr->Distance = i + 2;
		motionProfileDTOPtr->Time = i + 3;
		motionProfileDTOPtr->Velocity = i + 1 * 2;
		motionProfileDTOPtr->Acceleration = i + 1 * 3;
		motionProfileDTOPtr->Jerk = i + 1 * 4;
		motionProfileDTOPtrArray.push_back(motionProfileDTOPtr);
    }

    SAFEARRAYBOUND bound;
    bound.lLbound = 0;
    bound.cElements = motionProfileDTOPtrArray.size();
    SAFEARRAY* safeArray = SafeArrayCreateVector(VT_UNKNOWN, 0, motionProfileDTOPtrArray.size());

    for (size_t i = 0; i < motionProfileDTOPtrArray.size(); ++i) {
        IUnknown* pUnknown = nullptr;
		motionProfileDTOPtrArray[i]->QueryInterface(IID_IUnknown, reinterpret_cast<void**>(&pUnknown));
        SafeArrayPutElement(safeArray, (LONG*)&i, pUnknown);
        pUnknown->Release();
    }

    _variant_t mData;
    mData.vt = VT_ARRAY | VT_UNKNOWN;
    mData.parray = safeArray;

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Motion->MotionControl->Profiles, mData);
}

void MotionCallBack::MotionControlFetchLogsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control fetch logs" << std::endl;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void MotionCallBack::MotionControlFetchKinematicsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control fetch current kinematics" << std::endl;

	IMotionKinematicsDTOPtr motionKinematicsDTO(__uuidof(MotionKinematicsDTO));
	motionKinematicsDTO->Velocity = 1.0;
	motionKinematicsDTO->Acceleration = 2.0;
	motionKinematicsDTO->Jerk = 3.0;

	std::cout
		<< "\Sendback current kinematics data: "
		<< "\n-Velocity = " << motionKinematicsDTO->Velocity
		<< "\n-Acceleration = " << motionKinematicsDTO->Acceleration
		<< "\n-Jerk = " << motionKinematicsDTO->Jerk
		<< std::endl;

	_variant_t mData(motionKinematicsDTO, true);
	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Motion->MotionControl->Kinematics, mData);
}

void MotionCallBack::MotionControlCalculateCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control calculate command" << std::endl;

	IMotionTestDTOPtr motionTestDTO(__uuidof(MotionTestDTO));
	motionTestDTO->LoadDataFromJson(data.bstrVal);
	std::cout
		<< "\Received data for calculate: "
		<< "\n-Name = " << motionTestDTO->Name
		<< "\n-Point 1 = " << motionTestDTO->PointOne
		<< "\n-Point 1 delay = " << motionTestDTO->PointOneDelay
		<< "\n-Point 2 = " << motionTestDTO->PointTwo
		<< "\n-Point 2 delay = " << motionTestDTO->PointTwoDelay
		<< "\n-No. of cycles = " << motionTestDTO->NoOfCycles
		<< "\n-Velocity = " << motionTestDTO->Velocity
		<< "\n-Acceleration = " << motionTestDTO->Acceleration
		<< "\n-Jerk = " << motionTestDTO->Jerk
		<< std::endl;

	IMotionCalculateDTOPtr motionTestCalculateDTO(__uuidof(MotionCalculateDTO));
	motionTestCalculateDTO->PointOneEstTime = 1.0;
	motionTestCalculateDTO->PointTwoEstTime = 2.0;
	motionTestCalculateDTO->TotalEstCycleTime = 5.0;

	std::cout
		<< "\nSendback calculated data: "
		<< "\n-Point 1 estimate time = " << motionTestCalculateDTO->PointOneEstTime << " seconds"
		<< "\n-Point 2 estimate time = " << motionTestCalculateDTO->PointTwoEstTime << " seconds"
		<< "\n-Estimate cycle time = " << motionTestCalculateDTO->TotalEstCycleTime << " seconds"
		<< std::endl;

	_variant_t mData(motionTestCalculateDTO, true);
	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Motion->MotionControl->Calculate, mData);
}

void MotionCallBack::MotionControlStartCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control start command" << std::endl;
	IMotionTestDTOPtr motionTestDTO(__uuidof(MotionTestDTO));
	motionTestDTO->LoadDataFromJson(data.bstrVal);
	std::cout
		<< "\Received data for start motion: "
		<< "\n-Name = " << motionTestDTO->Name
		<< "\n-Point 1 = " << motionTestDTO->PointOne
		<< "\n-Point 1 delay = " << motionTestDTO->PointOneDelay
		<< "\n-Point 2 = " << motionTestDTO->PointTwo
		<< "\n-Point 2 delay = " << motionTestDTO->PointTwoDelay
		<< "\n-No. of cycles = " << motionTestDTO->NoOfCycles
		<< "\n-Velocity = " << motionTestDTO->Velocity
		<< "\n-Acceleration = " << motionTestDTO->Acceleration
		<< "\n-Jerk = " << motionTestDTO->Jerk
		<< std::endl;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
	_machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionControlStart, true), _master->CriticalControl->Update);
}

void MotionCallBack::MotionControlStopCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Motion Control stop command" << std::endl;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
	_machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionControlStart, false), _master->CriticalControl->Update);
}
