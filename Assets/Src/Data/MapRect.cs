using System;
using UnityEngine;

public class MapRect
{
	MapPoint p1,p2;

	public int MinX{
		get{
			return p1.X;
		}
	}
	public int MinY{
		get{
			return p1.Y;
		}
	}
	public int MaxX{
		get{
			return p2.X;
		}
	}
	public int MaxY{
		get{
			return p2.Y;
		}
	}
	public MapRect (int x1,int y1, int x2, int y2)
	{
		int minX = Math.Min(x1,x2);
		int maxX = Math.Max(x1,x2);
		int minY = Math.Min(y1,y2);
		int maxY = Math.Max(y1,y2);
		this.p1=new MapPoint(minX,minY);
		this.p2=new MapPoint(maxX,maxY);
	}


	public MapRect(MapRect copy)
	{
		p1 = new MapPoint(copy.p1);
		p2 = new MapPoint(copy.p2);
	}

	public void Foreach(Action<MapPoint> action)
	{
		for(int x=p1.X;x<=p2.X;x++)
		{
			for(int y=p1.Y;y<=p2.Y;y++)
			{
				action(new MapPoint(x,y));
			}
		}
	}

	public override bool Equals (object obj)
	{
		MapRect p = obj as MapRect;
		return p1.Equals(p.p1) && p2.Equals(p.p2);
	}



}

