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
		int minX=0,minY=0;
		int maxX = minX, maxY = minY;

		GetCellIndexes(point,rotation,ref minX,ref minY,ref maxX,ref maxY);

		bool res = true;

		for(int x=minX;x<=maxX;x++)
		{
			for(int y=minY;y<=maxY;y++)
			{
				CellController c =m.House.GetCell(new MapPoint(x,y));
				if(c==null || c.CellObject!=null)
				{
					res = false;
					break;
				}
			}
			if(!res)
				break;
		}

		if(res)
		{
			for(int x=minX+1;x<=maxX;x++)
			{
				for(int y=minY+1;y<=maxY;y++)
				{
					WallController w = m.House.GetWall(new WallPoint(x,y));
					if(w!=null)
					{
						res = false;
						break;
					}
				}
				if(!res)
					break;
			}
		}

		return res;
	}

	public void GetCellIndexes(MapPoint point, CellRotation rotation,
	                           ref int minX, ref int minY, ref int maxX, ref int maxY)
	{
		minX = point.X; minY = point.Y;
		maxX = minX; maxY = minY;
		
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

