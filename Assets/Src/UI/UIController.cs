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

	Modes state = Modes.Build;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
