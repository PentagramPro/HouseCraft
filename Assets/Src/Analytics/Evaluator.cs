using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Segmentator))]
public class Evaluator : BaseController {

	Segmentator segmentator;
	public int Penalty{
		get; internal set;
	}
	List<IRoomRule> RoomRules = new List<IRoomRule>()
	{
		new CleanHands(100)
	};
	protected override void Awake ()
	{
		base.Awake ();
		segmentator = GetComponent<Segmentator>();


	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Launch()
	{
		Penalty = 0;
		foreach(IRoomRule rule in RoomRules)
		{
			foreach(Room room in segmentator.Rooms)
			{
				if(rule.Process(segmentator,room))
					Penalty-=rule.Amount;	
			}
		}
		Debug.Log("Total penalty: "+Penalty);
		M.OnProcessed(segmentator,this);
	}
}
