using UnityEngine;
using System.Collections;

public class CellObjectController : BaseController, ICellObject {

	public int Cost = 0;


	public CellObjects ObjectType;


	// Use this for initialization
	void Start () {
	
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
