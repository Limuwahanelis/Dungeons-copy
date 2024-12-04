
using UnityEngine;
using UnityEditor;

using System.ComponentModel.Design;
public class CreateUIMenuItems : MonoBehaviour
{
    [SerializeField]static GameObject _mainButton;
    [MenuItem("GameObject/UI/My Button", false, 10)]
    static void CreateButtonFromPrefab(UnityEditor.MenuCommand menuCommand)
    {
        CreateObject("Assets/MyPackages/Custom UI elements/Prefabs/Button.prefab", "Button", menuCommand);
    }

    [MenuItem("GameObject/UI/My Slider", false, 10)]
    static void CreateSliderFromPrefab(UnityEditor.MenuCommand menuCommand)
    {
        CreateObject("Assets/MyPackages/Custom UI elements/Prefabs/Slider.prefab", "Slider", menuCommand);
    }

    [MenuItem("GameObject/UI/Tab", false, 10)]
    static void CreateTabFromPreab(UnityEditor.MenuCommand menuCommand)
    {
        CreateObject("Assets/MyPackages/Custom UI elements/Prefabs/Tab.prefab", "Tab", menuCommand);
    }

    private static void CreateObject(string prefabPath,string name, UnityEditor.MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject buttonPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
        GameObject go = PrefabUtility.InstantiatePrefab(buttonPrefab) as GameObject;
        go.name = name;
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
