
public class ColdHouse : BaseRule, IHouseRule
{
	public ColdHouse(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		int totalPower = 0;
		foreach(LogicHeater h in s.LCache.Heaters)
		{
			if(h.Operates)
				totalPower+=h.Power;
		}
		foreach(LogicFireplace f in s.LCache.Fireplaces)
			totalPower+=f.Power;

		if(totalPower<s.CellsCount)
			return true;
		return false;
	}


	#endregion


}


