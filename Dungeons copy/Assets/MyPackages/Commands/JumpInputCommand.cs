using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInputCommand : InputCommand
{
    public JumpInputCommand(PlayerState playerState) : base(playerState)
    {
    }

    public override void Execute()
    {
        _playerState.Jump();
    }

    public override void Undo()
    {
        _playerState.UndoComand();
    }
}
