using UnityEngine;
using System.Collections;

[RequireComponent (typeof (StructureController))]
public class CellController : BaseController {

	public MapPoint position;

	public MapPoint Position{
		set{
			transform.localPosition = new Vector3(value.X+0.5f,value.Y+0.5f, 0);
			position = value;
		}

		get{
			return position;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
