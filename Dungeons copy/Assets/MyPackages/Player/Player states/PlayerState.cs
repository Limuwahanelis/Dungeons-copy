using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerState
{
    public delegate PlayerState GetState(Type state);
    protected PlayerContext _context;
    protected GetState _getType;
    protected InputCommand _inputCommand;
    protected Type _stateTypeToChangeFromInputCommand;
    public PlayerState(GetState function)
    {
        _getType = function;
    }
    public virtual void SetUpState(PlayerContext context)
    {
        _context = context;
    }
   
    public abstract void Update();
    public virtual void FixedUpdate() { }
    public virtual void Move(Vector2 direction) { }
    public virtual void Jump() { }
    public virtual void Attack(PlayerCombat.AttackModifiers attackModifier=PlayerCombat.AttackModifiers.NONE) { }
    public virtual void Push() { /*ChangeState(PlayerPushedState.StateType);*/}
    public abstract void InterruptState();
    public void ChangeState(Type newStateType)
    {
        _stateTypeToChangeFromInputCommand = null;
        PlayerState state = _getType(newStateType);
        _context.ChangePlayerState(state);
        state.SetUpState(_context);
    }
    public virtual void UndoComand() { }
    public void SetInputCommand(ref InputCommand command)
    {
        _inputCommand = command;
    }
    protected bool PerformInputCommand()
    {
        if (_inputCommand == null) return false;
        _inputCommand.Execute();
        _inputCommand = null;
        return true;
    }
}
