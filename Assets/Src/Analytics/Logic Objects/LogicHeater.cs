
using System;
using System.Collections.Generic;

public class LogicHeater : ILogicObject
{
	public bool Operates = false;
	public int Power = 10;
	
	public LogicHeater(MapRect objectRect, int cost) 
		: base(CellObjects.Heater,objectRect,cost)
	{
		
	}
	
	
}

