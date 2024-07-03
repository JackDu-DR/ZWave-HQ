#pragma once
#include "RPCQueueHandler.h"

class VisionCallBack
{
public:
	VisionCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
	IMasterPtr _master;
	IMachineMessageHandlerPtr _machineAMQPHandler;
	RPCQueueHandler& m_handler;

	void VisionConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void VisionTriggerCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void VisionCameraParamsCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);

	void VisionExposureCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void VisionLightCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void VisionImageCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void ROICallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void ROISaveResultCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
	void MotionMovedCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};