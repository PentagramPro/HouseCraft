using UnityEngine;
using System.Collections;

public class UIController : BaseController {
	enum Modes{
		Build,Verify,Results
	}


	public BuildCommandsPanel bCommandsPanel;
	public BuildCostsPanel bCostsPanel;
	public BuildPatternPanel bPatternPanel;

	public VerifyCommandsPanel vCommandsPanel;
	public VerifyCostsPanel vCostsPanel;

	public ResultsPanel ResultsPanel;

	Modes state = Modes.Build;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShowVerify()
	{
		bCommandsPanel.gameObject.SetActive(false);
		bCostsPanel.gameObject.SetActive(false);
		bPatternPanel.gameObject.SetActive(false);

		vCommandsPanel.gameObject.SetActive(true);
		vCostsPanel.gameObject.SetActive(true);

		ResultsPanel.gameObject.SetActive(false);

		if(M.Statistic.Profit<=0)
			vCommandsPanel.ConfButton.gameObject.SetActive(false);
		else
			vCommandsPanel.ConfButton.gameObject.SetActive(true);
	}

	public void OnShowBuild()
	{
		bCommandsPanel.gameObject.SetActive(true);
		bCostsPanel.gameObject.SetActive(true);
		bPatternPanel.gameObject.SetActive(true);
		
		vCommandsPanel.gameObject.SetActive(false);
		vCostsPanel.gameObject.SetActive(false);

		ResultsPanel.gameObject.SetActive(false);
	}

	public void OnShowResults()
	{
		bCommandsPanel.gameObject.SetActive(false);
		bCostsPanel.gameObject.SetActive(false);
		bPatternPanel.gameObject.SetActive(false);
		
		vCommandsPanel.gameObject.SetActive(false);
		vCostsPanel.gameObject.SetActive(false);

		ResultsPanel.gameObject.SetActive(true);
	}
}
