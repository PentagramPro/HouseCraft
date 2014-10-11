using UnityEngine;
public class EnoughBedrooms : BaseRule, IHouseRule
{
	public EnoughBedrooms(int a) : base(a) {}

	#region IRoomRule implementation

	public bool Process (Segmentator s)
	{
		return s.LCache.BedroomsCount>((s.Conditions.FamilySize+1)/2);
	}


	#endregion


}


