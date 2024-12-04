using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEventSO _gameEvent;
    [SerializeField] UnityEvent _response;
    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        _gameEvent.UnRegisterListener(this);
    }
    public void RaiseResponse()
    {
        _response.Invoke();
    }
}
