using UnityEngine;
using System.Collections.Generic;

public class Room  {

	public readonly Color[] RoomColors = new Color[] {
		new Color(1,0,0, 0.4f),
		new Color(0,1,0, 0.4f),
		new Color(0,0,1, 0.4f),
		new Color(1,1,0, 0.4f),
		new Color(0,1,1, 0.4f),
		new Color(1,0,1, 0.4f),
		new Color(0.5f,1,0, 0.4f)

	};
	public RoomType TypeOfRoom = RoomType.Unknown;
	public bool Entrance = false;
	public bool GarageGate = false;
	public int Number=0;
	List<CellController> Cells = new List<CellController>();
	public List<MapPoint> VirtualCells = new List<MapPoint>();
	public List<Door> Doors =new List<Door>();
	public List<Room> ConnectedTo = new List<Room>();
	public bool Empty = true;

	public MapPoint LabelPos = null;
	//public int LabelSize;
	public Color RoomColor{
		get{
			return RoomColors[Number % RoomColors.Length];
		}
	}

	public int Size{
		get{
			return Cells.Count+VirtualCells.Count;
		}
	}
	public string Name{
		get{
			return System.Enum.GetName(typeof(RoomType),TypeOfRoom);
		}
	}

	public void AddDoor(Door d)
	{
		if(Doors.Contains(d))
			return;
		Doors.Add(d);
	}

	public void AddCell(CellController cell)
	{
		Cells.Add(cell);
		if(cell.SizeX>1 || cell.SizeY>1)
		{
			for(int x=0;x<cell.SizeX;x++)
			{
				for(int y = 0;y<cell.SizeY;y++)
				{
					if(x==0 && y==0)
						continue;
					VirtualCells.Add(new MapPoint(cell.Position.X+x,cell.Position.Y+y));
				}
			}
		}
	}

	public bool ContainsRectangle(int sizeX, int sizeY)
	{
		foreach(CellController c in Cells)
		{
			if(c.IsRectFree(
				new MapRect(c.Position.X, c.Position.Y, c.Position.X+sizeX-1,c.Position.Y+sizeY-1),false))
			{
			   return true;
			}
		}
		return false;

	}

	public void PrepareToRecognition()
	{
		ConnectedTo.Clear();
		LabelPos = null;
		Empty = true;
		foreach(CellController c in Cells)
		{
			if(LabelPos==null)
			{
				LabelPos = c.Position;
			}
			else if(LabelPos.X>=c.Position.X && LabelPos.Y<=c.Position.Y)
			{
				LabelPos = c.Position;
			}

			if(c.CellObject!=null)
			{
				Empty = false;
			}
		}

		foreach(Door d in Doors)
		{
			if(d.Rooms.Count == 2)
			{
				ConnectedTo.Add(d.GetAnotherRoom(this));
			}
		}
		
	}


	public bool Contains(CellObjects o)
	{
		foreach(CellController c in Cells)
		{
			if(c.CellObject!=null && c.CellObject.GetCellObjectType()==o)
				return true;
		}
		return false;
	}

	public void ForeachCell(System.Action<MapPoint> action)
	{
		foreach(CellController c in Cells)
		{
			action(c.Position);
		}

		foreach(MapPoint p in VirtualCells)
		{
			action(p);
		}
	}
}
