using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInputCommand : InputCommand
{
    private PlayerCombat.AttackModifiers _attackModifier;
    public AttackInputCommand(PlayerState playerState, PlayerCombat.AttackModifiers attackModifier=PlayerCombat.AttackModifiers.NONE) : base(playerState)
    {
        _attackModifier = attackModifier;
    }

    public override void Execute()
    {
        _playerState.Attack(_attackModifier);
    }

    public override void Undo()
    {
        _playerState.UndoComand();
    }
}
