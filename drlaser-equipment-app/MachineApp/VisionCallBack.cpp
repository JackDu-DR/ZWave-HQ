#include "VisionCallBack.h"
#include <atlcomcli.h>
#include "Helper.h"
#include "CriticalAction.h"

VisionCallBack::VisionCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->System->Vision->Connect,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionConnectCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Vision->Trigger,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionTriggerCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->Vision->Camera->Exposure,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionExposureCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->Vision->Camera->Light,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionLightCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->Vision->ImageDTO,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionImageCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->Vision->CameraParams,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { VisionCameraParamsCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->Vision->ROIDTO,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { ROICallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->Vision->SaveResultCommandId,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { ROISaveResultCallback(replyQueue, correlationId, deliveryTag, data); });
	
	m_handler.AddCallback(_master->System->Vision->MotionMovedId,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { MotionMovedCallback(replyQueue, correlationId, deliveryTag, data); });
}

void VisionCallBack::VisionConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Vision Camera connected" << std::endl;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void VisionCallBack::VisionTriggerCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Trigger received: \n";

	string filename = "C:\\Users\\viet.dinh\\Desktop\\sample\\mono12\\image0000182.tif";

	// Read the image file as a byte array
	vector<unsigned char> byteArray = ReadImageAsByteArray(filename);

	// Check if reading was successful
	if (byteArray.empty()) {
		cerr << "Failed to read image file: " << filename << endl;
	}

	_machineAMQPHandler->Reply_2(replyQueue, correlationId, deliveryTag, VectorToSafeArray(byteArray));
	std::cout << "\n Send trigger image ";
}

void VisionCallBack::VisionCameraParamsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n CameraParams Fetch received" << std::endl;

	ICameraParamsDTOPtr cameraDTO(__uuidof(CameraParamsDTO));
	cameraDTO->Exposure = 2.3;
	cameraDTO->Light = true;

	_variant_t mData(cameraDTO, true);
	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Vision->CameraParams, mData);
}

void VisionCallBack::VisionExposureCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "Exposure received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void VisionCallBack::VisionLightCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "Light received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void VisionCallBack::VisionImageCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "Zoom received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void VisionCallBack::ROICallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "ROI received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
	_machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionTeaching, true), _master->CriticalControl->Update);
}
void VisionCallBack::ROISaveResultCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "ROISaveResultCommand receive";
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void VisionCallBack::MotionMovedCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	IMotionMovedDTOPtr motionMovedDTO(__uuidof(MotionMovedDTO));
	motionMovedDTO->LoadDataFromJson(data.bstrVal);

	std::cout << "\Camera moving in ";
	std::cout << "\n- Transition X " << motionMovedDTO->TransitionX;
	std::cout << "\n- Transition Y " << motionMovedDTO->TransitionY;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
	std::this_thread::sleep_for(std::chrono::milliseconds(2000));
	std::cout << "\nCamera finished moving\n";
	_machineAMQPHandler->PublishToGeneral(CreateCriticalmData(CriticalType_MotionMoving, false), _master->CriticalControl->Update);
	std::cout << "\n";
}