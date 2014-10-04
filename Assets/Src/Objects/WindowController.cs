using UnityEngine;
using System.Collections;

public class WindowController : BaseController, IWallObject {

	public bool North=false,South=false,East=false,West=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EditorUpdateWall(HouseController house)
	{
		WallController wc = GetComponent<WallController>();

		MapPoint p;
		bool ne=false,nw=false,se=false,sw=false;

		p = new MapPoint(wc.Position.X, wc.Position.Y);
		if(house.GetCell(p)==null)
			ne=true;

		p = new MapPoint(wc.Position.X-1, wc.Position.Y);
		if(house.GetCell(p)==null)
			nw=true;

		p = new MapPoint(wc.Position.X, wc.Position.Y-1);
		if(house.GetCell(p)==null)
			se=true;

		p = new MapPoint(wc.Position.X-1, wc.Position.Y-1);
		if(house.GetCell(p)==null)
			sw=true;

		North = ne && nw;
		South = se && sw;
		East = ne && se;
		West = nw && sw;

	}

	#region IWallObject implementation
	

	public void UpdateWall ()
	{

	}


	public bool PrefabValidatePosition(Manager m, WallPoint point)
	{
		return false;
	}
	

	public void PrefabPrepareWall(Manager m, WallController wc)
	{
		throw new System.NotImplementedException ();
	}


	#endregion
}
