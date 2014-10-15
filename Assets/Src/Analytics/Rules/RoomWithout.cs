
public class RoomWithout : BaseRule, IRoomRule
{
	RoomType Room;
	CellObjects ObjectType;
	public RoomWithout(RoomType room, CellObjects type, int a) : base(a) {
		Room = room;
		ObjectType = type;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom==Room && !r.Contains(ObjectType))
			return true;
		return false;
	}


	#endregion

	public override string GetLocalizedName (Strings str)
	{
		return base.GetLocalizedName (str)+ObjectType.GetLocalizedName(str);
	}

}


