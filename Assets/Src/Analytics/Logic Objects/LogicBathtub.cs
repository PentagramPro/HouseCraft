
using System;
public class LogicBathtub : ILogicObject
{
	public bool HasHotWater = false;
	public LogicBathtub(MapRect objectRect, int cost) 
		: base(CellObjects.Bathtub,objectRect,cost)
	{
	
	}
}

