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

	#region ICellObject implementation

	public CellObjects GetCellObjectType ()
	{
		return ObjectType;
	}

	#endregion
}
