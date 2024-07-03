#pragma once
#include <iostream>
#include "def.h"
#include <map>
#include <vector>
#include <functional>

#import "CommonLib.tlb"

using namespace CommonLib;

class RPCQueueHandler : public ISubscribedMessage
{
public:
    RPCQueueHandler();
    virtual ~RPCQueueHandler();

    using CallbackFunction = std::function<void(BSTR, BSTR, unsigned __int64, _variant_t)>;

    HRESULT STDMETHODCALLTYPE get_QueueName(BSTR* pRetVal) override;

    HRESULT STDMETHODCALLTYPE put_QueueName(BSTR pRetVal) override;

    void AddCallback(_bstr_t id, const CallbackFunction& callback);

    HRESULT STDMETHODCALLTYPE raw_Callback(/*[in]*/ BSTR replyQueue, /*[in]*/ BSTR correlationId, /*[in]*/ unsigned __int64 deliveryTag, /*[in]*/ BSTR data) override;

    //IUnknown implementation
    HRESULT STDMETHODCALLTYPE QueryInterface(REFIID iid, void** ppvObject);

    //IUnknown implementation
    ULONG STDMETHODCALLTYPE AddRef();

    //IUnknown implementation
    ULONG STDMETHODCALLTYPE Release();

private:
    CallbackFunction m_callback;
    IMachineMessageHandlerPtr _machineAMQPHandler;
    DWORD m_dwRefCount;
    std::map<std::string, MyCallback> m_callbackMap;
    std::vector<std::tuple<std::string, MyCallback>> m_vectors;
    std::vector<std::tuple<std::string, CallbackFunction>> m_vectors1;
};


