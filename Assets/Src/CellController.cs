using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (StructureController))]
public class CellController : BaseController {

	public enum CellRotation {
		None, C90, C180, C270
	};

	Dictionary<CellRotation,float> angles = new Dictionary<CellRotation, float>()
	{
		{CellRotation.None,0},
		{CellRotation.C90,-90},
		{CellRotation.C180,-180},
		{CellRotation.C270,90}
	};
	public ICellObject CellObject;
	public int SizeX = 1, SizeY = 1;

	public MapPoint position;
	HouseController houseController;


	public MapPoint Position{
		set{
			transform.localPosition = new Vector3(value.X+0.5f,value.Y+0.5f, 0);
			position = value;
		}

		get{
			return position;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();
		CellObject = GetComponentInterface<ICellObject>();
		houseController = GetComponentInParent<HouseController>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// we need manager here because we call this method in prefab context
	public bool PrefabValidatePosition(Manager m, MapPoint point, CellRotation rotation)
	{


		MapRect rect = GetCellIndexes(point,rotation);

		bool res = m.House.Phantom.Place(rect);



		rect.Foreach((MapPoint p) => {

			CellController c =m.House.GetCell(p);
			if(c==null || c.CellObject!=null)
			{
				res = false;
				m.House.Phantom.SetRed(p);
			}

			if(p.X>rect.MinX && p.Y>rect.MinY)
			{
				WallController w = m.House.GetWall(new WallPoint(p.X,p.Y));
				if(w!=null)
					res = false;
			}

		});




		return res;
	}

	public MapRect GetCellIndexes(MapPoint point, CellRotation rotation)
	{
		int minX = point.X;
		int minY = point.Y;
		int maxX = minX;
		int maxY = minY;

		
		switch(rotation)
		{
		case CellRotation.None:
			maxX += SizeX-1;
			maxY += SizeY-1;
			break;
		case CellRotation.C90:
			maxX += SizeY-1;
			minY -= SizeX-1;
			
			break;
		case CellRotation.C180:
			minX -= SizeX-1;
			minY -= SizeY-1;
			
			break;
		case CellRotation.C270:
			minX -= SizeY-1;
			maxY += SizeX-1;
			
			break;
		}

		return new MapRect(minX,minY,maxX,maxY);
	}

	public void SetRotation(CellRotation rotation)
	{
		transform.localRotation = Quaternion.Euler(0,0,angles[rotation]);
	}
	public static CellController InstantiateMe(CellController prefab, Transform parent, MapPoint point)
	{
		CellController newCell = Instantiate<CellController>(prefab);
		newCell.transform.parent = parent;
		newCell.Position = point;
		return newCell;
	}
}

