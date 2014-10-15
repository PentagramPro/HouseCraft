using System;

public class DarkRoom : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;

	public DarkRoom(int a, RoomType type) : base(a) {
		TypeOfRoom = type;
	}

	/*public override string Name {
		get {
			return base.Name+": "+Enum.GetName(typeof(RoomType),TypeOfRoom);
		}
	}*/
	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom==TypeOfRoom)
		{
			if(!r.SouthWindows && !r.NorthWindows && !r.EastWindows && !r.WestWindows)
				return true;
		}
		return false;
	}


	#endregion


}


