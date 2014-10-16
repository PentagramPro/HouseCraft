
using System;
using System.Collections.Generic;

public class LogicFireplace : ILogicObject
{
	public int Power = 80;

	public LogicFireplace(MapRect objectRect, int cost) 
		: base(CellObjects.Fireplace,objectRect,cost)
	{
	
	}


}

