
public class EnoughToilets : BaseRule, IHouseRule
{
	public EnoughToilets(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		return s.LCache.ToiletsCount>=2 && s.Conditions.FamilySize>=4;
	}


	#endregion


}


