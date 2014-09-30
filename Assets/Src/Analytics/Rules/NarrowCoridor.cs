
public class NarrowCoridor : BaseRule, IRoomRule
{
	public NarrowCoridor(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
		
		return false;
	}


	#endregion


}


