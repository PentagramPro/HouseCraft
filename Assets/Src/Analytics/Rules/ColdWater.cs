
public class ColdWater : BaseRule, IObjectRule
{
	public ColdWater(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s, ILogicObject o)
	{
		if(o is IHotWaterConsumer)
		{
			IHotWaterConsumer hot = o as IHotWaterConsumer;
			if(!hot.HasHotWater)
				return true;
		}
		return false;
	}

	#endregion


}


