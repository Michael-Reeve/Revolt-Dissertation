using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class stateBox
{
	public State state;
	public Rect box = new Rect(0, 0, 200, 100);
}

public class FSMwindow : EditorWindow
{
	string stateMachineName = "State Machine";
	public stateBox[] states;

	[MenuItem ("FSM/FSMwindow")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(FSMwindow));
	}
	void OnGUI()
	{
		stateMachineName = EditorGUILayout.TextField("State Machine Name:", stateMachineName);
		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		SerializedProperty statesProperty = so.FindProperty("states");
		EditorGUILayout.PropertyField(statesProperty, true);
		so.ApplyModifiedProperties();

		GUILayout.BeginHorizontal(EditorStyles.helpBox);
		GUILayout.BeginScrollView(new Vector2(0, 0), EditorStyles.helpBox);
		for(int i = 0; i < states.Length; i++)
		{
			GUI.Box(states[i].box, "text");
		}
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
	}
}
