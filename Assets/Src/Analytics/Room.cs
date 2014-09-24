using UnityEngine;
using System.Collections.Generic;

public class Room  {

	public int Number=0;
	public List<CellController> Cells = new List<CellController>();
	public List<Door> Doors =new List<Door>();

	public void AddDoor(Door d)
	{
		if(Doors.Contains(d))
			return;
		Doors.Add(d);
	}
}
