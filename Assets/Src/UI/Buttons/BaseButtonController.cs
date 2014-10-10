using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseButtonController : BaseController {

	BuildPatternPanel patternPanel;
	protected Image backgroundImage;

	public Color BackgroundColor{
		set{
			backgroundImage.color = value;
		}
	}


	protected override void Awake()
	{
		base.Awake();
		backgroundImage = GetComponent<Image>();
		patternPanel = GetComponentInParent<BuildPatternPanel>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected  virtual void OnClick()
	{
		patternPanel.OnButtonSelected(this);
	}
}
