
public class UglyObject : BaseRule, IRoomRule
{
	public UglyObject(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		return r.Contains(CellObjects.Ventshaft) || r.Contains(CellObjects.Riser);
	}


	#endregion


}


