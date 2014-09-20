using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HouseController))]
public class HouseEditor : Editor {

	enum Modes {
		Idle, PlaceCell, PlaceWindow, PlaceThickWall
	}
	Modes state = Modes.Idle;
	EditorMouseInput input;

	public override void OnInspectorGUI()
	{
		
		DrawDefaultInspector();

		if(GUILayout.Button(state!=Modes.Idle? "Disable editor": "Enable editor"))
		{
			if(state!=Modes.Idle)
				state = Modes.Idle;
			else
			{
				state = Modes.PlaceCell;
				HouseController hc = (HouseController) target;
				hc.EditorCheckCells();
			}	
		}

		if(state!=Modes.Idle)
		{
			GUILayout.Label("Current mode: "+System.Enum.GetName(typeof(Modes),state));

			if(GUILayout.Button("Place cells"))
			{
				state = Modes.PlaceCell;
			}
			if(GUILayout.Button("Place thick walls"))
			{
				state = Modes.PlaceThickWall;
			}
			if(GUILayout.Button("Place windows"))
			{
				state = Modes.PlaceWindow;
			}
		}
	}


	/// <summary>
	/// Lets the Editor handle an event in the scene view.
	/// </summary>
	private void OnSceneGUI()
	{
		if(state==Modes.Idle)
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
				if (current.button == 1 &&
				    (current.type==EventType.MouseUp))
				{

					switch(state)
					{
					case Modes.PlaceCell:
						PlaceCell(hc,input.GetCellPositionFromMouseLocation());
						break;
					case Modes.PlaceWindow:
						PlaceWall(hc,input.GetWallPositionFromMouseLocation(), hc.WindowPrefab);
						break;
					case Modes.PlaceThickWall:
						PlaceWall(hc,input.GetWallPositionFromMouseLocation(), hc.ThickWallPrefab);
						break;
					}
					current.Use();
				}

			}
			
		}
		
		
		/*
		Handles.BeginGUI();
		GUI.Label(new Rect(10, Screen.height - 90, 100, 100), "LMB: Draw");
		GUI.Label(new Rect(10, Screen.height - 105, 100, 100), "RMB: Erase");
		Handles.EndGUI();
		*/
	}


	private void PlaceCell(HouseController hc, MapPoint p)
	{
		hc.SetCell(p, hc.CellPrefab);
		hc.EditorUpdateThickWalls(p);
	}

	private void PlaceWall(HouseController hc, WallPoint p, WallController prefab)
	{

		if(hc.GetWall(p)==null)
			return;

		hc.EditorRemoveWall(p);
		WallController w = hc.SetWall(p,prefab);
		w.EditorUpdateWall();

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

		
		
		
		// set the TileMap.MarkerPosition value
		Vector3 offset;
		if(state==Modes.PlaceCell)
		{
			var tilepos = input.GetCellPositionFromMouseLocation();
			offset = new Vector3(tilepos.X+0.5f, tilepos.Y+0.5f,0.5f);
		}
		else
		{
			var tilepos = input.GetWallPositionFromMouseLocation();
			offset = new Vector3(tilepos.X, tilepos.Y,0.5f);
		}
		hc.MarkerPosition = hc.transform.position + offset;
	}

}
