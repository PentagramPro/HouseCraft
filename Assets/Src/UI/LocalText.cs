using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocalText : BaseController {
	Strings S;

	protected override void Awake ()
	{
		try
		{
			// managed mode
			base.Awake ();
			S = M.UI.S;
		}
		catch (UnityException e)
		{

		}
	}
	// Use this for initialization
	void Start () {
		if(S.HasString(name))
		{
			Text text = GetComponent<Text>();

			text.text = S[name];
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
