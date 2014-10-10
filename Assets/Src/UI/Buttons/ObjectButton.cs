using UnityEngine;
using UnityEngine.UI;

using System.Collections;

[RequireComponent (typeof (Button))]
public class ObjectButton : BaseController {

	public CellController CellPrefab;
	public NumericFieldController CostText;
	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);

		int cost = CellPrefab.PrefabGetCost();
		if(CostText!=null)
			CostText.Value = cost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		M.House.SetHouseMode(HouseModes.SetObject,CellPrefab.gameObject);
	}
}
