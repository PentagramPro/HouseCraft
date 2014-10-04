
public class SmallRoom : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;
	int sizeX,sizeY;
	public SmallRoom(int a, RoomType type, int sizeX, int sizeY) : base(a) {
		TypeOfRoom = type;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
	}

	public override string Name {
		get {
			return base.Name+": "+Enum.GetName(typeof(RoomType),TypeOfRoom);
		}
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		if(r.TypeOfRoom==type)
		{
			return !r.ContainsRectangle(sizeX,sizeY);
		}
		return false;
	}


	#endregion


}


