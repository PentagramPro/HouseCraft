﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WallButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick()
	{
		M.House.SetHouseMode(HouseModes.SetWalls,null);
	}
}
