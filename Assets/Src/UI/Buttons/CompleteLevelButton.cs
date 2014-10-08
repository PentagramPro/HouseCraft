﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompleteLevelButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		M.OnCompleteLevel();
	}
}
