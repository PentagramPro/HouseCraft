using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CancelSaleButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick()
	{
		M.OnCancelSale();
	}
}
