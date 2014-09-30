using System;

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
}


