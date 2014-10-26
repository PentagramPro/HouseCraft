using UnityEngine;
using System;
using System.Collections.Generic;


[RequireComponent (typeof (Evaluator))]
public class Segmentator : BaseController {

	Evaluator evaluator;

	public Dictionary<int,LogicCell> LogicCells = new Dictionary<int, LogicCell>();
	public Dictionary<int, LogicWall> LogicWalls = new Dictionary<int, LogicWall>();
	List<LogicCell> processed = new List<LogicCell>();
	List<Room> rooms = new List<Room>();
	public Dictionary<int, Door> Doors = new Dictionary<int, Door>();

	Room curRoom = null;
	public LogicCache LCache = new LogicCache();
	public LevelConditions Conditions;

	public int CellsCount{
		get{return LogicCells.Count;}
	}
	public List<Room> Rooms{
		get{return rooms;}
	}
	/*public int ExpencesCommunications{
		get; internal set;
	}*/

	protected override void Awake ()
	{
		base.Awake ();
		evaluator = GetComponent<Evaluator>();
	}

	public void Launch(LevelConditions conditions, Dictionary<int,CellController> cells,
	                   Dictionary<int,WallController> walls)
	{
		rooms.Clear();
		processed.Clear();
		Doors.Clear();
		LCache.Clear();
		LogicCells.Clear();
		Conditions = conditions;

		// Filling logic walls
		foreach(WallController w in walls.Values)
			LogicWalls[w.Position.toInt()]  = new LogicWall(w.Position,w.wallSprite.Top,
			                                                w.wallSprite.Bottom,
			                                                w.wallSprite.Left,
			                                                w.wallSprite.Right);

		// Filling logic cells
		foreach(CellController cell in cells.Values)
		{
			if(LogicCells.ContainsKey(cell.Position.toInt()))
				continue;
			LogicCells.Add(cell.Position.toInt(),new LogicCell(cell.Position));
			if(cell.SizeX>1 || cell.SizeY>1)
			{
				MapRect rect = cell.GetCurCellIndexes();
				rect.Foreach((MapPoint p) => {
					if(!LogicCells.ContainsKey(p.toInt()))
						LogicCells.Add(p.toInt(),new LogicCell(p));
				});

			}

		}

		// Calculating reaches
		foreach(LogicCell cell in LogicCells.Values)
		{
			WallController w = null;
			LogicCell c = null;
		
			
			//left
			c = GetLogicCell(cell.Position.X-1, cell.Position.Y);
			w = GetWall(cell.Position.X, cell.Position.Y);
			if(c!=null && (w==null || w.wallSprite.Top==false))
				cell.ReachableCells.Add(c);


			//right
			c = GetLogicCell(cell.Position.X+1, cell.Position.Y);
			w = GetWall(cell.Position.X+1, cell.Position.Y+1);
			if(c!=null && (w==null || w.wallSprite.Bottom==false))
				cell.ReachableCells.Add(c);
			
			//top
			c = GetLogicCell(cell.Position.X, cell.Position.Y+1);
			w = GetWall(cell.Position.X+1, cell.Position.Y+1);
			if(c!=null && (w==null || w.wallSprite.Left==false))
				cell.ReachableCells.Add(c);
			
			//bottom
			c = GetLogicCell(cell.Position.X, cell.Position.Y-1);
			w = GetWall(cell.Position.X, cell.Position.Y);
			if(c!=null && (w==null || w.wallSprite.Right==false))
				cell.ReachableCells.Add(c);


			cell.AdjacentWalls+=GetWall(cell.Position.X, cell.Position.Y)==null?0:1;
			cell.AdjacentWalls+=GetWall(cell.Position.X+1, cell.Position.Y)==null?0:1;
			cell.AdjacentWalls+=GetWall(cell.Position.X, cell.Position.Y+1)==null?0:1;
			cell.AdjacentWalls+=GetWall(cell.Position.X+1, cell.Position.Y+1)==null?0:1;
		}
		// Finding Rooms
		curRoom = new Room(this);
		foreach(LogicCell cell in LogicCells.Values)
		{
			if(processed.Contains(cell))
				continue;
			Next (cell);
			if(curRoom.Size>0)
			{

				rooms.Add(curRoom);
				Debug.Log(string.Format("Found room #{1} with {0} cells",curRoom.Size,curRoom.Number));
				curRoom = new Room(this);
				curRoom.Number = rooms.Count;
			}

		}


		// STEP 2 - Recognize rooms
		Recognize();

		// STEP 3 - Calculate connections
		Connections();

		foreach(Room r in rooms)
		{
			M.Overlay.DrawRoom(r);
			Debug.Log(string.Format("Room #{0} is {1}, {2} objects", r.Number,
			                        Enum.GetName(typeof(RoomType),r.TypeOfRoom),
			                        r.LogicObjects.Count));
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
		evaluator.Launch();
	}

	bool FilterRoom(Room room, List<Room> local, List<Room> global)
	{
		bool preserve = room.Entrance || room.GarageGate;
		local.Add(room);
		global.Add(room);

		foreach(Room r in room.ConnectedTo)
		{
			if(!global.Contains(r))
				preserve |= FilterRoom(r,local,global);
		}
		return preserve;
	}
	void Recognize()
	{
		foreach(Room r in Rooms)
		{
			r.PrepareToRecognition();
		}


		// FILTER - remove rooms which cannot be entered
		List<Room> filterProcessed = new List<Room>();
		List<Room> roomsToRemove = new List<Room>();
		foreach(Room room in Rooms)
		{
			if(filterProcessed.Contains(room))
				continue;

			List<Room> local = new List<Room>();
			if(!FilterRoom(room,local,filterProcessed))
			{
				roomsToRemove.AddRange(local);
			}
			
		}

		foreach(Room room in roomsToRemove)
		{
			Rooms.Remove(room);

			foreach(Door door in room.Doors)
			{
				if(Doors.ContainsKey(door.Position.toInt()))
				{
					Doors.Remove(door.Position.toInt());
				}
			}
		}

		List<Room> unrecognized = new List<Room>();

		// PHASE 1 - small rooms
		foreach(Room r in rooms)
		{



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

						LCache.ToiletsCount++;
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
			LCache.BedroomsCount++;
		}
	}


	void Connections()
	{
		//ExpencesCommunications = 0;
		float plumbingLen = 0;
		float heatingLen = 0;
		float ventsLen = 0;
		// HOT WATER
		// looking for closest boiler
		foreach(LogicRiser l in LCache.Risers)
		{
			LogicBoiler b = LCache.FindClosest<LogicBoiler>(LCache.Boilers,l.Center);
			if(b!=null)
			{
				plumbingLen+=Vector3.Distance(b.Center,l.Center);
				l.HasHotWater = true;
			}
			else
			{
				l.HasHotWater = Conditions.HotWater;
			}
		}

		//looking for risers
		foreach(IHotWaterConsumer lh in LCache.HotWaterConsumers)
		{
			ILogicObject l = lh as ILogicObject;
			LogicRiser r = LCache.FindClosest<LogicRiser>(LCache.Risers,l.Center);
			if(r!=null)
			{
				plumbingLen+=Vector3.Distance(r.Center,l.Center);
				lh.HasHotWater = r.HasHotWater;
			}
		}

		// HEATING
		foreach(LogicHeatingPipe h in LCache.HeatingPipes)
			h.HasHotWater = Conditions.HotWater;

		foreach(LogicHeater h in LCache.Heaters)
		{
			LogicHeatingPipe pipe = LCache.FindClosest<LogicHeatingPipe>(LCache.HeatingPipes,h.Center);
			if(pipe!=null)
			{
				heatingLen+=Vector3.Distance(h.Center,pipe.Center);
				h.Operates = pipe.HasHotWater;
			}
		}

		if(LCache.Vents.Count>0)
		{
			// Vents
			foreach(Room r in Rooms)
			{
				if(r.TypeOfRoom!=RoomType.Kitchen &&
				   r.TypeOfRoom!=RoomType.Garage &&
				   r.TypeOfRoom!=RoomType.Dining &&
				   r.TypeOfRoom!=RoomType.Bathroom &&
				   r.TypeOfRoom!=RoomType.Toilet &&
				   r.TypeOfRoom!=RoomType.ToiletBathroom)
					continue;

				float distance = -1;
				LogicVentshaft closest = null;
				foreach(LogicVentshaft v in LCache.Vents)
				{
					float d = r.CalcDistance(v.Center);
					if(closest==null || distance>d)
					{
						closest = v;
						distance = d;
					}
				}
				ventsLen+=distance;
				r.Ventilated = true;

			}
		}
		Debug.Log(string.Format("pipes len {0}, heating len {1}, vents len {2}",
		                        plumbingLen,heatingLen,ventsLen));
		M.Statistic.CommunicationsCost =  (int)(plumbingLen*Conditions.PlumbingCost
		                                +heatingLen*Conditions.HeatingCost
		                                +ventsLen*Conditions.VentsCost);
	}

	void Next(LogicCell curCell)
	{


		curRoom.AddCell(curCell,LCache,M.House.GetCell(curCell.Position));
		processed.Add(curCell);


		M.House.ForEachWall(curCell.Position, (WallPoint wp, WallController wc,Corner corn) => {
			if(wc==null)
				return;
			if(wc.WallObject is DoorController)
			{
				Door door = null;
				if(!Doors.TryGetValue(wp.toInt(),out door))
				{
					door = new Door(wp);
					Doors.Add(wp.toInt(),door);
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
			else if(wc.WallObject is WindowController)
			{
				WindowController window = wc.WallObject as WindowController;
				curRoom.NorthWindows |= window.North;
				curRoom.SouthWindows |= window.South;
				curRoom.EastWindows |= window.East;
				curRoom.WestWindows |= window.West;
			}
		});

		foreach(LogicCell nc in curCell.ReachableCells)
		{
			if(!processed.Contains(nc))
				Next (nc);
		}
	}

	LogicCell GetLogicCell(int x, int y)
	{
		int key = new MapPoint(x,y).toInt();
		if(LogicCells.ContainsKey(key))
			return LogicCells[key];
		return null;
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
