
using System;
using System.Collections.Generic;

public class LogicHeater : ILogicObject
{
	
	
	public LogicHeater(MapRect objectRect, int cost) 
		: base(CellObjects.Heater,objectRect,cost)
	{
		
	}
	
	
}

