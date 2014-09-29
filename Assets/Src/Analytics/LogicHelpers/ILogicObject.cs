using System;
using UnityEngine;

public abstract class ILogicObject
{
	public CellObjects ObjectType {get; internal set;}
	public MapRect ObjectRect {get; internal set;}
	public int Cost {get; internal set;}

	public Vector3 Center{
		get{
			return ObjectRect.Center;
		}
	}
	public ILogicObject(CellObjects objectType, MapRect objectRect, int cost)
	{
		ObjectType = objectType;
		ObjectRect = objectRect;
		Cost = cost;
	}


}

