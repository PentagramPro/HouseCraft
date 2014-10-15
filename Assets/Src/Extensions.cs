using System;
using UnityEngine;

public static class Extensions
{
	
	public static T Next<T>(this T src) where T : struct
	{
		if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
		
		T[] Arr = (T[])Enum.GetValues(src.GetType());
		int j = Array.IndexOf<T>(Arr, src) + 1;
		return (Arr.Length==j) ? Arr[0] : Arr[j];            
	}

	public static string GetLocalizedName(this RoomType src,Strings str) 
	{
		string name = "Room."+Enum.GetName(typeof(RoomType),src);
		if(str.HasString(name))
			return str[name];
		else
		{
			Debug.LogError("No name for room "+name);
			return name;
		}
	}

	public static string GetLocalizedName(this CellObjects src,Strings str) 
	{
		string name = "Object."+Enum.GetName(typeof(CellObjects),src);
		if(str.HasString(name))
			return str[name];
		else
		{
			Debug.LogError("No name for object "+name);
			return name;
		}
	}
}
