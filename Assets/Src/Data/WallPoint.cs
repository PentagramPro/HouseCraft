using UnityEngine;
using System;

[Serializable]
public class WallPoint : MapPoint
{

	public WallPoint()
	{

	}
	public WallPoint (int x, int y) : base(x,y)
	{

	}

	public void Foreach(System.Action<WallPoint,Side> action)
	{
		for(int i=0;i<4;i++)
		{
			WallPoint ip = new WallPoint(X+xtable[i],Y+ytable[i]);
			action(ip,sidetable[i]);
		}
	}
}


