#pragma once
#include "RPCQueueHandler.h"

class LaserCallBack
{
public:
    LaserCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler);

private:
    IMasterPtr _master;
    IMachineMessageHandlerPtr _machineAMQPHandler;
    RPCQueueHandler& m_handler;

    void LaserFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);

    void LaserBasicFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBasicApplyCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBasicOutputCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBasicConnectCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBasicStandByCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);

    void LaserBurstApplyCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBurstPowerlockCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
    void LaserBurstFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data);
};