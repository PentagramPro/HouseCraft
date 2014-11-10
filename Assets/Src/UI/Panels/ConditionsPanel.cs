using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConditionsPanel : BaseController {

	public ConditionSideController ToNorth, ToSouth, ToEast, ToWest;
	public Text LevelName;


	// Use this for initialization
	void Start () {
		ToEast.SideType = M.House.LevelConditions.EastScenery;
		ToWest.SideType = M.House.LevelConditions.WestScenery;
		ToNorth.SideType = M.House.LevelConditions.NorthScenery;
		ToSouth.SideType = M.House.LevelConditions.SouthScenery;
		LevelName.text = LevelSelectorController.LocalizedLevelName;

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
