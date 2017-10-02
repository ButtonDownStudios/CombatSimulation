using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateUnitCharObject {
	[MenuItem("Assets/Create/UnitCharasteristics")]
	public static void CreateUnitCharAsset(){
		UnitCharacteristics newUnitChar = ScriptableObject.CreateInstance<UnitCharacteristics> ();
		AssetDatabase.CreateAsset (newUnitChar, "Assets/NewScriptableObject.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = newUnitChar;
	}
}
