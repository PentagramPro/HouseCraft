using UnityEngine;
using System;
using System.Collections.Generic;


[RequireComponent (typeof (Evaluator))]
public class Segmentator : BaseController {

	Evaluator evaluator;

	Dictionary<int,LogicCell> LogicCells = new Dictionary<int, LogicCell>();
	List<LogicCell> processed = new List<LogicCell>();
	List<Room> rooms = new List<Room>();
	Dictionary<int, Door> doors = new Dictionary<int, Door>();
	Room curRoom = null;
	public LogicCache LCache = new LogicCache();
	LevelConditions Conditions;

	public int CellsCount{
		get{return LogicCells.Count;}
	}
	public List<Room> Rooms{
		get{return rooms;}
	}
	public int ExpencesCommunications{
		get; internal set;
	}

	protected override void Awake ()
	{
		base.Awake ();
		evaluator = GetComponent<Evaluator>();
	}

	public void Launch(LevelConditions conditions, Dictionary<int,CellController> cells)
	{
		rooms.Clear();
		processed.Clear();
		doors.Clear();
		LCache.Clear();
		LogicCells.Clear();
		curRoom = new Room();
		Conditions = conditions;

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

		foreach(LogicCell cell in LogicCells.Values)
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


	void Connections()
	{
		ExpencesCommunications = 0;
		float plumbingLen = 0;
		float heatingLen = 0;
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

		ExpencesCommunications =  (int)(plumbingLen*Conditions.PlumbingCost
		                                +heatingLen*Conditions.HeatingCost);
	}

	void Next(LogicCell curCell)
	{


		curRoom.AddCell(curCell,LCache,M.House.GetCell(curCell.Position));
		processed.Add(curCell);


		M.House.ForEachWall(curCell.Position, (WallPoint wp, WallController wc) => {
			if(wc==null)
				return;
			if(wc.WallObject is DoorController)
			{
				Door door = null;
				if(!doors.TryGetValue(wp.toInt(),out door))
				{
					door = new Door(wp);
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
