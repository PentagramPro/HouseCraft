using UnityEngine;
using System;
using System.Collections.Generic;

public class HouseController : BaseController {

	public CellController CellPrefab;
	public WallController ThickWallPrefab;

	Dictionary<int,CellController> cells = new Dictionary<int, CellController>();
	Dictionary<int,WallController> walls = new Dictionary<int, WallController>();

	Vector3 markerPos;
	public Vector3 MarkerPosition{
		set{
			markerPos = value;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();

		CellController[] c = GetComponentsInChildren<CellController>();
		
		foreach(CellController cell in c)
		{
			cells.Add(cell.Position.toInt(),cell);
		}

		WallController[] w = GetComponentsInChildren<WallController>();
		
		foreach(WallController wall in w)
		{
			walls.Add(wall.Position.toInt(),wall);
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



	public void ForEachWall(MapPoint point, Action<WallPoint,WallController> action)
	{
		WallController wc = null;
		WallPoint wp;

		for(int i=0;i<4;i++)
		{
			wp = new WallPoint(point.X+i%2,point.Y+(i>1?1:0));

			if(wp.X<0 || wp.Y<0)
				continue;
			walls.TryGetValue(wp.toInt(),out wc);
			action(wp,wc);
			wc = null;
		}



	}

	public bool IsInsideBuilding(WallPoint wallPoint)
	{
		return cells.ContainsKey(wallPoint.toInt()) 
			&& cells.ContainsKey(new WallPoint(wallPoint.X+1,wallPoint.Y).toInt()) 
			&& cells.ContainsKey(new WallPoint(wallPoint.X,wallPoint.Y+1).toInt()) 
			&& cells.ContainsKey(new WallPoint(wallPoint.X+1,wallPoint.Y+1).toInt()) ;
	}

	public CellController GetCell(MapPoint point)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;

		int key = point.toInt();
		if(cells.ContainsKey(key))
			return cells[key];

		return null;
	}

	public void SetCell(MapPoint point)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return;

		int key = point.toInt();
		if(!cells.ContainsKey(key))
		{
			CellController newCell = Instantiate<CellController>(CellPrefab);
			newCell.transform.parent = transform;
			newCell.Position = point;
			cells.Add(key,newCell);
		}
	}

	#region Editor Methods
	public void EditorCheckCells ()
	{
		List<int> toRemove = new List<int> ();
		foreach (int k in cells.Keys) {
				if (cells [k] == null)
						toRemove.Add (k);
		}

		Debug.LogWarning ("To remove: " + toRemove.Count);
		foreach (int k in toRemove)
			cells.Remove (k);
	}

	public void EditorUpdateThickWalls (MapPoint point)
	{

		ForEachWall(point, (WallPoint wp, WallController wc) => {
			if(wc==null)
			{
				wc = Instantiate<WallController>(ThickWallPrefab,transform);
				wc.Position = wp;
				walls.Add(wp.toInt(),wc);

			}

			wc.UpdateWall(true);
			
		});
	}

	#endregion
}
