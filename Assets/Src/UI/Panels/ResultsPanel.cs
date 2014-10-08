using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsPanel : BaseController {

	public BrokenRulesPanel BrokenRules;
	public NumericFieldController Profit;

	public Animator animator;
	// Use this for initialization
	void Start () {


	}

	void OnEnable()
	{
		Profit.Value = M.Statistic.Profit;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnSecondStep()
	{
		BrokenRules.SlideIn();
	}
}
