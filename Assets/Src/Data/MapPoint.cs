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

}


