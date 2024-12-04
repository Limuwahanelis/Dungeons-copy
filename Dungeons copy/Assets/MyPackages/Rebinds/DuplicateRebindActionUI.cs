using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;
namespace Limuwahanelis.Rebinds
{
    public class DuplicateRebindActionUI : MonoBehaviour
    {
        [SerializeField] List<RebindActionUI> _rebinds = new List<RebindActionUI>();
        [SerializeField] TMP_Text rebindText;
        // Start is called before the first frame update
        void Awake()
        {
            _rebinds = GetComponentsInChildren<RebindActionUI>().ToList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckDuplicate(string label, InputAction duplicate, int duplicateBindingIndex)
        {

            InputAction currentAction;
            int currentBiding;
            Debug.Log(duplicateBindingIndex);
            foreach (RebindActionUI rebindActionUI in _rebinds)
            {
                rebindActionUI.ResolveActionAndBinding(out currentAction, out currentBiding);
                if (currentAction == duplicate)
                    if (currentBiding == duplicateBindingIndex)
                    {
                        Debug.Log("dupicate in " + label + " " + rebindActionUI.GetActionLabel());
                        rebindText.text = $"Inputs for {label} and {rebindActionUI.GetActionLabel()} would be duplicated \n" +
                            $"Please assign another input. ";
                        return;
                    }
            }
            Debug.LogError($"No customable binding for {duplicate.name}");
        }
    }
}