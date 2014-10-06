using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmButton : BaseController {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		M.OnVerified();
	}
}
