#include "DonorLiftingModuleConfigurationCallBack.h"
#include <atlcomcli.h>

DonorLiftingModuleConfigurationCallBack::DonorLiftingModuleConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Configuration->DonorLiftingModule,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InitConfigCallback(replyQueue, correlationId, deliveryTag, data); });

}

void DonorLiftingModuleConfigurationCallBack::InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	IConfigurationDTOPtr configurationDTO(__uuidof(ConfigurationDTO));
	configurationDTO->MachineId = "C2AAFFEF-4C61-46B7-8FD2-EFFD892A30D8";

	// Donor Lifting Module Configuration DTO Start
	IDonorLiftingModuleConfigurationDTOPtr donorLiftingModuleConfigurationDTO(__uuidof(DonorLiftingModuleConfigurationDTO));
	donorLiftingModuleConfigurationDTO->AxisName = "Donor_Axis_X";
	donorLiftingModuleConfigurationDTO->ArrowContentLeft1DefaultValue = 1;
	donorLiftingModuleConfigurationDTO->ArrowContentLeft2DefaultValue = 0.1;
	donorLiftingModuleConfigurationDTO->ArrowContentLeft3DefaultValue = 0.001;
	donorLiftingModuleConfigurationDTO->ArrowContentRight1DefaultValue = 0.001;
	donorLiftingModuleConfigurationDTO->ArrowContentRight2DefaultValue = 0.1;
	donorLiftingModuleConfigurationDTO->ArrowContentRight3DefaultValue = 1;
	donorLiftingModuleConfigurationDTO->EntryValue_Min = 1;
	donorLiftingModuleConfigurationDTO->EntryValue_Max = 5;

	IDonorLiftingModuleConfigurationDTOPtr donorLiftingModuleConfigurationDTO1(__uuidof(DonorLiftingModuleConfigurationDTO));
	donorLiftingModuleConfigurationDTO1->AxisName = "Donor_Axis_Y";
	donorLiftingModuleConfigurationDTO1->ArrowContentLeft1DefaultValue = 1;
	donorLiftingModuleConfigurationDTO1->ArrowContentLeft2DefaultValue = 0.1;
	donorLiftingModuleConfigurationDTO1->ArrowContentLeft3DefaultValue = 0.001;
	donorLiftingModuleConfigurationDTO1->ArrowContentRight1DefaultValue = 0.001;
	donorLiftingModuleConfigurationDTO1->ArrowContentRight2DefaultValue = 0.1;
	donorLiftingModuleConfigurationDTO1->ArrowContentRight3DefaultValue = 1;
	donorLiftingModuleConfigurationDTO1->EntryValue_Min = 1;
	donorLiftingModuleConfigurationDTO1->EntryValue_Max = 10;

	IDonorLiftingModuleConfigurationDTOPtr donorLiftingModuleConfigurationDTO2(__uuidof(DonorLiftingModuleConfigurationDTO));
	donorLiftingModuleConfigurationDTO2->AxisName = "Donor_Axis_Z";
	donorLiftingModuleConfigurationDTO2->ArrowContentLeft1DefaultValue = 1;
	donorLiftingModuleConfigurationDTO2->ArrowContentLeft2DefaultValue = 2;
	donorLiftingModuleConfigurationDTO2->ArrowContentLeft3DefaultValue = 3;
	donorLiftingModuleConfigurationDTO2->ArrowContentRight1DefaultValue = 4;
	donorLiftingModuleConfigurationDTO2->ArrowContentRight2DefaultValue = 5;
	donorLiftingModuleConfigurationDTO2->ArrowContentRight3DefaultValue = 6;
	donorLiftingModuleConfigurationDTO2->EntryValue_Min = 1;
	donorLiftingModuleConfigurationDTO2->EntryValue_Max = 5;

	configurationDTO->AddDonorLiftingModuleConfig(donorLiftingModuleConfigurationDTO);
	configurationDTO->AddDonorLiftingModuleConfig(donorLiftingModuleConfigurationDTO1);
	configurationDTO->AddDonorLiftingModuleConfig(donorLiftingModuleConfigurationDTO2);
	// Donor Lifting Module Configuration DTO End


	_variant_t config(configurationDTO, true);

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Configuration->DonorLiftingModule, config);
}
