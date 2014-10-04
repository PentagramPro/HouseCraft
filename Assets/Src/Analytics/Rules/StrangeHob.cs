using System.Collections.Generic;
using UnityEngine;

public class StrangeHob : BaseRule, IRoomRule
{
	public StrangeHob(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom!=RoomType.Dining ||
		   r.TypeOfRoom!=RoomType.Kitchen)
			return false;

		List<LogicHob> hobs = r.GetObjects<LogicHob>();
		foreach(LogicHob h in hobs)
		{
			MapPoint pos = new MapPoint(h.ObjectRect.MinX,h.ObjectRect.MinY);
			LogicCell cell = r.LogicCells[pos.toInt()];
			if(cell.ReachableCells.Count==4)
				return true;
		}

		return false;
	}


	#endregion


}


