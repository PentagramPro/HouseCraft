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
			break;
		case CellObjects.Fireplace:
			break;
		case CellObjects.Heater:
			break;
		case CellObjects.HeatingPipe:
			break;
		case CellObjects.Hob:
			break;
		case CellObjects.Riser:
			return new LogicRiser(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Shower:
			break;
		case CellObjects.Sink:
			return new LogicSink(cell.GetCurCellIndexes(),Cost);
		case CellObjects.Toilet:
			break;
		case CellObjects.Ventshaft:
			break;
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
