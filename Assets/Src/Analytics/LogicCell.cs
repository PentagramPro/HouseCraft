using System.Collections.Generic;
using System;

public class LogicCell
{
	public MapPoint Position;
	public List<MapPoint> ReachableCells = new List<MapPoint>();
	public LogicCell (MapPoint position)
	{
		Position = position;
	}
}


