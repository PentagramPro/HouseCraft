
public class WastingSpace : BaseRule, IHouseRule
{
	public WastingSpace(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		int coridors = 0;
		foreach(Room r in s.Rooms)
			if(r.TypeOfRoom==RoomType.Coridor)
				coridors+=r.LogicCells.Count;

		float portion = (float)coridors / (float)s.LogicCells.Count;

		return portion>=0.25f;
	}


	#endregion


}


