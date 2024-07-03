#pragma once
#include "RPCQueueHandler.h"

class CriticalActionCallback
{
public:
    CriticalActionCallback(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
    IMasterPtr _master;
    IMachineMessageHandlerPtr _machineAMQPHandler;
    RPCQueueHandler& m_handler;

    void CriticalActionFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};