using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		M.House.SwitchRotation();
	}
}
