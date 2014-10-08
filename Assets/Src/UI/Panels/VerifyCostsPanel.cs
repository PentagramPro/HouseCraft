using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerifyCostsPanel : BaseController {


	Animator animator;

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


		StartPrice.Value = M.Statistic.StartPrice;
		Equipment.Value = M.Statistic.EquipmentCost;
		Communications.Value = M.Statistic.CommunicationsCost;
		Penalty.Value = M.Statistic.Penalty;
		TotalExpences.Value = M.Statistic.TotalExpences;
		
		SellPrice.Value = M.Statistic.SellPrice;
		Bonus.Value = M.Statistic.Bonus;
		
		Profit.Value = M.Statistic.Profit;

		animator = GetComponent<Animator>();
		animator.SetTrigger("Start");
	}
	// Update is called once per frame
	void Update () {

	}
}
