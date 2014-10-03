
public class StiffyRoom : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;
	public StiffyRoom(int a, RoomType TypeOfRoom) : base(a) {
		this.TypeOfRoom = TypeOfRoom;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
	
		if(r.TypeOfRoom==TypeOfRoom && r.Ventilated==false)
			return true;
		return false;
	}


	#endregion


}


