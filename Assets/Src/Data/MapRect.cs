using System;

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
	public MapRect (MapPoint p1, MapPoint p2)
	{
		this.p1=p1;
		this.p2=p2;
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

}
