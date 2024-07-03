#include "PingCallBack.h"

PingCallBack::PingCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_machineAMQPHandler->PingRoutingKey,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { ReplyPingCallBack(replyQueue, correlationId, deliveryTag, data); });
}

void PingCallBack::ReplyPingCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) {
	_machineAMQPHandler->BasicAck(deliveryTag, false);

	_machineAMQPHandler->PublishToGeneral(false, _machineAMQPHandler->PingRoutingKey);
}