
using System;
public class LogicSink : ILogicObject, IHotWaterConsumer
{
	public bool HasHotWater = false;

	public LogicSink(MapRect objectRect, int cost) 
		: base(CellObjects.Sink,objectRect,cost)
	{
	
	}

	#region IHotWaterConsumer implementation

	public void SetHasHotWater (bool has)
	{
		HasHotWater=has;
	}

	#endregion
}

