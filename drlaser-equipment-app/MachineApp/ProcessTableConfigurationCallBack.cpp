#include "ProcessTableConfigurationCallBack.h"
#include <atlcomcli.h>

ProcessTableConfigurationCallBack::ProcessTableConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Configuration->ProcessTable,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InitConfigCallback(replyQueue, correlationId, deliveryTag, data); });

}

void ProcessTableConfigurationCallBack::InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	IConfigurationDTOPtr configurationDTO(__uuidof(ConfigurationDTO));
	configurationDTO->MachineId = "C2AAFFEF-4C61-46B7-8FD2-EFFD892A30D8";

	// Process Table Configuration DTO Start
	IProcessTableConfigurationDTOPtr processTableConfigurationDTO(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO->AxisName = "Pro_Table_Axis_X";
	processTableConfigurationDTO->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO->ArrowContentLeft2DefaultValue = 0.1;
	processTableConfigurationDTO->ArrowContentLeft3DefaultValue = 0.001;
	processTableConfigurationDTO->ArrowContentRight1DefaultValue = 0.001;
	processTableConfigurationDTO->ArrowContentRight2DefaultValue = 0.1;
	processTableConfigurationDTO->ArrowContentRight3DefaultValue = 1;
	processTableConfigurationDTO->EntryValue_Min = 0.01;
	processTableConfigurationDTO->EntryValue_Max = 5;

	IProcessTableConfigurationDTOPtr processTableConfigurationDTO1(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO1->AxisName = "Pro_Table_Axis_Y";
	processTableConfigurationDTO1->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO1->ArrowContentLeft2DefaultValue = 0.1;
	processTableConfigurationDTO1->ArrowContentLeft3DefaultValue = 0.001;
	processTableConfigurationDTO1->ArrowContentRight1DefaultValue = 0.001;
	processTableConfigurationDTO1->ArrowContentRight2DefaultValue = 0.1;
	processTableConfigurationDTO1->ArrowContentRight3DefaultValue = 1;
	processTableConfigurationDTO1->EntryValue_Min = 0.01;
	processTableConfigurationDTO1->EntryValue_Max = 10;

	IProcessTableConfigurationDTOPtr processTableConfigurationDTO2(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO2->AxisName = "Pro_Table_Tip_Tilt_TX";
	processTableConfigurationDTO2->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO2->ArrowContentLeft2DefaultValue = 2;
	processTableConfigurationDTO2->ArrowContentLeft3DefaultValue = 3;
	processTableConfigurationDTO2->ArrowContentRight1DefaultValue = 4;
	processTableConfigurationDTO2->ArrowContentRight2DefaultValue = 5;
	processTableConfigurationDTO2->ArrowContentRight3DefaultValue = 6;
	processTableConfigurationDTO2->EntryValue_Min = 0.01;
	processTableConfigurationDTO2->EntryValue_Max = 5;

	IProcessTableConfigurationDTOPtr processTableConfigurationDTO3(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO3->AxisName = "Pro_Table_Tip_Tilt_TY";
	processTableConfigurationDTO3->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO3->ArrowContentLeft2DefaultValue = 0.1;
	processTableConfigurationDTO3->ArrowContentLeft3DefaultValue = 0.001;
	processTableConfigurationDTO3->ArrowContentRight1DefaultValue = 0.001;
	processTableConfigurationDTO3->ArrowContentRight2DefaultValue = 0.1;
	processTableConfigurationDTO3->ArrowContentRight3DefaultValue = 1;
	processTableConfigurationDTO3->EntryValue_Min = 0.01;
	processTableConfigurationDTO3->EntryValue_Max = 5;

	IProcessTableConfigurationDTOPtr processTableConfigurationDTO4(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO4->AxisName = "Pro_Table_Tip_Tilt_Z";
	processTableConfigurationDTO4->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO4->ArrowContentLeft2DefaultValue = 0.1;
	processTableConfigurationDTO4->ArrowContentLeft3DefaultValue = 0.001;
	processTableConfigurationDTO4->ArrowContentRight1DefaultValue = 0.001;
	processTableConfigurationDTO4->ArrowContentRight2DefaultValue = 0.1;
	processTableConfigurationDTO4->ArrowContentRight3DefaultValue = 1;
	processTableConfigurationDTO4->EntryValue_Min = 0.01;
	processTableConfigurationDTO4->EntryValue_Max = 5;

	IProcessTableConfigurationDTOPtr processTableConfigurationDTO5(__uuidof(ProcessTableConfigurationDTO));
	processTableConfigurationDTO5->AxisName = "Pro_Table_Tip_Tilt_T";
	processTableConfigurationDTO5->ArrowContentLeft1DefaultValue = 1;
	processTableConfigurationDTO5->ArrowContentLeft2DefaultValue = 0.1;
	processTableConfigurationDTO5->ArrowContentLeft3DefaultValue = 0.001;
	processTableConfigurationDTO5->ArrowContentRight1DefaultValue = 0.001;
	processTableConfigurationDTO5->ArrowContentRight2DefaultValue = 0.1;
	processTableConfigurationDTO5->ArrowContentRight3DefaultValue = 1;
	processTableConfigurationDTO5->EntryValue_Min = 0.01;
	processTableConfigurationDTO5->EntryValue_Max = 5;

	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO);
	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO1);
	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO2);
	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO3);
	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO4);
	configurationDTO->AddProcessTableConfig(processTableConfigurationDTO5);
	// Process Table Configuration DTO End

	_variant_t config(configurationDTO, true);

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Configuration->ProcessTable, config);
}
