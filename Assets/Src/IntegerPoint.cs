using UnityEngine;
using System;

[Serializable]
public class IntegerPoint
{
	public int X;
	public int Y;

	public MapPoint()
	{
		X = 0;Y = 0;
	}
	public MapPoint (int x, int y)
	{
		this.X=x;
		this.Y=y;
	}

	public void Clamp(int width, int height)
	{
		X = Mathf.Clamp(X,0,width-1);
		Y = Mathf.Clamp(Y,0,height-1);
	}

	public int toInt()
	{
		return X << 16 | Y;
	}

	public void fromInt(int v)
	{
		X = v >> 16;
		Y = (v & 0xffff);
	}
}


