using UnityEngine;
using System.Collections.Generic;

public class LevelSelectorController : MonoBehaviour {

	public List<LevelItemController> Levels = new List<LevelItemController>();
	public NumericFieldController TotalProfit;
	LevelItemController selectedLevel = null;
	// Use this for initialization
	void Start () {
		PlayerProfile profile = GetComponent<PlayerProfile>();
		int total = 0;
		foreach(LevelItemController level in Levels)
		{
			level.LevelSelector = this;
			total+=profile.GetProfitForLevel(level.SceneName);
		}

		TotalProfit.Value = total;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnLevelSelected(LevelItemController l)
	{
		selectedLevel = l;
		foreach(LevelItemController level in Levels)
		{
			level.Deselect();
		}
	}

	public void OnPlay()
	{
		if(selectedLevel==null)
			return;

		Application.LoadLevel(selectedLevel.SceneName);
	}
}
