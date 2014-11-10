using UnityEngine;
using System.Collections.Generic;

public class LevelSelectorController : MonoBehaviour {

	public static string LocalizedLevelName = "";
	//public List<LevelItemController> Levels = new List<LevelItemController>();
	LevelItemController[] Levels;
	public NumericFieldController TotalProfit;
	LevelItemController selectedLevel = null;
	PlayerProfile profile;

	public InstructionsPanelController InstructionsPanel;
	public Animator ProfitPanelAnimator;

	// Use this for initialization
	void Start () {
		profile = GetComponent<PlayerProfile>();
		Levels= GetComponentsInChildren<LevelItemController>();
		int total = 0;
		foreach(LevelItemController level in Levels)
		{
			level.LevelSelector = this;
			total+=profile.GetProfitForLevel(level.SceneName);
		}

		TotalProfit.Value = total;

	}

	void OnEnable()
	{
		ProfitPanelAnimator.SetTrigger("Start");
	}

	void OnDisable()
	{
		ProfitPanelAnimator.SetTrigger("Stop");
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void OnInstructions()
	{
		InstructionsPanel.gameObject.SetActive(true);
	}

	public void OnCloseInstructions()
	{
		InstructionsPanel.gameObject.SetActive(false);
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

		LocalizedLevelName = selectedLevel.LevelNameLabel.text;
		Application.LoadLevel(selectedLevel.SceneName);
	}

	public void OnReset()
	{
		profile.Reset();
	}
}
