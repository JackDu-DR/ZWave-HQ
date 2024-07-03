#pragma once
#include "RPCQueueHandler.h"

class ProcessTableConfigurationCallBack
{
public:
	ProcessTableConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
	IMasterPtr _master;
	IMachineMessageHandlerPtr _machineAMQPHandler;
	RPCQueueHandler& m_handler;

	void InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};