using System;

public class NoisyRoom : BaseRule, IRoomRule
{
	RoomType TypeOfRoom;
	public NoisyRoom(int a, RoomType type) : base(a) {
		TypeOfRoom = type;
	}

	public override string Name {
		get {
			return base.Name+": "+Enum.GetName(typeof(RoomType),TypeOfRoom);
		}
	}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		return r.TypeOfRoom==TypeOfRoom &&
			((s.Conditions.SouthScenery==SceneryType.Road && r.SouthWindows) ||
			 (s.Conditions.NorthScenery==SceneryType.Road && r.NorthWindows) ||
			 (s.Conditions.EastScenery==SceneryType.Road && r.EastWindows) ||
			 (s.Conditions.WestScenery==SceneryType.Road && r.WestWindows));

		return false;
	}


	#endregion


}


