
public class HotRoom : BaseRule, IRoomRule
{

	public HotRoom(int a) : base(a) {

	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		int totalPower = 0;
		if(r.TypeOfRoom==RoomType.Coridor 
		   || r.TypeOfRoom==RoomType.Storage
		   || r.TypeOfRoom==RoomType.Garage
		   || r.TypeOfRoom==RoomType.Bathroom
		   || r.TypeOfRoom==RoomType.ToiletBathroom)
			return false;

		foreach(ILogicObject o in r.LogicObjects)
		{
			if(o is LogicHeater)
			{
				LogicHeater heater = o as LogicHeater;
				if(heater.Operates)
					totalPower+=heater.Power;
			}
			/*if(o is LogicFireplace)
			{
				LogicFireplace fire = o as LogicFireplace;
				totalPower+=fire.Power;
			}*/
		}

		if(totalPower>r.LogicCells.Count*2)
			return true;
		return false;
	}


	#endregion


}


