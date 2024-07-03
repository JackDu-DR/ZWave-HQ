#pragma once
#include "RPCQueueHandler.h"

class InspectionVisionCallBack
{
public:
	InspectionVisionCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
	IMasterPtr _master;
	IMachineMessageHandlerPtr _machineAMQPHandler;
	RPCQueueHandler& m_handler;

	void InspectionVisionConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void InspectionVisionTriggerCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void InspectionVisionCameraParamsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);

	void InspectionVisionExposureCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void InspectionVisionLightCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void InspectionVisionImageCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void ROICallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void SearchRegionCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void ROISaveResultCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};