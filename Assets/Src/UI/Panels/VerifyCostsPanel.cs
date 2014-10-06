using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerifyCostsPanel : BaseController {
	enum Modes{
		Wait,Scale,Idle
	}
	float t=0;
	Modes state = Modes.Wait;
	public float WaitDelay=2;
	public float Speed = 0.5f;

	public Vector2 lastPos = new Vector2(1,1);
	public Vector3 lastScale = new Vector3(0.7f,0.7f,1);

	public NumericFieldController StartPrice,
		Equipment,Communications,Penalty,TotalExpences;

	public NumericFieldController SellPrice,Bonus;

	public NumericFieldController Profit;

	RectTransform rect;
	// Use this for initialization
	void Start () {


	}

	void OnEnable()
	{
		t=0;
		state = Modes.Wait;
		rect = GetComponent<RectTransform>();
		rect.pivot = new Vector2(0.5f,0.5f);
		rect.localScale = new Vector3(1,1,1);

		StartPrice.Value = M.Statistic.StartPrice;
		Equipment.Value = M.Statistic.EquipmentCost;
		Communications.Value = M.Statistic.CommunicationsCost;
		Penalty.Value = M.Statistic.Penalty;
		TotalExpences.Value = M.Statistic.TotalExpences;
		
		SellPrice.Value = M.Statistic.SellPrice;
		Bonus.Value = M.Statistic.Bonus;
		
		Profit.Value = M.Statistic.Profit;
	}
	// Update is called once per frame
	void Update () {
		switch(state)
		{
		case Modes.Scale:
			rect.pivot = Vector2.MoveTowards(rect.pivot,lastPos,Time.smoothDeltaTime*Speed);
			rect.localScale = Vector3.MoveTowards(rect.localScale,lastScale,
			                                      Time.smoothDeltaTime*Speed);
			if(rect.pivot==lastPos && rect.localScale==lastScale)
				state = Modes.Idle;
			break;
		case Modes.Wait:
			t+=Time.smoothDeltaTime;
			if(t>WaitDelay)
			{
				state = Modes.Scale;
				t=0;
			}
			break;
		}
	}
}
