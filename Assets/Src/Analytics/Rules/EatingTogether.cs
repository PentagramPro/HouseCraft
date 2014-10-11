
public class EatingTogether : BaseRule, IRoomRule
{
	public EatingTogether(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		return r.TypeOfRoom==RoomType.Dining && s.Conditions.FamilySize>=4;
	}


	#endregion


}


