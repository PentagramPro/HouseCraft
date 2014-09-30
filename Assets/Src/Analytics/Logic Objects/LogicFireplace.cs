
using System;
using System.Collections.Generic;

public class LogicFireplace : ILogicObject
{


	public LogicFireplace(MapRect objectRect, int cost) 
		: base(CellObjects.Fireplace,objectRect,cost)
	{
	
	}


}

