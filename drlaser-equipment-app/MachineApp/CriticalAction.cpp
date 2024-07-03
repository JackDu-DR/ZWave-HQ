#include "RPCQueueHandler.h"

_variant_t CreateCriticalmData(CriticalType criticalType, bool processing, int timeout = 15000) {
	ICriticalActionDTOPtr criticalActionDTO(__uuidof(CriticalActionDTO));
	criticalActionDTO->IsProcessing = processing;
	criticalActionDTO->_CriticalType = criticalType;
	criticalActionDTO->Timeout = timeout;

	_variant_t mData1(criticalActionDTO, true);

	return mData1;
}