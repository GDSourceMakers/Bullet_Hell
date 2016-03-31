using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(EnemyMovement))]
public class EnemyMovementEditor : Editor
{

	EnemyMovement EMtarget;

	#region Default
	SerializedProperty speed;
	SerializedProperty sAI;
	SerializedProperty sRot;
	#endregion

	#region Catmull Rom
	SerializedProperty RomType;
	SerializedProperty controlPointsList;
	SerializedProperty sTargets;
	SerializedProperty isLooping;
	#endregion

	#region Line
	SerializedProperty turn;
	#endregion

	#region Sinus
	SerializedProperty width;
	SerializedProperty height;
	#endregion

	SerializedProperty active;

	AIType ai;

	private Transform handleTransform;
	private Quaternion handleRotation;

	Quaternion rotation;
	Quaternion originalRot;
	Vector3 position;
	Vector3 originalPos;

	private const float handleSize = 0.1f;
	private const float pickSize = 0.2f;

	private int selectedIndex = -1;
	private int selectedLineIndex = -1;

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		ai = ((EnemyMovement)target).aiType;

		EditorGUILayout.PropertyField(speed);

		EditorGUILayout.PropertyField(sAI, new GUIContent("AI type", "Size of the tiles"));

		switch (ai)
		{
			case AIType.Linear:

				EditorGUILayout.PropertyField(turn);

				break;
			case AIType.Sinus:

				EditorGUILayout.PropertyField(width);
				EditorGUILayout.PropertyField(height);

				break;
			case AIType.CatmullRom:

				EditorGUILayout.PropertyField(RomType);
				//EditorGUILayout.PropertyField(controlPointsList, true);
				EditorGUILayout.BeginHorizontal();



				EditorGUILayout.IntField("Points", EMtarget.controlPointsList.Count);

				if (GUILayout.Button("+"))
				{
					controlPointsList.InsertArrayElementAtIndex(selectedIndex);
				}
				if (GUILayout.Button("-"))
				{
					controlPointsList.DeleteArrayElementAtIndex(selectedIndex);
				}
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.PropertyField(controlPointsList, true);

				EditorGUILayout.PropertyField(isLooping);

				break;
			case AIType.StopPos:
				break;
			case AIType.None:
				break;
			default:
				break;
		}

		EditorGUILayout.PropertyField(sRot, new GUIContent("Turn Type", "Size of the tiles"));

		EditorGUILayout.PropertyField(active);

