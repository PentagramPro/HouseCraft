
using System;
public class LogicRiser : ILogicObject
{
	public bool HasHotWater = false;

	public LogicRiser(MapRect objectRect, int cost) 
		: base(CellObjects.Riser,objectRect,cost)
	{
	
	}
}

