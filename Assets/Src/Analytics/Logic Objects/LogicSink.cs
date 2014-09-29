
using System;
public class LogicSink : ILogicObject
{
	public bool HasHotWater = false;

	public LogicSink(MapRect objectRect, int cost) 
		: base(CellObjects.Sink,objectRect,cost)
	{
	
	}
}

