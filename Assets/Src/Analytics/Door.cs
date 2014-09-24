using System.Collections.Generic;

public class Door
{
	public List<Room> Rooms = new List<Room>();
	public Door ()
	{
	}

	public void AddRoom(Room r)
	{
		if(Rooms.Contains(r))
			return;
		Rooms.Add(r);
	}

	public Room GetAnotherRoom(Room room)
	{
		foreach(Room r in Rooms)
		{
			if(r!=room)
				return r;
		}
		return null;
	}
}
