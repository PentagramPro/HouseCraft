using System.Collections.Generic;
using UnityEngine;

public class TightKitchen : BaseRule, IRoomRule
{
	public TightKitchen(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom!=RoomType.Dining ||
		   r.TypeOfRoom!=RoomType.Kitchen)
			return false;

		List<LogicHob> hobs = new List<LogicHob>();
		List<LogicSink> sinks = new List<LogicSink>();

		foreach(ILogicObject l in r.LogicObjects)
		{
			if(l.ObjectType==CellObjects.Hob)
				hobs.Add(l as LogicHob);
			else if(l.ObjectType==CellObjects.Sink)
				sinks.Add(l as LogicSink);
		}

		foreach(LogicHob hob in hobs)
		{
			foreach(LogicSink sink in sinks)
			{
				if(Vector3.Distance(sink.Center,hob.Center)<1.5f)
					return true;
			}
		}

		return false;
	}


	#endregion


}


