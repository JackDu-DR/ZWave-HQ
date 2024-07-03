#include "InspectionVisionConfigurationCallBack.h"
#include <atlcomcli.h>

InspectionVisionConfigurationCallBack::InspectionVisionConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Configuration->InspectionVision,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InitConfigCallback(replyQueue, correlationId, deliveryTag, data); });

}

void InspectionVisionConfigurationCallBack::InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	IConfigurationDTOPtr configurationDTO(__uuidof(ConfigurationDTO));
	configurationDTO->MachineId = "C2AAFFEF-4C61-46B7-8FD2-EFFD892A30D8";

	// Inspection Vision Configuration DTO Start
	IInspectionVisionConfigurationDTOPtr inspectionVisionConfigurationDTO(__uuidof(InspectionVisionConfigurationDTO));
	inspectionVisionConfigurationDTO->CameraName = "ProsUpLookCam";
	inspectionVisionConfigurationDTO->ZoomValue_Min = 1;
	inspectionVisionConfigurationDTO->ZoomValue_Max = 10;
	inspectionVisionConfigurationDTO->CameraMaxSize_X = 7920;
	inspectionVisionConfigurationDTO->CameraMaxSize_Y = 6004;
	inspectionVisionConfigurationDTO->ExposureTime_Min = 0;
	inspectionVisionConfigurationDTO->ExposureTime_Max = 10;
	inspectionVisionConfigurationDTO->ExposureTime_ValueType = "us";
	inspectionVisionConfigurationDTO->Gain_Min = 1;
	inspectionVisionConfigurationDTO->Gain_Max = 15;
	inspectionVisionConfigurationDTO->Gain_ValueType = "db";
	inspectionVisionConfigurationDTO->Gamma_Min = 0.0937;
	inspectionVisionConfigurationDTO->Gamma_Max = 2;
	inspectionVisionConfigurationDTO->Gamma_ValueType = "";
	inspectionVisionConfigurationDTO->BlackLevel_Min = 0;
	inspectionVisionConfigurationDTO->BlackLevel_Max = 255;
	inspectionVisionConfigurationDTO->BlackLevel_ValueType = "";
	inspectionVisionConfigurationDTO->Phi_Min = 0;
	inspectionVisionConfigurationDTO->Phi_Max = 360;
	inspectionVisionConfigurationDTO->Phi_ValueType = "";
	inspectionVisionConfigurationDTO->ShowSharpness = false;

	IInspectionVisionConfigurationDTOPtr inspectionVisionConfigurationDTO2(__uuidof(InspectionVisionConfigurationDTO));
	inspectionVisionConfigurationDTO2->CameraName = "ProsDownLookCam";
	inspectionVisionConfigurationDTO2->ZoomValue_Min = 1;
	inspectionVisionConfigurationDTO2->ZoomValue_Max = 4;
	inspectionVisionConfigurationDTO2->CameraMaxSize_X = 2048;
	inspectionVisionConfigurationDTO2->CameraMaxSize_Y = 2048;
	inspectionVisionConfigurationDTO2->ExposureTime_Min = 0;
	inspectionVisionConfigurationDTO2->ExposureTime_Max = 10;
	inspectionVisionConfigurationDTO2->ExposureTime_ValueType = "us";
	inspectionVisionConfigurationDTO2->Gain_Min = 1;
	inspectionVisionConfigurationDTO2->Gain_Max = 8;
	inspectionVisionConfigurationDTO2->Gain_ValueType = "db";
	inspectionVisionConfigurationDTO2->Gamma_Min = 0.45;
	inspectionVisionConfigurationDTO2->Gamma_Max = 1;
	inspectionVisionConfigurationDTO2->Gamma_ValueType = "";
	inspectionVisionConfigurationDTO2->Sharpness_Min = 1;
	inspectionVisionConfigurationDTO2->Sharpness_Max = 7;
	inspectionVisionConfigurationDTO2->Sharpness_ValueType = "";
	inspectionVisionConfigurationDTO2->BlackLevel_Min = -25.09;
	inspectionVisionConfigurationDTO2->BlackLevel_Max = 25;
	inspectionVisionConfigurationDTO2->BlackLevel_ValueType = "";
	inspectionVisionConfigurationDTO2->Phi_Min = 0;
	inspectionVisionConfigurationDTO2->Phi_Max = 360;
	inspectionVisionConfigurationDTO2->Phi_ValueType = "";
	inspectionVisionConfigurationDTO2->ShowSharpness = true;

	IInspectionVisionConfigurationDTOPtr inspectionVisionConfigurationDTO3(__uuidof(InspectionVisionConfigurationDTO));
	inspectionVisionConfigurationDTO3->CameraName = "BufferUpLookCam";
	inspectionVisionConfigurationDTO3->ZoomValue_Min = 1;
	inspectionVisionConfigurationDTO3->ZoomValue_Max = 4;
	inspectionVisionConfigurationDTO3->CameraMaxSize_X = 2048;
	inspectionVisionConfigurationDTO3->CameraMaxSize_Y = 2048;
	inspectionVisionConfigurationDTO3->ExposureTime_Min = 0;
	inspectionVisionConfigurationDTO3->ExposureTime_Max = 10;
	inspectionVisionConfigurationDTO3->ExposureTime_ValueType = "us";
	inspectionVisionConfigurationDTO3->Gain_Min = 1;
	inspectionVisionConfigurationDTO3->Gain_Max = 8;
	inspectionVisionConfigurationDTO3->Gain_ValueType = "db";
	inspectionVisionConfigurationDTO3->Gamma_Min = 0.45;
	inspectionVisionConfigurationDTO3->Gamma_Max = 1;
	inspectionVisionConfigurationDTO3->Gamma_ValueType = "";
	inspectionVisionConfigurationDTO3->Sharpness_Min = 1;
	inspectionVisionConfigurationDTO3->Sharpness_Max = 7;
	inspectionVisionConfigurationDTO3->Sharpness_ValueType = "";
	inspectionVisionConfigurationDTO3->BlackLevel_Min = -25.09;
	inspectionVisionConfigurationDTO3->BlackLevel_Max = 25;
	inspectionVisionConfigurationDTO3->BlackLevel_ValueType = "";
	inspectionVisionConfigurationDTO3->Phi_Min = 0;
	inspectionVisionConfigurationDTO3->Phi_Max = 360;
	inspectionVisionConfigurationDTO3->Phi_ValueType = "";
	inspectionVisionConfigurationDTO3->ShowSharpness = true;

	configurationDTO->AddInspectionVisionConfig(inspectionVisionConfigurationDTO);
	configurationDTO->AddInspectionVisionConfig(inspectionVisionConfigurationDTO2);
	configurationDTO->AddInspectionVisionConfig(inspectionVisionConfigurationDTO3);
	// Inspection Vision Configuration DTO End

	_variant_t config(configurationDTO, true);

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Configuration->InspectionVision, config);
}
