using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputStack : MonoBehaviour
{
    public InputCommand CurrentCommand { get { return _currentCommand; } 
        set {
            if (_currentCommand != null) _currentCommand.Undo();
            _currentCommand = value; 
            _commandLifeTime = 0;
            _playerController.CurrentPlayerState.SetInputCommand(ref value);
        } }

    [SerializeField] float _commandMaxLife;
    [SerializeField] PlayerController _playerController;
    private InputCommand _currentCommand;
    private float _commandLifeTime;

    // Update is called once per frame
    void Update()
    {
        if (_currentCommand == null) return;
        _commandLifeTime += Time.deltaTime;
        if(_commandLifeTime > _commandMaxLife ) 
        {
            _currentCommand.Undo();
            _currentCommand = null;
        }
    }
}
