using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveObjectButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick()
	{
		M.House.SetHouseMode(HouseModes.RemoveObjects,null);
	}
}
