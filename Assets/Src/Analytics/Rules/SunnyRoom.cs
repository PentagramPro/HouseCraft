using System;

public class SunnyRoom : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;
	
	public SunnyRoom(int a, RoomType type) : base(a) {
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
			if(r.SouthWindows)
				return true;
		}
		return false;
	}
	
	
	#endregion
	
	
}


