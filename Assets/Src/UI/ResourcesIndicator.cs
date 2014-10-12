using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourcesIndicator : BaseController {

	public Image ImageColdWater,ImageHotWater,ImageSteam;

	public bool ColdWater{
		set{
			ImageColdWater.gameObject.SetActive(value);
		}
	}
	public bool HotWater{
		set{
			ImageHotWater.gameObject.SetActive(value);
		}
	}public bool Steam{
		set{
			ImageSteam.gameObject.SetActive(value);
		}
	}


	// Use this for initialization
	void Start () {
		ColdWater = true;
		HotWater = M.House.LevelConditions.HotWater;
		Steam = M.House.LevelConditions.HotWater;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
