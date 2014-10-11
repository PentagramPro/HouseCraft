
public class Studio : BaseRule, IRoomRule
{
	public Studio(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(s.Conditions.FamilySize==2 && r.TypeOfRoom==RoomType.Studio)
			return true;

		return false;
	}


	#endregion


}


