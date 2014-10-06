using UnityEngine;
using System.Collections;

public class Stats : BaseController {

	public int StartPrice;
	public int EquipmentCost;
	public int CommunicationsCost;
	public int Penalty;
	public int SellPrice;
	public int Bonus;

	public int TotalExpences{
		get{
			return StartPrice+EquipmentCost+CommunicationsCost+Penalty;
		}
	}

	public int Profit{
		get{
			return SellPrice+Bonus-TotalExpences;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
