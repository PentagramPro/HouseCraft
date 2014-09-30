
using System;
public class LogicSink : ILogicObject, IHotWaterConsumer
{


	public LogicSink(MapRect objectRect, int cost) 
		: base(CellObjects.Sink,objectRect,cost)
	{
		HasHotWater = false;
	}

	#region IHotWaterConsumer implementation
	
	public bool HasHotWater {get;set;}
	
	#endregion
}

