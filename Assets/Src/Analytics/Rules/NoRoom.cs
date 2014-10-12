
public class NoRoom : BaseRule, IHouseRule
{
	RoomType[] Rooms;
		
	public NoRoom(int a, params RoomType[] rooms) : base(a) {
		Rooms = rooms;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		foreach(Room room in s.Rooms)
		{
			foreach(RoomType rt in Rooms)
				if(rt==room.TypeOfRoom)
					return false;
		}
		return true;
	}


	#endregion


}


