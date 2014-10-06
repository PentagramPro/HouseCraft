using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof (Segmentator))]
public class Evaluator : BaseController {

	Segmentator segmentator;
	public int Penalty{
		get; internal set;
	}
	public int Bonus{
		get; internal set;
	}

	List<IRoomRule> RoomRules = new List<IRoomRule>()
	{
		new CleanHands(100),
		new NarrowCoridor(-500),
		new HotRoom(-200),
		new ColdRoom(-200),
		new RoomWithout(RoomType.Kitchen,CellObjects.Hob,-500),
		new RoomWithout(RoomType.Dining,CellObjects.Hob,-500),
		new RoomWithout(RoomType.Kitchen,CellObjects.Sink,-500),
		new RoomWithout(RoomType.Dining,CellObjects.Sink,-500),
		new StiffyRoom(-100,RoomType.Kitchen),
		new StiffyRoom(-100,RoomType.Dining),
		new StiffyRoom(-500,RoomType.Bathroom),
		new StiffyRoom(-500,RoomType.ToiletBathroom),
		new StiffyRoom(-500,RoomType.Toilet),
		new StiffyRoom(-500,RoomType.Garage),
		new TightKitchen(-200),
		new DarkRoom(-1000,RoomType.Kitchen),
		new DarkRoom(-1000,RoomType.Dining),
		new DarkRoom(-1000,RoomType.Bedroom),
		new SunnyRoom(-50,RoomType.Kitchen),
		new FreshAir(1000,RoomType.Bedroom),
		new FreshAir(-500,RoomType.Bedroom),
		new SmallRoom(-1000, RoomType.Garage,3,3),
		new UglyObject(-400)

	};
	List<IObjectRule> ObjectRules = new List<IObjectRule>()
	{
		new ColdWater(-100),
		new BlockedDoor(-200)
	};

	List<IHouseRule> HouseRules = new List<IHouseRule>()
	{
		new NoRoom(RoomType.Bedroom,RoomType.Bedroom,-2000),
		new NoRoom(RoomType.Kitchen,RoomType.Dining,-2000),
		new NoRoom(RoomType.Bathroom,RoomType.ToiletBathroom,-2000),
		new NoRoom(RoomType.Toilet,RoomType.ToiletBathroom,-2000),
		new ColdHouse(-500),
		new WastingSpace(-1000)
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
		foreach(IHouseRule rule in HouseRules)
		{
			if(rule.Process(segmentator))
				ApplyRule(rule as BaseRule);
		}
		Debug.Log("Total penalty: "+Penalty);
		M.Statistic.Penalty = Penalty;
		M.Statistic.Bonus = Bonus;
		M.OnProcessed(segmentator,this);
	}

	void ApplyRule(BaseRule rule)
	{
		if(rule.Amount<0)
			Penalty-=rule.Amount;
		else
			Bonus+=rule.Amount;
		Debug.Log(string.Format("Rule '{0}' for ${1}",rule.Name,rule.Amount));
	}
}
