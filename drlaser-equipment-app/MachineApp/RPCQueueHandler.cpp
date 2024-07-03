#include "RPCQueueHandler.h"

RPCQueueHandler::RPCQueueHandler()
{
    m_dwRefCount = 0;
    _machineAMQPHandler.CreateInstance(__uuidof(MachineMessageHandler));
}

RPCQueueHandler::~RPCQueueHandler()
{

}

HRESULT STDMETHODCALLTYPE RPCQueueHandler::get_QueueName(BSTR* pRetVal)
{
    _bstr_t _bstr(_machineAMQPHandler->GetMachineQueue());
    *pRetVal = _bstr.copy();

    return S_OK;
}

HRESULT STDMETHODCALLTYPE RPCQueueHandler::put_QueueName(BSTR pRetVal)
{
    QueueName = pRetVal;
    return S_OK;
}

void RPCQueueHandler::AddCallback(_bstr_t id, const CallbackFunction& callback)
{
    const std::string basicOutputId(_bstr_t(id, true));
    std::tuple<std::string, CallbackFunction> var = { basicOutputId, callback };
    m_vectors1.push_back(var);
}

HRESULT STDMETHODCALLTYPE RPCQueueHandler::raw_Callback(/*[in]*/ BSTR replyQueue, /*[in]*/ BSTR correlationId, /*[in]*/ unsigned __int64 deliveryTag, /*[in]*/ BSTR data)
{
    IMessagePtr m_message(__uuidof(Message));
    auto hr = m_message->LoadDataFromJson(data);
    if (!SUCCEEDED(hr))
    {
        return hr;
    }

    const std::string m_messageId(_bstr_t(m_message->id, true));

    for (const std::tuple<std::string, CallbackFunction> i : m_vectors1) {
        auto id = std::get<std::string>(i);
        if (id == m_messageId)
        {
            auto callback = std::get<CallbackFunction>(i);
            callback(replyQueue, correlationId, deliveryTag, m_message->data);
        }
    };

    return S_OK;
}

HRESULT STDMETHODCALLTYPE RPCQueueHandler::QueryInterface(REFIID iid, void** ppvObject)
{
    if (iid == __uuidof(ISubscribedMessage))
    {
        m_dwRefCount++;
        *ppvObject = (void*)this;
        return S_OK;
    }

    if (iid == IID_IUnknown)
    {
        m_dwRefCount++;
        *ppvObject = (void*)this;
        return S_OK;
    }

    return E_NOINTERFACE;
}

ULONG STDMETHODCALLTYPE RPCQueueHandler::AddRef()
{
    m_dwRefCount++;
    return m_dwRefCount;
}

ULONG STDMETHODCALLTYPE RPCQueueHandler::Release()
{
    ULONG l;

    l = m_dwRefCount--;

    if (0 == m_dwRefCount)
    {
        delete this;
    }

    return l;
}