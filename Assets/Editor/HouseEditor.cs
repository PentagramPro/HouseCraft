using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HouseController))]
public class HouseEditor : Editor {

	bool edit = false;
	EditorMouseInput input;

	public override void OnInspectorGUI()
	{
		
		DrawDefaultInspector();

		if(GUILayout.Button(edit? "Disable editor": "Enable editor"))
		{
			edit = !edit;
		}
	}


	/// <summary>
	/// Lets the Editor handle an event in the scene view.
	/// </summary>
	private void OnSceneGUI()
	{
		if(!edit)
			return;
		HouseController hc = (HouseController) target;
		if(input==null)
			input = new EditorMouseInput(hc.transform);
		
		// if UpdateHitPosition return true we should update the scene views so that the marker will update in real time
		if (input.UpdateHitPosition())
		{
			SceneView.RepaintAll();
		}
		
		// Calculate the location of the marker based on the location of the mouse
		RecalculateMarkerPosition();
		
		// get a reference to the current event
		Event current = Event.current;
		
		// if the mouse is positioned over the layer allow drawing actions to occur
		if (this.IsMouseOnLayer())
		{
			// if mouse down or mouse drag event occurred
			if (current.type == EventType.MouseDown || 
			    current.type == EventType.MouseDrag || 
			    current.type==EventType.MouseUp)
			{
				/*if (current.button == 1 &&
				    (current.type==EventType.MouseUp || selType==SelType.Block))
				{
					MapPoint mp = input.GetTilePositionFromMouseLocation();
					switch(selType)
					{
					case SelType.Block:
						PlaceBlock(tc,mp);
						break;
					case SelType.Building:
						PlaceBuilding(tc,mp);
						break;
					case SelType.Vehicle:
						PlaceVehicle(tc,mp);
						break;
					}
					current.Use();
				}*/

			}
			
		}
		
		
		/*
		Handles.BeginGUI();
		GUI.Label(new Rect(10, Screen.height - 90, 100, 100), "LMB: Draw");
		GUI.Label(new Rect(10, Screen.height - 105, 100, 100), "RMB: Erase");
		Handles.EndGUI();
		*/
	}

	private bool IsMouseOnLayer()
	{
		return true;
	}

	/// <summary>
	/// Recalculates the position of the marker based on the location of the mouse pointer.
	/// </summary>
	private void RecalculateMarkerPosition()
	{
		// get reference to the tile map component
		HouseController hc = (HouseController) target;
		
		// store the tile location (Column/Row) based on the current location of the mouse pointer
		var tilepos = input.GetTilePositionFromMouseLocation();
		
		
		
		// set the TileMap.MarkerPosition value
		hc.MarkerPosition = hc.transform.position + new Vector3(tilepos.X+0.5f, tilepos.Y+0.5f,0.5f);
	}

}
