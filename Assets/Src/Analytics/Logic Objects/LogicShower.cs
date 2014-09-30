
using System;
using System.Collections.Generic;

public class LogicShower : ILogicObject, IHotWaterConsumer
{

	
	public LogicShower(MapRect objectRect, int cost) 
		: base(CellObjects.Shower,objectRect,cost)
	{
		HasHotWater = false;
	}
	
	
	#region IHotWaterConsumer implementation
	
	public bool HasHotWater {get;set;}
	
	#endregion
}
