using UnityEngine;
using System.Collections;

[RequireComponent (typeof (StructureController))]
public class WallController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
