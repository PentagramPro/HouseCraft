
public class CleanHands : BaseRule, IRoomRule
{
	public CleanHands(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom!=RoomType.Bathroom &&
		   r.TypeOfRoom!=RoomType.ToiletBathroom)
			return false;

		return r.Contains(CellObjects.Sink);

	}

	#endregion


}


