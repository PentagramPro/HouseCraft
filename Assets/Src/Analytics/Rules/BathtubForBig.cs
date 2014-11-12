
public class BathtubForBig : BaseRule, IHouseRule
{
	public BathtubForBig(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
	
		return s.LCache.Bathtubs.Count>0 && s.Conditions.FamilySize>=4;

	}


	#endregion


}


