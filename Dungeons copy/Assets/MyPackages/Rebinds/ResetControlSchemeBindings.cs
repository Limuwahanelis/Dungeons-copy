using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Limuwahanelis.Rebinds
{
    public class ResetControlSchemeBindings : MonoBehaviour
    {
        private enum ControlScheme
        {
            MOUSE_KEYBOARD, GAMEPAD
        }
        [SerializeField] InputActionAsset _actionAsset;
        [SerializeField] string _keyboardScheme;
        [SerializeField] string _mouseScheme;
        [SerializeField] string _gamepadScheme;
        private ControlScheme _selectedScheme;
        private List<string> _targetSchemes = new List<string>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeSchemeToKeyboard()
        {
            _selectedScheme = ControlScheme.MOUSE_KEYBOARD;
        }
        public void ChangeSchemeToGamepad()
        {
            _selectedScheme = ControlScheme.GAMEPAD;
        }
        public void ResetSelectedControlSchemeBindings()
        {
            // TODO: Change how schemes to reset are selected
            _targetSchemes.Clear();
            if (_selectedScheme == ControlScheme.GAMEPAD) _targetSchemes.Add(_gamepadScheme);
            else
            {
                _targetSchemes.Add(_keyboardScheme);
                _targetSchemes.Add(_mouseScheme);
            }
            foreach (InputActionMap map in _actionAsset.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    action.RemoveBindingOverride(InputBinding.MaskByGroups(_targetSchemes.ToArray()));
                }
            }
        }
    }
}
