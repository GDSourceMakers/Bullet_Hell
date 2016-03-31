using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
[CustomPropertyDrawer(typeof(CatmullRomMovement))]
class CatmullRomMovementEditor : PropertyDrawer
{

	CatmullRomMovement targetCont;

	SerializedProperty RomPoints;
	SerializedProperty isLooping;

	private int selectedIndex = -1;

	void OnEnable()
	{
		//targetCont = target as MovemetController;
	}

	void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		RomPoints = property.FindPropertyRelative("RomPoints");
		isLooping = property.FindPropertyRelative("isLooping");


		EditorGUILayout.BeginHorizontal();



		EditorGUILayout.IntField("Points", RomPoints.arraySize);

		if (GUILayout.Button("+"))
		{
			RomPoints.InsertArrayElementAtIndex(selectedIndex);
		}
		if (GUILayout.Button("-"))
		{
			RomPoints.DeleteArrayElementAtIndex(selectedIndex);
		}
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.PropertyField(RomPoints,true);
		EditorGUILayout.PropertyField(isLooping);

	}
}
*/