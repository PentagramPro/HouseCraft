using System;
using UnityEngine;

public class BaseRule
{
	public int Amount {
		get;internal set;
	}
	public BaseRule (int amount)
	{
		Amount = amount;
	}

	public string Name{
		get{
			return this.GetType().Name;
		}

	}

	public virtual string GetLocalizedName(Strings str)
	{
		string name = "Rule."+Name;
		if(str.HasString(name))
			return str[name];
		else
		{
			return Name;
			Debug.LogError("No localized name for rule "+Name);
		}
	}

	public virtual string GetLocalizedDescr(Strings str)
	{
		string name = "Rule."+Name+".Descr";
		if(str.HasString(name))
			return str[name];
		else
		{
			return "";
			Debug.LogError("No localized description for rule "+Name);
		}
	}
}


