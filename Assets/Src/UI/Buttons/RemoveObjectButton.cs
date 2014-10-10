using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveObjectButton : BaseButtonController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	protected override void OnClick()
	{
		base.OnClick();
		M.House.SetHouseMode(HouseModes.RemoveObjects,null);
	}
}
