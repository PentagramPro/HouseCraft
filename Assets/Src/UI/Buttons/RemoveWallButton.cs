using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveWallButton : BaseButtonController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	protected override void OnClick()
	{
		base.OnClick();
		M.House.SetHouseMode(HouseModes.RemoveWalls,null);
	}
}
