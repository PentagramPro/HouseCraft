using UnityEngine;
using System.Collections;

[RequireComponent (typeof (StructureController))]
public class WallController : BaseController {

	bool update = true;
	HouseController houseController;
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
			if(w!=null && w.wallSprite.Right)
				wallSprite.Left = true;

			w = houseController.GetWall(new WallPoint(position.X+1,position.Y));
			if(w!=null && w.wallSprite.Left)
				wallSprite.Right = true;

			w = houseController.GetWall(new WallPoint(position.X,position.Y-1));
			if(w!=null && w.wallSprite.Top)
				wallSprite.Bottom = true;

			w = houseController.GetWall(new WallPoint(position.X,position.Y+1));
			if(w!=null && w.wallSprite.Bottom)
				wallSprite.Top = true;

			wallSprite.UpdateWall();
		}
	}

	public void UpdateWall( )
	{

			update = true;
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
		{

			return true;
		}
		wallSprite.Top = (tr==null) ^ (tl==null);
		wallSprite.Bottom = (br==null) ^ (bl==null);
		
		wallSprite.Right = (tr==null) ^ (br==null);
		wallSprite.Left = (tl==null) ^ (bl==null);
		
		wallSprite.UpdateWall();

		return false;
	}
}
