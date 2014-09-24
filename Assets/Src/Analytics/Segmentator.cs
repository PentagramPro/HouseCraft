using UnityEngine;
using System;
using System.Collections.Generic;

public class Segmentator : BaseController {



	List<CellController> processed = new List<CellController>();
	List<Room> rooms = new List<Room>();
	Dictionary<int, Door> doors = new Dictionary<int, Door>();
	Room curRoom = new Room();

	public void Launch(Dictionary<int,WallController> walls, Dictionary<int,CellController> cells)
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
				Debug.Log(string.Format("Found room #{1} with {0} cells",curRoom.Cells.Count,curRoom.Number));
				curRoom = new Room();
				curRoom.Number = rooms.Count;
			}

		}

		foreach(Room r in rooms)
		{
			foreach(Door d in r.Doors)
			{
				if(d.Rooms.Count==1)
				{
					Debug.Log(string.Format("Room #{0} contains door that leads to nothing",r.Number));
				}
				else if(d.Rooms.Count==2)
		        {

					Debug.Log(string.Format("Room #{0} contains door that leads to room #{1}",
					                        r.Number, d.GetAnotherRoom(r).Number));
				}	        
				else
				{
					Debug.Log(string.Format("Buggy room #{0}",r.Number));
				}
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

		M.House.ForEachWall(curCell.Position, (WallPoint wp, WallController wc) => {
			if(wc==null)
				return;
			if(wc.WallObject is DoorController)
			{
				Door door = null;
				if(!doors.TryGetValue(wp.toInt(),out door))
				{
					door = new Door();
					doors.Add(wp.toInt(),door);
				}

				door.AddRoom(curRoom);
				curRoom.AddDoor(door);
			}
			else if(wc.WallObject is EntranceController)
			{
				curRoom.Entrance = true;
			}
			else if(wc.WallObject is GarageGateController)
			{
				curRoom.GarageGate = true;
			}
		});

		foreach(CellController nc in reachable)
		{
			if(!processed.Contains(nc))
				Next (nc);
		}
	}

	WallController GetWall(int x, int y)
	{

		return M.House.GetWall(new WallPoint(x,y));
	}

	CellController GetCell(int x, int y)
	{
		return M.House.GetCell(new MapPoint(x,y));
	}


	/*
	void ForeachCrossCell(MapPoint p, Action<WallController, WallController, CellController> action)
	{
		CellController c;
		WallController w1,w2;
		//left
		c = GetCell(curCell.Position.X-1, curCell.Position.Y);
		w1 = GetWall(curCell.Position.X, curCell.Position.Y);
		w2 = GetWall(curCell.Position.X, curCell.Position.Y+1);
		action(w1,w2,c);
		
		//right
		c = GetCell(curCell.Position.X+1, curCell.Position.Y);
		w1 = GetWall(curCell.Position.X+1, curCell.Position.Y+1);
		w2 = GetWall(curCell.Position.X+1, curCell.Position.Y);
		action(w1,w2,c);

		//top
		c = GetCell(curCell.Position.X, curCell.Position.Y+1);
		w1 = GetWall(curCell.Position.X+1, curCell.Position.Y+1);
		w2 = GetWall(curCell.Position.X, curCell.Position.Y+1);
		action(w1,w2,c);

		//bottom
		c = GetCell(curCell.Position.X, curCell.Position.Y-1);
		w1 = GetWall(curCell.Position.X, curCell.Position.Y);
		w2 = GetWall(curCell.Position.X+1, curCell.Position.Y);
		action(w1,w2,c);
	}*/

}
