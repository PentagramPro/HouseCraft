using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveWallButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void OnClick()
	{
		M.House.SetHouseMode(HouseModes.RemoveWalls,null);
	}
}
