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

	public void RestoreCache()
	{
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
	protected override void Awake ()
	{
		base.Awake ();

		RestoreCache();

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


	public WallController SetWall(WallPoint point)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;

		WallController res = null;
		int key = point.toInt();
		if(!walls.ContainsKey(key))
		{
			WallController newWall = Instantiate<WallController>(WallPrefab);
			newWall.transform.parent = transform;
			newWall.Position = point;
			walls.Add(key,newWall);
			res = newWall;
		}
		else
			res = walls[key];
		return res;
	}

	public WallController GetWall(WallPoint point)
	{

		if(walls.ContainsKey(point.toInt()))
			return walls[point.toInt()];

		return null;
	}
	void OnTap()
	{
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition)+transform.position;

		if(state == Modes.SetWalls)
		{

			WallPoint wp = new WallPoint(Mathf.RoundToInt(pz.x),Mathf.RoundToInt(pz.y));
			//new Rect(wp.X-0.5f,wp.Y-0.5f,wp.X+0.5f,wp.Y+0.5f).Contains(pz)

			WallController wall = null, adjWall=null;

			if(new Rect(wp.X-0.25f,wp.Y-0.25f,0.5f,0.5f).Contains(pz))
			{
				//middle
				wall = SetWall(wp);
			}
			else if(new Rect(wp.X-0.5f,wp.Y-0.25f,0.25f,0.5f).Contains(pz))
			{
				//left
				wall = SetWall(wp);
				wall.wallSprite.Left = true;
				WallPoint adjWallPoint = new WallPoint(wp.X-1,wp.Y);
				adjWall = GetWall(adjWallPoint);
				if(adjWall==null)
					SetWall(adjWallPoint);


			}
			else if(new Rect(wp.X-0.25f,wp.Y+0.25f,0.5f,0.25f).Contains(pz))
			{
				//top
				wall = SetWall(wp);
				wall.wallSprite.Top = true;

				WallPoint adjWallPoint = new WallPoint(wp.X,wp.Y+1);
				adjWall = GetWall(adjWallPoint);
				if(adjWall==null)
					SetWall(adjWallPoint);
			}
			else if(new Rect(wp.X+0.25f,wp.Y-0.25f,0.25f,0.5f).Contains(pz))
			{
				//right
				wall = SetWall(wp);
				wall.wallSprite.Right = true;

				WallPoint adjWallPoint = new WallPoint(wp.X+1,wp.Y);
				adjWall = GetWall(adjWallPoint);
				if(adjWall==null)
					SetWall(adjWallPoint);
			}
			else if(new Rect(wp.X-0.25f,wp.Y-0.5f,0.5f,0.25f).Contains(pz))
			{
				//bottom
				wall = SetWall(wp);
				wall.wallSprite.Bottom = true;

				WallPoint adjWallPoint = new WallPoint(wp.X,wp.Y-1);
				adjWall = GetWall(adjWallPoint);
				if(adjWall==null)
					SetWall(adjWallPoint);
			}

			if(wall!=null)
			{
				wall.UpdateWall();
			}

			if(adjWall!=null)
			{
				adjWall.UpdateWall();
			}

		}
	}

	#region Editor Methods
	public void EditorCheckCells ()
	{
		cells.Clear();
		walls.Clear();
		RestoreCache();
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
