using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WallGfxController))]
public class WallGfxEditor : Editor {
	public override void OnInspectorGUI()
	{
		
		DrawDefaultInspector();

		WallGfxController c = target as WallGfxController;

		if(GUILayout.Button("Update"))
			c.UpdateWall();
	}
}