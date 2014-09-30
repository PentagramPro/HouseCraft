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
	List<IObjectRule> ObjectRules = new List<IObjectRule>()
	{
		new ColdWater(-100)
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
					ApplyRule(rule as BaseRule);

			}
		}
		foreach(IObjectRule rule in ObjectRules)
		{
			foreach(ILogicObject obj in segmentator.LCache.Objects)
			{
				if(rule.Process(segmentator,obj))
					ApplyRule(rule as BaseRule);
			}
		}
		Debug.Log("Total penalty: "+Penalty);
		M.OnProcessed(segmentator,this);
	}

	void ApplyRule(BaseRule rule)
	{
		Penalty-=rule.Amount;
		Debug.Log(string.Format("Rule '{0}' for ${1}",rule.Name,rule.Amount));
	}
}
