using UnityEngine;
using System.Collections.Generic;

public class Room  {
	public RoomType TypeOfRoom = RoomType.Unknown;
	public bool Entrance = false;
	public bool GarageGate = false;
	public int Number=0;
	public List<CellController> Cells = new List<CellController>();
	public List<Door> Doors =new List<Door>();
	public List<Room> ConnectedTo = new List<Room>();
	public bool Empty = true;

	public MapPoint LabelPos = null;
	//public int LabelSize;

	public void AddDoor(Door d)
	{
		if(Doors.Contains(d))
			return;
		Doors.Add(d);
	}

	public bool ContainsRectangle(int sizeX, int sizeY)
	{
		foreach(CellController c in Cells)
		{
			if(c.IsRectFree(
				new MapRect(c.Position.X, c.Position.Y, c.Position.X+sizeX-1,c.Position.Y+sizeY-1)))
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
			else if(LabelPos.X<=c.Position.X || LabelPos.Y>=c.Position.Y)
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
}
