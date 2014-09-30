
public class NoRoom : BaseRule, IHouseRule
{
	RoomType TypeOfRoom1 = RoomType.Unknown,
		TypeOfRoom2 = RoomType.Unknown;
	public NoRoom(RoomType type1,RoomType type2,int a) : base(a) {
		TypeOfRoom1 = type1;
		TypeOfRoom2 = type2;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		foreach(Room room in s.Rooms)
		{
			if(room.TypeOfRoom==TypeOfRoom1 || room.TypeOfRoom==TypeOfRoom2)
				return false;
		}
		return true;
	}


	#endregion


}


