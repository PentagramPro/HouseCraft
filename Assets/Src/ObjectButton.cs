using UnityEngine;
using UnityEngine.UI;

using System.Collections;

[RequireComponent (typeof (Button))]
public class ObjectButton : BaseController {

	public CellController CellPrefab;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		M.House.SetHouseMode(HouseModes.SetObject,CellPrefab.gameObject);
	}
}
