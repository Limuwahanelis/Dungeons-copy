using UnityEngine;
using UnityEngine.InputSystem;

namespace Limuwahanelis.Rebinds
{
    public class RebindSaveLoad : MonoBehaviour
    {
        public InputActionAsset actions;

        public void Save()
        {
            var rebinds = actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("rebinds", rebinds);
        }
        public void Load()
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
                actions.LoadBindingOverridesFromJson(rebinds);
        }
        public void OnEnable()
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
                actions.LoadBindingOverridesFromJson(rebinds);
        }

        public void OnDisable()
        {
            Save();
        }
    }
}
