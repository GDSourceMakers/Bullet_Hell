using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
[CustomEditor(typeof(MovemetController))]
class MovementControllerEditor : Editor
{

	MovemetController targetCont;

	SerializedProperty speed;
	SerializedProperty moves;

	SerializedProperty test;

	ReorderableList list;

	void OnEnable()
	{
		targetCont = target as MovemetController;

		speed = serializedObject.FindProperty("speed");

		moves = serializedObject.FindProperty("moves");

		test = serializedObject.FindProperty("test");


		list = new ReorderableList(serializedObject, moves, true, true, true, true);

		//list.drawElementBackgroundCallback = DrawBack;
		list.elementHeight = EditorGUIUtility.singleLineHeight;

		list.drawHeaderCallback = rect =>
		{
			EditorGUI.LabelField(rect, "Layers");
		};

		list.drawElementCallback = DrawListElement;

		list.onAddDropdownCallback = DrawDropDown;
	}


	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(speed);

		//EditorGUILayout.PropertyField(test);

		list.DoLayoutList();

		EditorGUILayout.PropertyField(moves.GetArrayElementAtIndex(list.index),true);

		serializedObject.ApplyModifiedProperties();
	}


	void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
	{
		rect.x += 3;
		rect.width -= 2;
		SerializedProperty property = list.serializedProperty.GetArrayElementAtIndex(index);
		EditorGUI.LabelField(rect, "");
	} 

	/*
	public void DrawBack(Rect rect, int index, bool active, bool focused)
	{
		if (active)
		{
			rect.height = EditorGUIUtility.singleLineHeight;
			rect.x += 3;
			rect.width -= 2;
			Texture2D tex = new Texture2D(1, 1);
			tex.SetPixel(0, 0, new Color(0.33f, 0.66f, 1f, 0.66f));
			tex.Apply();

			GUI.DrawTexture(rect, tex as Texture);
		}
	}
	

	public void DrawDropDown(Rect buttonRect, ReorderableList l)
	{
		GenericMenu menu = new GenericMenu();
		menu.AddItem(new GUIContent("Linear"), false, ClickHandler, "Linear");
		menu.AddItem(new GUIContent("Sinus"), false, ClickHandler, "Sinus");
		menu.AddItem(new GUIContent("CatmullRom"), false, ClickHandler, "CatmullRom");

		menu.ShowAsContext();
	}

	private void ClickHandler(object target)
	{
		string data = (string)target;

		MovementType m = new MovementType(targetCont);

		switch (data)
		{
			case "Linear":
				m = new LinearMovement(targetCont);
				break;
			case "Sinus":
				m = new LinearMovement(targetCont);
				break;
			case "CatmullRom":
				m = new CatmullRomMovement(targetCont);
				break;
			default:
				break;
		}

		Debug.Log(targetCont);

		targetCont.moves.Add(m);

		
	}

}

*/