		serializedObject.ApplyModifiedProperties();
	}

	void OnEnable()
	{
		EMtarget = target as EnemyMovement;

		speed = serializedObject.FindProperty("speed");
		sAI = serializedObject.FindProperty("aiType");
		sRot = serializedObject.FindProperty("rotType");

		RomType = serializedObject.FindProperty("RomType");
		controlPointsList = serializedObject.FindProperty("controlPointsList");
		sTargets = serializedObject.FindProperty("targets");
		isLooping = serializedObject.FindProperty("isLooping");

		turn = serializedObject.FindProperty("turn");

		width = serializedObject.FindProperty("width");
		height = serializedObject.FindProperty("height");

		active = serializedObject.FindProperty("active");
	}

	void OnSceneGUI()
	{

		#region Handle
		handleTransform = EMtarget.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;
		#endregion


		Gizmos.color = Color.white;

		rotation = EMtarget.transform.rotation;
		position = EMtarget.transform.position;

		if (EditorApplication.isPlaying)
		{
			originalPos = EMtarget.originalPos;
			originalRot = EMtarget.originalRot;
		}
		else
		{
			originalPos = EMtarget.transform.position;
			originalRot = EMtarget.transform.rotation;
		}

		switch (EMtarget.aiType)
		{
			case AIType.Linear:
				#region Linear

				Handles.ArrowCap(0, position, rotation * Quaternion.Euler(90, 0, 0), 2);

				/*
				EMtarget.line0 = ShowPoint(EMtarget.line0, 0);
				EMtarget.line1 = ShowPoint(EMtarget.line1, 1);

				Handles.DrawLine(EMtarget.line0 + originalPos, EMtarget.line1 + originalPos);
				*/

				#endregion
				break;
			case AIType.Sinus:
				#region Sinus
				float p = 0.5f;

				Quaternion angle = originalRot * Quaternion.Euler(0, 0, -90);

				for (float i = 0; i < 50; i += p)
				{
					Vector3 p0;
					Vector3 p1;



					p0 = originalPos + angle * new Vector3(i, Mathf.Sin((i * Mathf.PI) / EMtarget.height) * EMtarget.width);
					p1 = originalPos + angle * new Vector3(i + p, Mathf.Sin(((i + p) * Mathf.PI) / EMtarget.height) * EMtarget.width);

					Handles.DrawLine(p0, p1);
				}

				#endregion
				break;
			case AIType.CatmullRom:
				#region Rom
				for (int i = 0; i < EMtarget.controlPointsList.Count; i++)
				{
					//Handles.SphereCap(0, EMtarget.controlPointsList[i], Quaternion.identity, 0.3f);

					ShowControllPoint(i);

					if ((i == 0 || i == EMtarget.controlPointsList.Count - 2 || i == EMtarget.controlPointsList.Count - 1) && !EMtarget.isLooping)
					{
						continue;
					}

					if (EMtarget.isLooping)
					{
						Handles.DrawLine(EMtarget.transform.position, EMtarget.controlPointsList[0]);
					}
					else
					{
						Handles.DrawLine(EMtarget.transform.position, EMtarget.controlPointsList[1]);
					}

					DisplayCatmullRomSpline(i);
				}
				#endregion
				break;
			case AIType.StopPos:
				break;
			case AIType.None:
				break;
			default:
				break;
		}

	}

	void DisplayCatmullRomSpline(int pos)
	{
		Vector3 p0;
		Vector3 p1;
		Vector3 p2;
		Vector3 p3;

		int size = EMtarget.controlPointsList.Count;

		if (pos == 0)
		{
			Handles.color = Color.green;
		}

		p0 = EMtarget.controlPointsList[CatmullRom.ClampListPos(pos - 1, EMtarget.controlPointsList.Count)];
		//p0 += originalPos;

		p1 = EMtarget.controlPointsList[pos];
		//p1 += originalPos;

		p2 = EMtarget.controlPointsList[CatmullRom.ClampListPos(pos + 1, EMtarget.controlPointsList.Count)];
		//p2 += originalPos;

		p3 = EMtarget.controlPointsList[CatmullRom.ClampListPos(pos + 2, EMtarget.controlPointsList.Count)];
		//p3 += originalPos;

		Vector3 lastPos = Vector3.zero;

		for (float t = 0; t < 1; t += 0.1f)
		{
			Vector3 newPos = CatmullRom.ReturnCatmullRom(t, p0, p1, p2, p3);
			if (t == 0)
			{
				lastPos = newPos;
				continue;
			}

			Handles.DrawLine(lastPos, newPos);
			lastPos = newPos;
		}
		
		Handles.DrawLine(lastPos, p2);
	}

	private Vector3 ShowControllPoint(int index)
	{
		Vector3 point = EMtarget.controlPointsList[index];
		Handles.color = Color.white;
		if (index == 0)
		{
			Handles.color = Color.green;
		}

		if (Handles.Button(point, handleRotation, handleSize, pickSize, Handles.DotCap))
		{
			selectedIndex = index;
		}
		if (selectedIndex == index)
		{
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(EMtarget, "Move Point");
				EditorUtility.SetDirty(EMtarget);
				EMtarget.controlPointsList[index] = point;
			}
		}
		Handles.color = Color.white;
		return point;
	}

	private Vector3 ShowPoint(Vector3 a, int index)
	{
		Vector3 point = a;
		Handles.color = Color.white;

		if (Handles.Button(point, handleRotation, handleSize, pickSize, Handles.DotCap))
		{
			selectedLineIndex = index;
		}
		if (selectedLineIndex == index)
		{
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(EMtarget, "Move Point");
				EditorUtility.SetDirty(EMtarget);
			}
		}
		return point;
	}
}
