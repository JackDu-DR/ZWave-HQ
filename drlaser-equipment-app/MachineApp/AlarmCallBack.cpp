#include "AlarmCallBack.h"
#include "AlarmStorage.h"
#include <atlcomcli.h>
#include "Helper.h"

AlarmStorage* alarmStoragePtr;

AlarmCallBack::AlarmCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Alarm->Fetch,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { AlarmFetchCallBack(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->Alarm->Ack,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { AlarmAckCallBack(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->Alarm->Retry,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { AlarmRetryCallBack(replyQueue, correlationId, deliveryTag, data); });
	m_handler.AddCallback(_master->Alarm->Cancel,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { AlarmCancelCallBack(replyQueue, correlationId, deliveryTag, data); });
}

void AlarmCallBack::AlarmFetchCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n Alarm Fetched request received" << std::endl;
	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Alarm->Fetch, alarmStoragePtr->ToVariant());
	std::cout << "\n Send total of " << alarmStoragePtr->GetAlarms().size() << " alarms" << std::endl;
}

void AlarmCallBack::AlarmAckCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();
	_machineAMQPHandler->BasicAck(deliveryTag, false);

	std::cout << "\n Acknowlege alarm with Id " << restr << std::endl;
	_machineAMQPHandler->PublishToGeneral(restr, _master->Alarm->AcknowlegedAlarm);
	
	// Remove acknowleged alarm from machine alarm storage
	alarmStoragePtr->RemoveAlarm(restr);
}

void AlarmCallBack::AlarmRetryCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();
	_machineAMQPHandler->BasicAck(deliveryTag, false);

	auto result = true; // This result value should get from Retry action of machine

	IAlarmRetryResultDTOPtr alarmRetryResultPtr(__uuidof(AlarmRetryResultDTO));
	alarmRetryResultPtr->IsSuccess = result;
	alarmRetryResultPtr->AlarmId = restr;

	_variant_t mData(alarmRetryResultPtr, true);
	_machineAMQPHandler->PublishToGeneral(mData, _master->Alarm->RetriedAlarm);
	std::cout << "\n Retry alarm with Id " << restr << " " << (result ? " successfully" : " failed") << std::endl;

	// Remove alarm from machine alarm storage if retry successfully
	if (result) {
		alarmStoragePtr->RemoveAlarm(restr);
	}
}

void AlarmCallBack::AlarmCancelCallBack(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	CComBSTR temp(data.bstrVal);
	_bstr_t restr = temp.Detach();
	_machineAMQPHandler->BasicAck(deliveryTag, false);

	std::cout << "\n Cancel alarm with Id " << restr << std::endl;
	_machineAMQPHandler->PublishToGeneral(restr, _master->Alarm->CancelledAlarm);

	// Remove cancelled alarm from machine alarm storage
	alarmStoragePtr->RemoveAlarm(restr);
}
