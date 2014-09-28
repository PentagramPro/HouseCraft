using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalCostController : BaseController {

	Text text;
	int totalCost = 0;

	protected override void Awake ()
	{
		base.Awake ();
		text = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddCost(int cost)
	{
		totalCost+=cost;
		text.text = totalCost.ToString();
	}
}
