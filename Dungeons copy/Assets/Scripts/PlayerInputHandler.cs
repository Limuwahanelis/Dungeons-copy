using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    //[SerializeField] PlayerRotation _playerRotation;
    //[SerializeField] TileObjectPlacer _tileObjectPlacer;
    //[SerializeField] ExitApp _exitApp;
    [SerializeField] DungeonBlockSelector _tileSelection;
   // [SerializeField] SeparateSelector _separateSelector;
    private Vector2 _direction;
    private bool _canRotate = false;
    private bool _showExitPanel = false;
    private bool _isHolding = false;
    // Start is called before the first frame update
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (_showExitPanel) return;
    }

    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();

    }
    public void OnLook(InputValue value)
    {
        if (_showExitPanel) return;
        //if (_canRotate) _playerRotation.RotateMouse(value.Get<Vector2>());
    }
    public void OnLockRotation(InputValue value)
    {
        _canRotate = value.Get<float>() >= 1 ? true : false;
    }

    public void OnExit(InputValue value)
    {
        _showExitPanel = !_showExitPanel;
        Cursor.lockState = _showExitPanel ? CursorLockMode.None : CursorLockMode.Confined;
       // _exitApp.SetExitPanel(_showExitPanel);
    }

    public void OnFire(InputValue value)
    {
      //  Logger.Log("Click");
        //if(value.Get<float>()>0) _tileSelection.ClickDungeonTile();
        if (_showExitPanel) return;
    }
    public void OnFireHold(InputValue value)
    {
        //Logger.Log($"HOLD: {value.Get<float>()}");
        if(value.Get<float>()>0)
        {
            //Logger.Log("Hold");
            _isHolding = true;
            _tileSelection.StartTileHold();
           // _separateSelector.StartSelection();
        }
        else
        {
            if (_isHolding)
            {
                //_separateSelector.EndSelection();
                _tileSelection.StopTileHold();
                _isHolding = false;
            }
            else _tileSelection.ClickDungeonTile();
        }
    }

    public void OnCursor(InputValue value)
    {
        if (_showExitPanel) return;
        _tileSelection.SetMousePos(value.Get<Vector2>());
        //_separateSelector.SetMousePos(value.Get<Vector2>());
    }
}
