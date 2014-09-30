
using System;
public class LogicBathtub : ILogicObject, IHotWaterConsumer
{


	public LogicBathtub(MapRect objectRect, int cost) 
		: base(CellObjects.Bathtub,objectRect,cost)
	{
		HasHotWater = false;
	}

	#region IHotWaterConsumer implementation

	public bool HasHotWater {get;set;}

	#endregion
}

