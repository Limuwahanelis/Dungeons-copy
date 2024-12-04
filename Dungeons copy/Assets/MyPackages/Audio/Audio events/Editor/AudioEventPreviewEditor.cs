using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioEvent),true)]
public class AudioEventPreviewEditor : Editor
{

	public AudioSource _previewer;

	public void OnEnable()
	{
		_previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
	}

	public void OnDisable()
	{
		DestroyImmediate(_previewer.gameObject);
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
		if (GUILayout.Button("Preview"))
		{
			((AudioEvent) target).Preview(_previewer,50f,50f);
		}
		EditorGUI.EndDisabledGroup();
	}
}
