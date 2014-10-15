
public class ColdRoom : BaseRule, IRoomRule
{

	public ColdRoom(int a) : base(a) {

	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		int totalPower = 0;
		if(r.TypeOfRoom==RoomType.Coridor 
		   || r.TypeOfRoom==RoomType.Storage
		   || r.TypeOfRoom==RoomType.Garage
		   || r.TypeOfRoom==RoomType.Toilet)
			return false;

		foreach(ILogicObject o in r.LogicObjects)
		{
			if(o is LogicHeater)
			{
				LogicHeater heater = o as LogicHeater;
				if(heater.Operates)
					totalPower+=heater.Power;
			}
		}

		if(totalPower<r.LogicCells.Count)
			return true;
		return false;
	}


	#endregion

	public override string GetLocalizedName (Strings str)
	{
		return base.GetLocalizedName (str);
	}
}


