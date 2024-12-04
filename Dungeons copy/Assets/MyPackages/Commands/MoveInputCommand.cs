using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInputCommand : InputCommand
{
    private Vector2 _direction;
    public MoveInputCommand(PlayerState playerState,Vector2 direction) : base(playerState)
    {
        _direction = direction;
    }

    public override void Execute()
    {
        _playerState.Move(_direction);
    }

    public override void Undo()
    {
        _playerState.UndoComand();
    }
}
