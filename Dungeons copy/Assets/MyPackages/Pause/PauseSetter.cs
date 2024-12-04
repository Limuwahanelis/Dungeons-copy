using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

public class PauseSetter : MonoBehaviour
{
    [SerializeField] InputActionReference _playerPause;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    private static bool _isForcedPause = false;
    public void SetPauseNoTimeStop(bool value)
    {
        if (_isForcedPause) return;
        PauseSettings.SetPause(value,false);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }
    public void SwitchPause()
    {
        if (_isForcedPause) return;
        PauseSettings.SetPause(!PauseSettings.IsGamePaused, !PauseSettings.IsGamePaused);
        if (PauseSettings.IsGamePaused) OnPause?.Invoke();
        else OnResume?.Invoke();
    }
    public void SetPause(bool value)
    {
        if (_isForcedPause) return;
        PauseSettings.SetPause(value,value);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }

    public void SetForcedPause(bool value)
    {
        if (value) _playerPause.action.Disable();
        else _playerPause.action.Enable();
        _isForcedPause = value;
        PauseSettings.SetPause(value,value);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }
}