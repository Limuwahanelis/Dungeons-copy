using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

[CreateAssetMenu(menuName ="Animation duration list")]
public class AnimationDurationList : ScriptableObject
{
    [SerializeField]
    public List<AnimationData> animations;
#if UNITY_EDITOR
    [HideInInspector]
    public AnimatorController animatorController;
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;
    public void RefreshList()
    {
        serializedObject = new SerializedObject(this);
        serializedProperty = serializedObject.FindProperty("animations");
        animations.Clear();
        serializedObject.Update();
        for(int i=0;i<animatorController.layers.Length;i++)
        {
            GetAllStatesFromSubStateMachine(animatorController.layers[i].stateMachine,i);
        }
        
        serializedObject.ApplyModifiedProperties();
    }

    private void GetAllStatesFromSubStateMachine(AnimatorStateMachine stateMachine,int layer)
    {
        for (int i = 0; i < stateMachine.states.Length; i++)
        {
            AnimatorState state = stateMachine.states[i].state;
            if (state.motion == null || state.motion.GetType() == typeof(BlendTree))
            {
                serializedProperty.InsertArrayElementAtIndex(serializedProperty.arraySize);
                AnimationData tmp2 = new AnimationData(state.name, 0,0,layer);
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize-1).FindPropertyRelative("name").stringValue = tmp2.name;
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize-1).FindPropertyRelative("duration").floatValue = tmp2.duration;
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1).FindPropertyRelative("speed").floatValue = tmp2.speed;
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1).FindPropertyRelative("layer").intValue = tmp2.layer;
                continue;
            }
                serializedProperty.InsertArrayElementAtIndex(serializedProperty.arraySize );
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize-1 ).FindPropertyRelative("name").stringValue = state.name;
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize-1 ).FindPropertyRelative("duration").floatValue = state.motion.averageDuration;
                serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1).FindPropertyRelative("speed").floatValue = state.speed;
            serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1).FindPropertyRelative("layer").intValue = layer;
        }
        foreach(ChildAnimatorStateMachine subStateMachine in stateMachine.stateMachines)
        {
            GetAllStatesFromSubStateMachine(subStateMachine.stateMachine,layer);
        }
    }
#endif
}
