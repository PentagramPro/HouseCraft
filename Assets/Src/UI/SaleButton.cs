using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaleButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick()
	{
		M.Sale();
	}
}
