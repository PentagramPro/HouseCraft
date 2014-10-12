using UnityEngine;
using System.Collections;

[RequireComponent (typeof (StructureController))]
public class WallController : BaseController {

	bool update = true;
	HouseController houseController;
	public IWallObject WallObject;

	public WallGfxController wallSprite;

	public WallPoint position;



	public WallPoint Position{
		set{
			transform.localPosition = new Vector3(value.X,value.Y, 0);
			position = value;
		}
		
		get{
			return position;
		}
	}


	protected override void Awake ()
	{
		base.Awake ();
		houseController = GetComponentInParent<HouseController>();
		WallObject = GetComponentInterface<IWallObject>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(update)
		{
			update = false;
			if(houseController==null)
				houseController = GetComponentInParent<HouseController>();

			WallController w = houseController.GetWall(new WallPoint(position.X-1,position.Y));
			if(w==null)
				wallSprite.Left = false;
			else if(w.wallSprite.Right)
				wallSprite.Left = true;

			w = houseController.GetWall(new WallPoint(position.X+1,position.Y));
			if(w==null)
				wallSprite.Right = false;
			else if(w.wallSprite.Left)
				wallSprite.Right = true;

			w = houseController.GetWall(new WallPoint(position.X,position.Y-1));
			if(w==null)
				wallSprite.Bottom = false;
			else if(w.wallSprite.Top)
				wallSprite.Bottom = true;

			w = houseController.GetWall(new WallPoint(position.X,position.Y+1));
			if(w==null)
				wallSprite.Top = false;
			else if(w.wallSprite.Bottom)
				wallSprite.Top = true;

			wallSprite.UpdateWall();
			if(WallObject!=null)
				WallObject.UpdateWall();
		}
	}

	public void UpdateWall( )
	{

			update = true;
	}

	public WallController PrefabSetWall(Manager m, WallPoint point)
	{
		IWallObject wo = GetComponentInterface<IWallObject>();
		WallController w = m.House.ReplaceWall(point,this);
		if(wo!=null)
		{
			wo.PrefabPrepareWall(m,w);
		}
		return w;
	}

	public bool PrefabIsDoor()
	{
		IWallObject wo = GetComponentInterface<IWallObject>();
		return (wo!=null && wo is DoorController);
	}
	public bool PrefabValidatePosition(Manager m, WallPoint point)
	{
		IWallObject wo = GetComponentInterface<IWallObject>();

		if(wo!=null && wo.PrefabValidatePosition(m,point)==false)
			return false;

		if(!m.House.IsInsideBuilding(point))
			return false;
		WallController w = null;

		w = m.House.GetWall(new WallPoint(point.X-1,point.Y));
		if(w!=null && w.WallObject!=null)
			return false;
		w = m.House.GetWall(new WallPoint(point.X+1,point.Y));
		if(w!=null && w.WallObject!=null)
			return false;
		w = m.House.GetWall(new WallPoint(point.X,point.Y-1));
		if(w!=null && w.WallObject!=null)
			return false;
		w = m.House.GetWall(new WallPoint(point.X,point.Y+1));
		if(w!=null && w.WallObject!=null)
			return false;

		return true;
	}

	// returns true if wall has to be destroyed
	public bool EditorUpdateWall()
	{
		if(houseController==null)
			houseController = GetComponentInParent<HouseController>();
		CellController tr = houseController.GetCell(new MapPoint(position.X,position.Y));
		CellController tl = houseController.GetCell(new MapPoint(position.X-1,position.Y));
		
		CellController br = houseController.GetCell(new MapPoint(position.X,position.Y-1));
		CellController bl = houseController.GetCell(new MapPoint(position.X-1,position.Y-1));

		if(tr!=null && tl!=null && br!=null && bl!=null)
			return true;
		if(tr==null && tl==null && br==null && bl==null)
			return true;


		wallSprite.Top = (tr==null) ^ (tl==null);
		wallSprite.Bottom = (br==null) ^ (bl==null);
		
		wallSprite.Right = (tr==null) ^ (br==null);
		wallSprite.Left = (tl==null) ^ (bl==null);
		
		wallSprite.UpdateWall();

		IWallObject wo = GetComponentInterface<IWallObject>();
		if(wo!=null && wo is WindowController)
			(wo as WindowController).EditorUpdateWall(houseController);
		return false;
	}
}
