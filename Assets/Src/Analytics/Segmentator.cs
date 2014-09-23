using UnityEngine;
using System.Collections.Generic;

public class Segmentator {

	Dictionary<int,WallController> walls;
	Dictionary<int,CellController> cells;


	List<CellController> processed = new List<CellController>();
	List<Room> rooms = new List<Room>();
	Room curRoom = new Room();
	public Segmentator(Dictionary<int,WallController> walls, Dictionary<int,CellController> cells)
	{
		this.walls = walls;
		this.cells = cells;
	}
	public void Start()
	{
		Debug.Log("Starting segmentation");
		rooms.Clear();
		processed.Clear();
		foreach(CellController cell in cells.Values)
		{
			if(processed.Contains(cell))
				continue;
			Next (cell);
			if(curRoom.Cells.Count>0)
			{
				rooms.Add(curRoom);
				Debug.Log(string.Format("Found room with {0} cells",curRoom.Cells.Count));
				curRoom = new Room();
			}

		}

	}

	void Next(CellController curCell)
	{
		List<CellController> reachable = new List<CellController>();
		WallController w = null;
		CellController c = null;

		curRoom.Cells.Add(curCell);
		processed.Add(curCell);

		//left
		c = GetCell(curCell.Position.X-1, curCell.Position.Y);
		w = GetWall(curCell.Position.X, curCell.Position.Y);
		if(c!=null && (w==null || w.wallSprite.Top==false))
			reachable.Add(c);

		//right
		c = GetCell(curCell.Position.X+1, curCell.Position.Y);
		w = GetWall(curCell.Position.X+1, curCell.Position.Y+1);
		if(c!=null && (w==null || w.wallSprite.Bottom==false))
			reachable.Add(c);

		//top
		c = GetCell(curCell.Position.X, curCell.Position.Y+1);
		w = GetWall(curCell.Position.X+1, curCell.Position.Y+1);
		if(c!=null && (w==null || w.wallSprite.Left==false))
			reachable.Add(c);
		
		//bottom
		c = GetCell(curCell.Position.X, curCell.Position.Y-1);
		w = GetWall(curCell.Position.X, curCell.Position.Y);
		if(c!=null && (w==null || w.wallSprite.Right==false))
			reachable.Add(c);

		foreach(CellController nc in reachable)
		{
			if(!processed.Contains(nc))
				Next (nc);
		}
	}

	WallController GetWall(int x, int y)
	{

		return GetWall(new WallPoint(x,y));
	}
	WallController GetWall(WallPoint point)
	{
		
		if(walls.ContainsKey(point.toInt()))
			return walls[point.toInt()];
		
		return null;
	}
	CellController GetCell(int x, int y)
	{
		return GetCell(new MapPoint(x,y));
	}
	CellController GetCell(MapPoint point)
	{
		if(point.X<0 || point.Y<0 || point.X>0xffff || point.Y>0xffff)
			return null;
		
		int key = point.toInt();
		if(cells.ContainsKey(key))
			return cells[key];
		
		return null;
	}

}
