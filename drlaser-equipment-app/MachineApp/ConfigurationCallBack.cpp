#include "ConfigurationCallBack.h"
#include <atlcomcli.h>

ConfigurationCallBack::ConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Configuration->Self,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InitConfigCallback(replyQueue, correlationId, deliveryTag, data); });
}

void ConfigurationCallBack::InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	std::cout << "\n AxisConfiguration Fetch received" << std::endl;

	IAxisConfigDTOPtr axisConfigDTO(__uuidof(AxisConfigDTO));
	axisConfigDTO->AxisName = "Axis 1";
	axisConfigDTO->PositionRel = 0;
	axisConfigDTO->PositionAbs = 2;
	axisConfigDTO->PositionMin = 0;
	axisConfigDTO->PositionMax = 30;
	axisConfigDTO->Velocity = 2;
	axisConfigDTO->VelocityMin = 0;
	axisConfigDTO->VelocityMax = 20;
	axisConfigDTO->Jerk = 2;
	axisConfigDTO->JerkMin = 0;
	axisConfigDTO->JerkMax = 10;
	axisConfigDTO->Unit = "mm";

	IAxisConfigDTOPtr axisConfigDTO1(__uuidof(AxisConfigDTO));
	axisConfigDTO1->AxisName = "Axis 2";
	axisConfigDTO1->PositionRel = 2;
	axisConfigDTO1->PositionAbs = 0;
	axisConfigDTO1->PositionMin = 0;
	axisConfigDTO1->PositionMax = 10;
	axisConfigDTO1->Velocity = 2;
	axisConfigDTO1->VelocityMin = 0;
	axisConfigDTO1->VelocityMax = 20;
	axisConfigDTO1->Jerk = 2;
	axisConfigDTO1->JerkMin = 0;
	axisConfigDTO1->JerkMax = 30;
	axisConfigDTO1->Unit = "deg";

	IConfigurationDTOPtr configurationDTO(__uuidof(ConfigurationDTO));
	configurationDTO->MachineId = "C2AAFFEF-4C61-46B7-8FD2-EFFD892A30D8";
	configurationDTO->AddAxisConfig(axisConfigDTO);
	configurationDTO->AddAxisConfig(axisConfigDTO1);
	configurationDTO->CriticalActionTimeoutDefault = 15000;

	_variant_t config(configurationDTO, true);

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Configuration->Self, config);
}
