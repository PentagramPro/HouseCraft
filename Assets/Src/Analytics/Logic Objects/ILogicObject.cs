using System;
public abstract class ILogicObject
{
	public CellObjects ObjectType {get; internal set;}
	public MapRect ObjectRect {get; internal set;}
	public int Cost {get; internal set;}

	public ILogicObject(CellObjects objectType, MapRect objectRect, int cost)
	{
		ObjectType = objectType;
		ObjectRect = objectRect;
		Cost = cost;
	}


}

