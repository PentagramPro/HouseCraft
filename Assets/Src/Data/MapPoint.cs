using UnityEngine;
using System;

[Serializable]
public class MapPoint : IntegerPoint
{


	public MapPoint()
	{

	}
	public MapPoint (int x, int y) : base(x,y)
	{

	}

	public MapPoint(MapPoint p) : base(p.X,p.Y)
	{

	}

	 
	public static bool operator ==(MapPoint p1, MapPoint p2)
	{
		return p1.X==p2.X && p1.Y==p2.Y;
	}
	
	public static bool operator !=(MapPoint p1, MapPoint p2)
	{
		return p1.X!=p2.X || p1.Y!=p2.Y;
	}

}


