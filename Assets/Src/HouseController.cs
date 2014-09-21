using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent (typeof (HouseController))]
public class HouseController : BaseController {

	enum Modes{
		Idle, SetWalls, SetObject
	}
	public CellController CellPrefab;
	public WallController ThickWallPrefab;
	public WallController WallPrefab;
	public WallController WindowPrefab;

	Dictionary<int,CellController> cells = new Dictionary<int, CellController>();
	Dictionary<int,WallController> walls = new Dictionary<int, WallController>();

	CellController selectedPrefab = null;
	CellController.CellRotation selectedRotation = CellController.CellRotation.None;

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
		// BAD THING
		if(Input.GetKeyUp(KeyCode.Space))
		{
			selectedRotation = selectedRotation.Next();
		}
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

	public CellController ReplaceCell(MapPoint point, CellController prefab)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;


		CellController newCell = CellController.InstantiateMe(prefab,transform,point);

		int minX=0,maxX=0,minY=0,maxY=0;
		prefab.GetCellIndexes(point,selectedRotation,ref minX,ref minY,ref maxX, ref maxY);

		for(int x=minX;x<=maxX;x++)
		{
			for(int y=minY;y<=maxY;y++)
			{
				MapPoint p = new MapPoint(x,y);
				int key = p.toInt();
				if(cells.ContainsKey(key))
				{
					GameObject.Destroy(cells[key].gameObject);
					cells.Remove(key);
					cells[key] = newCell;
				}
			}
		}
		newCell.SetRotation(selectedRotation);

		return newCell;
	}



	public WallController ReplaceWall(WallPoint point, WallController prefab)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;

		WallController res = null;
		int key = point.toInt();

		if(walls.ContainsKey(key))
		{
			GameObject.Destroy(walls[key].gameObject);
			walls.Remove(key);
		}

		res = SetWall(point, prefab);

		return res;
	}

	public WallController SetWall(WallPoint point, WallController prefab)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;

		WallController res = null;
		int key = point.toInt();
		if(!walls.ContainsKey(key))
		{
			WallController newWall = Instantiate<WallController>(prefab);
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
			if(IsInsideBuilding(wp))
			{
				WallController wall = null, adjWall=null;

				if(new Rect(wp.X-0.25f,wp.Y-0.25f,0.5f,0.5f).Contains(pz))
				{
					//middle
					wall = SetWall(wp, WallPrefab);
				}
				else if(new Rect(wp.X-0.5f,wp.Y-0.25f,0.25f,0.5f).Contains(pz))
				{
					//left
					wall = SetWall(wp, WallPrefab);
					wall.wallSprite.Left = true;
					WallPoint adjWallPoint = new WallPoint(wp.X-1,wp.Y);
					adjWall = GetWall(adjWallPoint);
					if(adjWall==null)
						SetWall(adjWallPoint, WallPrefab);


				}
				else if(new Rect(wp.X-0.25f,wp.Y+0.25f,0.5f,0.25f).Contains(pz))
				{
					//top
					wall = SetWall(wp, WallPrefab);
					wall.wallSprite.Top = true;

					WallPoint adjWallPoint = new WallPoint(wp.X,wp.Y+1);
					adjWall = GetWall(adjWallPoint);
					if(adjWall==null)
						SetWall(adjWallPoint, WallPrefab);
				}
				else if(new Rect(wp.X+0.25f,wp.Y-0.25f,0.25f,0.5f).Contains(pz))
				{
					//right
					wall = SetWall(wp, WallPrefab);
					wall.wallSprite.Right = true;

					WallPoint adjWallPoint = new WallPoint(wp.X+1,wp.Y);
					adjWall = GetWall(adjWallPoint);
					if(adjWall==null)
						SetWall(adjWallPoint, WallPrefab);
				}
				else if(new Rect(wp.X-0.25f,wp.Y-0.5f,0.5f,0.25f).Contains(pz))
				{
					//bottom
					wall = SetWall(wp, WallPrefab);
					wall.wallSprite.Bottom = true;

					WallPoint adjWallPoint = new WallPoint(wp.X,wp.Y-1);
					adjWall = GetWall(adjWallPoint);
					if(adjWall==null)
						SetWall(adjWallPoint, WallPrefab);
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
		else if(state==Modes.SetObject)
		{
			MapPoint mp = new MapPoint(Mathf.FloorToInt(pz.x),Mathf.FloorToInt(pz.y));
			if(CellPrefab.PrefabValidatePosition(M,mp,selectedRotation))
			{
				ReplaceCell(mp,selectedPrefab);
			}



		}
	}

	public void SetHouseMode(HouseModes mode, CellController prefab)
	{
		switch(mode)
		{
		case HouseModes.Idle:
			state = Modes.Idle;
			selectedPrefab = null;
			break;
		case HouseModes.SetWalls:
			state = Modes.SetWalls;
			selectedPrefab = null;
			break;
		case HouseModes.SetObject:
			state = Modes.SetObject;
			selectedPrefab = prefab;
			break;
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

	public void EditorRemoveWall(WallPoint point)
	{
		WallController w = null;
		if(walls.TryGetValue(point.toInt(),out w))
		{
			GameObject.DestroyImmediate(w.gameObject);
			walls.Remove(point.toInt());
		}
	}

	public CellController EditorSetCell(MapPoint point, CellController prefab)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return  null;
		
		CellController newCell = null;
		int key = point.toInt();
		if(!cells.ContainsKey(key))
		{
			newCell = CellController.InstantiateMe(prefab,transform,point);
			cells.Add(key,newCell);
		}
		else
			newCell = cells[key];
		
		return newCell;
	}

	#endregion
}
