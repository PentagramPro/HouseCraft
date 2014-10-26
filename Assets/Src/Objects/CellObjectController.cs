using UnityEngine;
using System.Collections;

public class CellObjectController : BaseController, ICellObject {

	public int Cost = 0;
	public bool Autorotate = false;


	public CellObjects ObjectType;


	// Use this for initialization
	void Start () {
		CellController cell = GetComponent<CellController>();
		CellController.CellRotation rotation = CellController.CellRotation.None;

		if(Autorotate && cell.SizeX==1 && cell.SizeY==1)
		{
			WallController bl = M.House.GetWall(new WallPoint(cell.Position.X,cell.Position.Y));
			WallController tr = M.House.GetWall(new WallPoint(cell.Position.X+1,cell.Position.Y+1));
			         
			if(bl!=null)
			{
				if(bl.wallSprite.Right)
					rotation = CellController.CellRotation.None;
				else if(bl.wallSprite.Top)
					rotation = CellController.CellRotation.C90;
			}
			if(tr!=null)
			{
				if(tr.wallSprite.Left)
					rotation = CellController.CellRotation.C180;
				else if(tr.wallSprite.Bottom)
					rotation = CellController.CellRotation.C270;
			}
			cell.SetRotation(rotation);
			                                  
			/*M.House.ForEachWall(cell.Position, (WallPoint wp, WallController wc, Corner corner) =>{
				switch(corner)
				{
				case Corner.BottomLeft:
					if(
					break;
				case Corner.BottomRight:
					break;
				case Corner.TopLeft:
					break;
				case Corner.TopRight:
					break;
				}
			});*/
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public ILogicObject Fabricate()
	{
		CellController cell = GetComponent<CellController>();
		switch(ObjectType)
		{
		case CellObjects.Bathtub:
			return new LogicBathtub(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Boiler:
			return new LogicBoiler(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Fireplace:
			return new LogicFireplace(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Heater:
			return new LogicHeater(cell.GetCurCellIndexes(),Cost);
		case CellObjects.HeatingPipe:
			return new LogicHeatingPipe(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Hob:
			return new LogicHob(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Riser:
			return new LogicRiser(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Shower:
			return new LogicShower(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Sink:
			return new LogicSink(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Toilet:
			return new LogicToilet(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Ventshaft:
			return new LogicVentshaft(cell.GetCurCellIndexes(),Cost);
		}
		return null;
	}
	#region ICellObject implementation

	public CellObjects GetCellObjectType ()
	{
		return ObjectType;
	}

	public int GetCost()
	{
		return Cost;
	}


	#endregion
}
