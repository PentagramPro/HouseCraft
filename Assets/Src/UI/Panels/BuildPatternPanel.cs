using UnityEngine;
using System.Collections;

public class BuildPatternPanel : MonoBehaviour {
	public Color SelectionColor;
	BaseButtonController lastButton = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonSelected(BaseButtonController button)
	{
		if(lastButton!=null)
			lastButton.BackgroundColor = Color.white;

		button.BackgroundColor = SelectionColor;
		lastButton = button;
	}
}
