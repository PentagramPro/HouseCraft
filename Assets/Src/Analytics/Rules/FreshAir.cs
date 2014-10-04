
public class FreshAir : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;
	public FreshAir(int a, RoomType type) : base(a) {
		TypeOfRoom = type;
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		return r.TypeOfRoom==TypeOfRoom &&
			((s.Conditions.SouthScenery==SceneryType.Park && r.SouthWindows) ||
			 (s.Conditions.NorthScenery==SceneryType.Park && r.NorthWindows) ||
			 (s.Conditions.EastScenery==SceneryType.Park && r.EastWindows) ||
			 (s.Conditions.WestScenery==SceneryType.Park && r.WestWindows));

	}


	#endregion


}


