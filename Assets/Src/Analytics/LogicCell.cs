using System.Collections.Generic;
using System;
using UnityEngine;

public class LogicCell
{
	public int AdjacentWalls = 0;
	public MapPoint Position;
	public List<LogicCell> ReachableCells = new List<LogicCell>();
	public LogicCell (MapPoint position)
	{
		Position = position;
	}
	public Vector3 Center{
		get{
			return new Vector3(Position.X+0.5f,Position.Y+0.5f,0);
		}
	}
}


