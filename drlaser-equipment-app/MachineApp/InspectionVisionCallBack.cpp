#include "InspectionVisionCallBack.h"
#include <atlcomcli.h>
#include "Helper.h"

InspectionVisionCallBack::InspectionVisionCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->Connect,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InspectionVisionConnectCallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->Trigger,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InspectionVisionTriggerCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->ImageDTO,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InspectionVisionImageCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->ROIDTO,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { ROICallback(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->RegionDTO,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { SearchRegionCallback(replyQueue, correlationId, deliveryTag, data); });

	m_handler.AddCallback(_master->System->ProcessSystem->InspectionVision->SaveResultCommandId,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { ROISaveResultCallback(replyQueue, correlationId, deliveryTag, data); });
}

void InspectionVisionCallBack::InspectionVisionConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Vision Camera connected" << std::endl;

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void InspectionVisionCallBack::InspectionVisionTriggerCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
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

void InspectionVisionCallBack::InspectionVisionCameraParamsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n CameraParams Fetch received" << std::endl;

	ICameraParamsDTOPtr cameraDTO(__uuidof(CameraParamsDTO));
	cameraDTO->Exposure = 2.3;
	cameraDTO->Light = true;

	_variant_t mData(cameraDTO, true);
	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->System->Vision->CameraParams, mData);
}

void InspectionVisionCallBack::InspectionVisionImageCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "Zoom received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}

void InspectionVisionCallBack::ROICallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "ROI received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}
void InspectionVisionCallBack::SearchRegionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "Search Region received: " << restr;
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}
void InspectionVisionCallBack::ROISaveResultCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();

	std::cout << "ROISaveResultCommand receive";
	std::cout << "\n";

	_machineAMQPHandler->BasicAck(deliveryTag, false);
}
