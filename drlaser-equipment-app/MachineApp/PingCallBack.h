#pragma once
#include "RPCQueueHandler.h"
#include <list>

class PingCallBack
{
public:
	PingCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
	IMasterPtr _master;
	IMachineMessageHandlerPtr _machineAMQPHandler;
	RPCQueueHandler& m_handler;

	void ReplyPingCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};