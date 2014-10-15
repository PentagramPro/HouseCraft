using System.Collections.Generic;

using System;

public class AppliedRule: IComparable
{
	public string Name{get;internal set;}
	public string Description{get;internal set;}
	public int Cost{get;internal set;}

	public AppliedRule (string name, string description, int cost)
	{
		Name = name;
		Description = description;
		Cost = cost;
	}

	#region IComparable implementation

	public int CompareTo (object obj)
	{
		if (obj == null) return 1;

		AppliedRule ar = obj as AppliedRule;

		return Cost.CompareTo(ar.Cost);
	}

	#endregion
}


