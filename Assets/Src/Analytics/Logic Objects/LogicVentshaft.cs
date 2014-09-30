
using System;
using System.Collections.Generic;

public class LogicVentshaft : ILogicObject
{
	
	
	public LogicVentshaft(MapRect objectRect, int cost) 
		: base(CellObjects.Ventshaft,objectRect,cost)
	{
		
	}
	
	
}
