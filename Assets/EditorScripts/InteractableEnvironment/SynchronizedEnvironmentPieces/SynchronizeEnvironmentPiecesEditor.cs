// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor (typeof(SynchronizeEnvironmentPieces))]
// public class SynchronizeEnvironmentPiecesEditor : Editor {
// 	private SerializedProperty actionOffsetValuesProperty;
// 	private SerializedProperty environmentPiecesProperty;

// 	private const string envPiecePropOffsetValuesName = "environmentPiecesActionOffsetValues";
// 	private const string envPiecePropPiecesName = "environmentPieces";

// 	private bool[] showEnvironmentPieceSlots= new bool[2];

// 	private void OnEnable() {
// 		actionOffsetValuesProperty = 
// 			serializedObject.FindProperty(envPiecePropOffsetValuesName);
// 		environmentPiecesProperty = 
// 			serializedObject.FindProperty(envPiecePropPiecesName);
// 		Debug.Log(environmentPiecesProperty.arraySize);
// 	}

// 	public override void OnInspectorGUI(){
// 		serializedObject.Update();

// 		for (int i = 0; i < 2; i++){
// 			EnvironmentPieceGUI(i);
// 		}

// 		serializedObject.ApplyModifiedProperties();
// 	}

// 	private void EnvironmentPieceGUI(int index) {
// 		EditorGUILayout.BeginVertical(GUI.skin.box);
// 		EditorGUI.indentLevel++;

// 		showEnvironmentPieceSlots[index] = EditorGUILayout.Foldout(showEnvironmentPieceSlots[index], "Environment Piece Slot" + index);
// 		if (showEnvironmentPieceSlots[index]){
// 			EditorGUILayout.PropertyField (actionOffsetValuesProperty.GetArrayElementAtIndex(index));
// 			EditorGUILayout.PropertyField(environmentPiecesProperty.GetArrayElementAtIndex(index));
			
// 		}


// 		EditorGUI.indentLevel--;		
// 		EditorGUILayout.EndVertical();
// 	}

// }
