using UnityEngine;
using System.Collections;

public class EntranceController : BaseController, IWallObject {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IWallObject implementation

	public void UpdateWall ()
	{
		
	}

	public bool PrefabValidatePosition(Manager m, WallPoint point)
	{
		return false;
	}
	
	
	public void PrefabPrepareWall(Manager m, WallController wc)
	{
		throw new System.NotImplementedException ();
	}
	
	
	#endregion
}
