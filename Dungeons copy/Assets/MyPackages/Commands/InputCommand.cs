using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputCommand
{
    protected PlayerState _playerState;
    public InputCommand(PlayerState playerState)
    {
        _playerState = playerState;
    }
    public abstract void Execute();

    public abstract void Undo();

}
