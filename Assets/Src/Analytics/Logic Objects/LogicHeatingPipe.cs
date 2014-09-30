
using System;
using System.Collections.Generic;

public class LogicHeatingPipe : ILogicObject
{
	
	
	public LogicHeatingPipe(MapRect objectRect, int cost) 
		: base(CellObjects.HeatingPipe,objectRect,cost)
	{
		
	}
	
	
}

