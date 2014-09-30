
public class RuleTemplate : BaseRule, IRoomRule
{
	public RuleTemplate(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, Room r)
	{
	
		return false;
	}


	#endregion


}


