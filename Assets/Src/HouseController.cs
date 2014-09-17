using UnityEngine;
using System.Collections.Generic;

public class HouseController : BaseController {


	Dictionary<int,CellController> cells = new Dictionary<int, CellController>();

	Vector3 markerPos;
	public Vector3 MarkerPosition{
		set{
			markerPos = value;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmosSelected()
	{

		
		Gizmos.color = Color.red;    
		Gizmos.DrawWireCube(markerPos, new Vector3(1,1, 1) * 1.1f);
	}

	private int MapPointToInt(MapPoint point)
	{
		return point.X << 16 | point.Y;
	}

	public CellController GetCell(MapPoint point)
	{
		int key = MapPointToInt(point);
		if(cells.ContainsKey(key))
			return cells[key];

		return null;
	}

}
