
public class HotRoom : BaseRule, IRoomRule
{
	public int S=1,K=1;
	public HotRoom(int a, int S,int K) : base(a) {
		this.S=S;
		this.K=K;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		int totalPower = 0;
		if(r.TypeOfRoom==RoomType.Coridor 
		   || r.TypeOfRoom==RoomType.Storage
		   || r.TypeOfRoom==RoomType.Garage)
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

		if(S*(totalPower-K*r.LogicCells.Count)>0)
			return true;
		return false;
	}


	#endregion


}


