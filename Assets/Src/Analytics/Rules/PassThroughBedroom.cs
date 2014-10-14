
public class PassThroughBedroom : BaseRule, IRoomRule
{
	public PassThroughBedroom(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom!=RoomType.Bedroom)
			return false;

		int count = 0;
		foreach(Room n in r.ConnectedTo)
		{
			if(n.TypeOfRoom!=RoomType.Toilet && n.TypeOfRoom!=RoomType.ToiletBathroom)
				count++;
		}
		if(count>1)
			return true;
		
		return false;
	}


	#endregion


}


