using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocalText : BaseController {

	// Use this for initialization
	void Start () {
		if(M.UI.S.HasString(name))
		{
			Text text = GetComponent<Text>();

			text.text = M.UI.S[name];
		}
		else
		{
			Debug.LogError("String not found for "+name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
