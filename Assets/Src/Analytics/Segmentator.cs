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

		// STEP 1 - Find rooms
		Debug.Log("Starting segmentation");
		rooms.Clear();
		processed.Clear();
		foreach(CellController cell in cells.Values)
		{
			if(processed.Contains(cell))
				continue;
			Next (cell);
			if(curRoom.Size>0)
			{

				rooms.Add(curRoom);
				Debug.Log(string.Format("Found room #{1} with {0} cells",curRoom.Size,curRoom.Number));
				curRoom = new Room();
				curRoom.Number = rooms.Count;
			}

		}


		// STEP 2 - Recognize rooms
		Recognize();


		foreach(Room r in rooms)
		{
			M.Overlay.DrawRoom(r);
			Debug.Log(string.Format("Room #{0} is {1}", r.Number, Enum.GetName(typeof(RoomType),r.TypeOfRoom)));
			foreach(Door d in r.Doors)
			{
				if(d.Rooms.Count==1)
				{
					Debug.Log(string.Format("  Room #{0} contains door that leads to nothing",r.Number));
				}
				else if(d.Rooms.Count==2)
		        {

					Debug.Log(string.Format("  Room #{0} contains door that leads to room #{1}",
					                        r.Number, d.GetAnotherRoom(r).Number));
				}	        
				else
				{
					Debug.Log(string.Format("  Buggy room #{0}",r.Number));
				}
			}
		}
		M.OnProcessed();
	}

	void Recognize()
	{
		List<Room> unrecognized = new List<Room>();

		// PHASE 1 - small rooms
		foreach(Room r in rooms)
		{
			r.PrepareToRecognition();


			if(r.GarageGate)
			{
				r.TypeOfRoom = RoomType.Garage;
			}
			else if(r.ConnectedTo.Count==1)
			{
				if(r.Empty && r.Size<=4)
					r.TypeOfRoom = RoomType.Storage;
				else
				{
					bool bathtub = r.Contains(CellObjects.Bathtub);
					bool toilet = r.Contains(CellObjects.Toilet);
					bool shower = r.Contains(CellObjects.Shower);
					if(toilet)
					{
						if(bathtub || shower)
							r.TypeOfRoom = RoomType.ToiletBathroom;
						else
							r.TypeOfRoom = RoomType.Toilet;
					}
					else if(bathtub || shower)
						r.TypeOfRoom = RoomType.Bathroom;
				}
			}

			if(r.TypeOfRoom==RoomType.Unknown)
			{
				// coridor detector
				if(!r.ContainsRectangle(3,3))
				{
					r.TypeOfRoom = RoomType.Coridor;
				}
				else
				{
					unrecognized.Add(r);
				}
			}

		}

		// PHASE 2 - kitchen
		if(unrecognized.Count==0)
			return;
		else if(unrecognized.Count==1)
		{
			unrecognized[0].TypeOfRoom = RoomType.Studio;
			return;
		}
		Room kitchen = null, smallest = unrecognized[0];

		foreach(Room r in unrecognized)
		{
			if(r.Contains(CellObjects.Hob))
			{
				if(kitchen==null || kitchen.Size>r.Size)
					kitchen = r;
			}

			if(smallest.Size>r.Size)
				smallest = r;
		}

		if(kitchen==null)
			kitchen = smallest;
		if(kitchen.Size>16)
			kitchen.TypeOfRoom = RoomType.Dining;
		else
			kitchen.TypeOfRoom = RoomType.Kitchen;

		unrecognized.Remove(kitchen);

		// PHASE 3 - bedrooms

		foreach(Room r in unrecognized)
		{
			r.TypeOfRoom = RoomType.Bedroom;
		}
	}

	void Next(CellController curCell)
	{
		List<CellController> reachable = new List<CellController>();
		WallController w = null;
		CellController c = null;

		curRoom.AddCell(curCell);
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
