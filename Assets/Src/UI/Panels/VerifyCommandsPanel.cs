using UnityEngine;
using System.Collections;

public class VerifyCommandsPanel : BaseController {

	public ConfirmButton ConfButton;
	// Use this for initialization
	void Start () {
	
	}

	void OnEnabled()
	{
		if(M.Statistic.Profit<=0)
			ConfButton.gameObject.SetActive(false);
		else
			ConfButton.gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
