﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : BaseController {
	enum Modes{
		Build,Verify,Results
	}
	public Strings S{get
		{
			return stringLoader.S;
		}
	}



	List<GameObject> panels;
	StringLoaderController stringLoader;

	public ConditionsPanel ConditionsPanel;

	public BuildCommandsPanel bCommandsPanel;
	public BuildCostsPanel bCostsPanel;
	public BuildPatternPanel bPatternPanel;

	public VerifyCommandsPanel vCommandsPanel;
	public VerifyCostsPanel vCostsPanel;

	public ResultsPanel ResultsPanel;
	public RectTransform WaitPanel;

	Modes state = Modes.Build;
	protected override void Awake ()
	{

		base.Awake ();
		stringLoader = GetComponent<StringLoaderController>();
	



		panels = new List<GameObject>(){
			ConditionsPanel.gameObject,
			bCommandsPanel.gameObject,
			bCostsPanel.gameObject,
			bPatternPanel.gameObject,
			vCommandsPanel.gameObject,
			vCostsPanel.gameObject,
			ResultsPanel.gameObject
		};
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShowVerify()
	{
		HideAll();

		vCommandsPanel.gameObject.SetActive(true);
		vCostsPanel.gameObject.SetActive(true);


		if(M.Statistic.Profit<=0)
			vCommandsPanel.ConfButton.gameObject.SetActive(false);
		else
			vCommandsPanel.ConfButton.gameObject.SetActive(true);
	}

	public void OnShowWait(bool show)
	{
		WaitPanel.gameObject.SetActive(show);
	}

	public void OnShowBuild()
	{
		HideAll();

		bCommandsPanel.gameObject.SetActive(true);
		bCostsPanel.gameObject.SetActive(true);
		bPatternPanel.gameObject.SetActive(true);
		

	}

	public void OnShowResults()
	{
		HideAll();

		ResultsPanel.gameObject.SetActive(true);
	}

	public void OnShowConditions()
	{
		HideAll();

		ConditionsPanel.gameObject.SetActive(true);
	}
	void HideAll()
	{
		foreach(GameObject o in panels)
		{
			o.SetActive(false);
		}
	}
}
