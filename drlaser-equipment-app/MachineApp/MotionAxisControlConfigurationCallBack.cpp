#include "MotionAxisControlConfigurationCallBack.h"
#include <atlcomcli.h>

MotionAxisControlConfigurationCallBack::MotionAxisControlConfigurationCallBack(RPCQueueHandler* handler, IMasterPtr master, IMachineMessageHandlerPtr machineAMQPHandler)
	: _master(master), m_handler(*handler), _machineAMQPHandler(machineAMQPHandler)
{
	m_handler.AddCallback(_master->Configuration->MotionAxisControl,
		[this](BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data) { InitConfigCallback(replyQueue, correlationId, deliveryTag, data); });

}

void MotionAxisControlConfigurationCallBack::InitConfigCallback(BSTR replyQueue, BSTR correlationId, unsigned __int64 deliveryTag, _variant_t data)
{
	IConfigurationDTOPtr configurationDTO(__uuidof(ConfigurationDTO));
	configurationDTO->MachineId = "C2AAFFEF-4C61-46B7-8FD2-EFFD892A30D8";

	// Motion Axis Control Configuration DTO Start
	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO->AxisName = "ProTableX";
	motionAxisControlConfigurationDTO->PositionREL_DefaultValue = 1;
	motionAxisControlConfigurationDTO->PositionABS_DefaultValue = 2;
	motionAxisControlConfigurationDTO->Velocity_DefaultValue = 3;
	motionAxisControlConfigurationDTO->Accel_DefaultValue = 4;
	motionAxisControlConfigurationDTO->Jerk_DefaultValue = 5;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO2(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO2->AxisName = "ProsTableY";
	motionAxisControlConfigurationDTO2->PositionREL_DefaultValue = 6;
	motionAxisControlConfigurationDTO2->PositionABS_DefaultValue = 7;
	motionAxisControlConfigurationDTO2->Velocity_DefaultValue = 8;
	motionAxisControlConfigurationDTO2->Accel_DefaultValue = 9;
	motionAxisControlConfigurationDTO2->Jerk_DefaultValue = 10;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO3(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO3->AxisName = "ProsUpLookCamZ";
	motionAxisControlConfigurationDTO3->PositionREL_DefaultValue = 11;
	motionAxisControlConfigurationDTO3->PositionABS_DefaultValue = 22;
	motionAxisControlConfigurationDTO3->Velocity_DefaultValue = 13;
	motionAxisControlConfigurationDTO3->Accel_DefaultValue = 14;
	motionAxisControlConfigurationDTO3->Jerk_DefaultValue = 15;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO4(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO4->AxisName = "ProsDownLookCamZ";
	motionAxisControlConfigurationDTO4->PositionREL_DefaultValue = 16;
	motionAxisControlConfigurationDTO4->PositionABS_DefaultValue = 17;
	motionAxisControlConfigurationDTO4->Velocity_DefaultValue = 18;
	motionAxisControlConfigurationDTO4->Accel_DefaultValue = 19;
	motionAxisControlConfigurationDTO4->Jerk_DefaultValue = 20;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO5(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO5->AxisName = "CaswayDonorLifterX";
	motionAxisControlConfigurationDTO5->PositionREL_DefaultValue = 21;
	motionAxisControlConfigurationDTO5->PositionABS_DefaultValue = 22;
	motionAxisControlConfigurationDTO5->Velocity_DefaultValue = 23;
	motionAxisControlConfigurationDTO5->Accel_DefaultValue = 24;
	motionAxisControlConfigurationDTO5->Jerk_DefaultValue = 25;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO6(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO6->AxisName = "CaswayDonorLifterY";
	motionAxisControlConfigurationDTO6->PositionREL_DefaultValue = 26;
	motionAxisControlConfigurationDTO6->PositionABS_DefaultValue = 27;
	motionAxisControlConfigurationDTO6->Velocity_DefaultValue = 28;
	motionAxisControlConfigurationDTO6->Accel_DefaultValue = 29;
	motionAxisControlConfigurationDTO6->Jerk_DefaultValue = 30;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO7(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO7->AxisName = "CaswayDonorLifterZ";
	motionAxisControlConfigurationDTO7->PositionREL_DefaultValue = 31;
	motionAxisControlConfigurationDTO7->PositionABS_DefaultValue = 32;
	motionAxisControlConfigurationDTO7->Velocity_DefaultValue = 33;
	motionAxisControlConfigurationDTO7->Accel_DefaultValue = 34;
	motionAxisControlConfigurationDTO7->Jerk_DefaultValue = 35;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO8(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO8->AxisName = "CaswaySubLifterZ";
	motionAxisControlConfigurationDTO8->PositionREL_DefaultValue = 36;
	motionAxisControlConfigurationDTO8->PositionABS_DefaultValue = 37;
	motionAxisControlConfigurationDTO8->Velocity_DefaultValue = 38;
	motionAxisControlConfigurationDTO8->Accel_DefaultValue = 39;
	motionAxisControlConfigurationDTO8->Jerk_DefaultValue = 40;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO9(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO9->AxisName = "BuffTableX";
	motionAxisControlConfigurationDTO9->PositionREL_DefaultValue = 41;
	motionAxisControlConfigurationDTO9->PositionABS_DefaultValue = 42;
	motionAxisControlConfigurationDTO9->Velocity_DefaultValue = 43;
	motionAxisControlConfigurationDTO9->Accel_DefaultValue = 44;
	motionAxisControlConfigurationDTO9->Jerk_DefaultValue = 45;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO10(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO10->AxisName = "BuffTableY";
	motionAxisControlConfigurationDTO10->PositionREL_DefaultValue = 46;
	motionAxisControlConfigurationDTO10->PositionABS_DefaultValue = 47;
	motionAxisControlConfigurationDTO10->Velocity_DefaultValue = 48;
	motionAxisControlConfigurationDTO10->Accel_DefaultValue = 49;
	motionAxisControlConfigurationDTO10->Jerk_DefaultValue = 50;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO11(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO11->AxisName = "BuffTableT";
	motionAxisControlConfigurationDTO11->PositionREL_DefaultValue = 51;
	motionAxisControlConfigurationDTO11->PositionABS_DefaultValue = 52;
	motionAxisControlConfigurationDTO11->Velocity_DefaultValue = 53;
	motionAxisControlConfigurationDTO11->Accel_DefaultValue = 54;
	motionAxisControlConfigurationDTO11->Jerk_DefaultValue = 55;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO12(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO12->AxisName = "BuffUpLookCamZ";
	motionAxisControlConfigurationDTO12->PositionREL_DefaultValue = 56;
	motionAxisControlConfigurationDTO12->PositionABS_DefaultValue = 57;
	motionAxisControlConfigurationDTO12->Velocity_DefaultValue = 58;
	motionAxisControlConfigurationDTO12->Accel_DefaultValue = 59;
	motionAxisControlConfigurationDTO12->Jerk_DefaultValue = 60;

	IMotionAxisControlConfigurationDTOPtr motionAxisControlConfigurationDTO13(__uuidof(MotionAxisControlConfigurationDTO));
	motionAxisControlConfigurationDTO13->AxisName = "BuffThinkFocusZ";
	motionAxisControlConfigurationDTO13->PositionREL_DefaultValue = 61;
	motionAxisControlConfigurationDTO13->PositionABS_DefaultValue = 62;
	motionAxisControlConfigurationDTO13->Velocity_DefaultValue = 63;
	motionAxisControlConfigurationDTO13->Accel_DefaultValue = 64;
	motionAxisControlConfigurationDTO13->Jerk_DefaultValue = 65;

	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO2);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO3);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO4);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO5);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO6);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO7);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO8);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO9);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO10);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO11);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO12);
	configurationDTO->AddMotionAxisControlConfig(motionAxisControlConfigurationDTO13);
	// Motion Axis Control Configuration DTO End

	_variant_t config(configurationDTO, true);

	_machineAMQPHandler->Reply(replyQueue, correlationId, deliveryTag, _master->Configuration->MotionAxisControl, config);
}
