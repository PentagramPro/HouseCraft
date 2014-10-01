using System.Collections.Generic;
using System;

public class LogicCell
{

	public MapPoint Position;
	public List<LogicCell> ReachableCells = new List<LogicCell>();
	public LogicCell (MapPoint position)
	{
		Position = position;
	}
}


