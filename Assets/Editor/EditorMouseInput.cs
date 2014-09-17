using UnityEngine;
using UnityEditor;


public class EditorMouseInput
{
	Transform target;
	public EditorMouseInput (Transform target)
	{
		this.target = target;
	}

	private Vector3 mouseHitPos;

	
	/// <summary>
	/// Calculates the position of the mouse over the tile map in local space coordinates.
	/// </summary>
	/// <returns>Returns true if the mouse is over the tile map.</returns>
	public bool UpdateHitPosition()
	{
		// get reference to the tile map component


		// build a plane object that 
		var p = new Plane(target.TransformDirection(new Vector3(0,0,1)), target.position);
		
		// build a ray type from the current mouse position
		var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
		
		// stores the hit location
		var hit = new Vector3();
		
		// stores the distance to the hit location
		float dist;
		
		// cast a ray to determine what location it intersects with the plane
		if (p.Raycast(ray, out dist))
		{
			// the ray hits the plane so we calculate the hit location in world space
			hit = ray.origin + (ray.direction.normalized * dist);
		}
		
		// convert the hit location from world space to local space
		var value = target.InverseTransformPoint(hit);
		
		// if the value is different then the current mouse hit location set the 
		// new mouse hit location and return true indicating a successful hit test
		if (value != this.mouseHitPos)
		{
			this.mouseHitPos = value;
			return true;
		}
		
		// return false if the hit test failed
		return false;
	}


	
	/// <summary>
	/// Calculates the location in tile coordinates (Column/Row) of the mouse position
	/// </summary>
	/// <returns>Returns a <see cref="Vector2"/> type representing the Column and Row where the mouse of positioned over.</returns>
	public MapPoint GetTilePositionFromMouseLocation()
	{


		
		// round the numbers to the nearest whole number using 5 decimal place precision
		MapPoint pos = new MapPoint(
			(int)System.Math.Round(mouseHitPos.x, 5, System.MidpointRounding.ToEven), 
			(int)System.Math.Round(mouseHitPos.y, 5, System.MidpointRounding.ToEven));
		
		
		// do a check to ensure that the row and column are with the bounds of the tile map
		//pos.Clamp(hc.MapX,hc.MapZ);
		
		// return the column and row values
		return pos;
	}
}


