using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent (typeof (HouseController))]
public class HouseController : BaseController {

	enum Modes{
		Idle, SetWalls
	}
	public CellController CellPrefab;
	public WallController ThickWallPrefab;
	public WallController WallPrefab;

	Dictionary<int,CellController> cells = new Dictionary<int, CellController>();
	Dictionary<int,WallController> walls = new Dictionary<int, WallController>();


	Modes state = Modes.SetWalls;

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

		GetComponent<TapController>().OnTap+=OnTap;
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


	public void SetWall(WallPoint point)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return;

		int key = point.toInt();
		if(!walls.ContainsKey(key))
		{
			WallController newWall = Instantiate<WallController>(WallPrefab);
			newWall.transform.parent = transform;
			newWall.Position = point;
			walls.Add(key,newWall);
		}
	}

	void OnTap()
	{
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition)+transform.position;

		if(state == Modes.SetWalls)
		{

			WallPoint wp = new WallPoint(Mathf.RoundToInt(pz.x),Mathf.RoundToInt(pz.y));
			Rect r = new Rect(wp.X-0.5f,wp.Y-0.5f,wp.X+0.5f,wp.Y+0.5f);
			if(r.Contains(pz))
			{
				SetWall(wp);
			}

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
		if(toRemove.Count==0)
			return;

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

			if(wc.EditorUpdateWall())
			{
				GameObject.DestroyImmediate(wc.gameObject);
				walls.Remove(wp.toInt());
			}
			
		});


	}

	#endregion
}
