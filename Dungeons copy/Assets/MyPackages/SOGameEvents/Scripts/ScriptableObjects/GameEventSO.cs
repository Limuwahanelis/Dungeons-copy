using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new game event",menuName ="Game event")]
public class GameEventSO : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for(int i=_listeners.Count-1; i>=0; i--)
        {
            _listeners[i].RaiseResponse();
        }
    }
    public void RegisterListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }
    public void UnRegisterListener(GameEventListener listener) 
    {
        _listeners.Remove(listener);
    }
}
