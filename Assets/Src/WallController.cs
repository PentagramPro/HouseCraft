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

			InternalUpdateWall();
		}
	}

	public void UpdateWall(bool immediately)
	{
		if(immediately)
		{
			if(houseController==null)
				houseController = GetComponentInParent<HouseController>();
			InternalUpdateWall();
		}
		else
			update = true;
	}

	private void InternalUpdateWall()
	{

		CellController tr = houseController.GetCell(new MapPoint(position.X,position.Y));
		CellController tl = houseController.GetCell(new MapPoint(position.X-1,position.Y));
		
		CellController br = houseController.GetCell(new MapPoint(position.X,position.Y-1));
		CellController bl = houseController.GetCell(new MapPoint(position.X-1,position.Y-1));
		
		wallSprite.Top = (tr==null) ^ (tl==null);
		wallSprite.Bottom = (br==null) ^ (bl==null);
		
		wallSprite.Right = (tr==null) ^ (br==null);
		wallSprite.Left = (tl==null) ^ (bl==null);
		
		wallSprite.UpdateWall();
	}
}
