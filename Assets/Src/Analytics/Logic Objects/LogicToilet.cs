
using System;
using System.Collections.Generic;

public class LogicToilet : ILogicObject
{
	
	
	public LogicToilet(MapRect objectRect, int cost) 
		: base(CellObjects.Toilet,objectRect,cost)
	{
		
	}
	
	
}
