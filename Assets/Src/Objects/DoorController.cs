using UnityEngine;
using System.Collections;

public class DoorController : BaseController, IWallObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IWallObject implementation

	public void UpdateWall ()
	{
		
	}

	private bool PrefabGetNeighbours(Manager m, WallPoint point, out WallController w1, out WallController w2)
	{
		w1=null;w2=null;

		WallController left = m.House.GetWall(new WallPoint(point.X-1,point.Y));
		WallController right = m.House.GetWall(new WallPoint(point.X+1,point.Y));
		WallController top = m.House.GetWall(new WallPoint(point.X,point.Y+1));
		WallController bottom = m.House.GetWall(new WallPoint(point.X,point.Y-1));

		WallController cur = m.House.GetWall(point);
		if(cur!=null && cur.WallObject!=null)
		   return false;

		if(left!=null && right!=null)
		{
			if(top!=null || bottom!=null)
				return false;
			w1 = left;w2=right;
		}
		else if(top!=null && bottom!=null)
		{
			if(left!=null || right!=null)
				return false;
			w1 = bottom;w2 = top;
		}
		else
			return false;
		return true;
	}
	public void PrefabPrepareWall(Manager m, WallController wc)
	{
		WallPoint point = wc.Position;

		WallController left = m.House.GetWall(new WallPoint(point.X-1,point.Y));
		WallController right = m.House.GetWall(new WallPoint(point.X+1,point.Y));


		if(left!=null && right!=null)
		{
			wc.wallSprite.Left = true;
			wc.wallSprite.Right = true;
		}
		else
		{
			wc.wallSprite.Top = true;
			wc.wallSprite.Bottom = true;
		}



	}

	public bool PrefabValidatePosition(Manager m, WallPoint point)
	{
		WallController w1,w2;
		if(!PrefabGetNeighbours(m,point,out w1,out w2))
			return false;

		if(w1.WallObject!=null || w2.WallObject!=null)
			return false;

		return true;
	}

	#endregion
}
