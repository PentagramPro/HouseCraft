using UnityEngine;
using System.Collections;

public class BrokenRulesPanel : BaseController {

	Animator animator;
	public BrokenRule BrokenRulePrefab;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddRule(string Name, int Cost)
	{
		BrokenRule r = Instantiate<BrokenRule>(BrokenRulePrefab);
		r.Name = Name;
		r.Value = Cost;
		r.transform.parent = transform;
	}

	public void SlideIn()
	{
		foreach(AppliedRule r in M.Statistic.WorstRulesBroken)
		{
			AddRule(r.Name,Mathf.Abs(r.Cost));
		}
		animator.SetTrigger("Start");
	}
}
