#include "CriticalActionCallback.h"
#include <atlcomcli.h>
#include "CriticalAction.h"

CriticalActionCallback::CriticalActionCallback(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
    : _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
    m_handler.AddCallback(_master->CriticalControl->Fetch,
        [this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { CriticalActionFetchCallback(replyQueue, correlationId, deliveryTag, data); });
}

void CriticalActionCallback::CriticalActionFetchCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
    std::cout << "\n CriticalAction Fetch received" << std::endl;

    _machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->CriticalControl->Fetch, CreateCriticalmData(CriticalType_MotionAxisGo, true));
}
