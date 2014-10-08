using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConditionSideController : BaseController {

	public Sprite Houses,Road,Park;

	public SceneryType SideType{
		set{
			Image image = GetComponent<Image>();
			switch(value)
			{
			case SceneryType.Houses:
				image.sprite = Houses;
				break;
			case SceneryType.Road:
				image.sprite = Road;
				break;
			case SceneryType.Park:
				image.sprite = Park;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
