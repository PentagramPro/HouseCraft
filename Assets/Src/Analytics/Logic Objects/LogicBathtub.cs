
using System;
public class LogicBathtub : ILogicObject, IHotWaterConsumer
{
	public bool HasHotWater = false;
	public LogicBathtub(MapRect objectRect, int cost) 
		: base(CellObjects.Bathtub,objectRect,cost)
	{
	
	}

	#region IHotWaterConsumer implementation

	public void SetHasHotWater (bool has)
	{
		HasHotWater=has;
	}

	#endregion
}

