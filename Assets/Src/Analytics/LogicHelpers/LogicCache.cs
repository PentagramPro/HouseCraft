using UnityEngine;
using System.Collections.Generic;
public class LogicCache
{
	public List<LogicRiser> Risers = new List<LogicRiser>();
	public List<IHotWaterConsumer> HotWaterConsumers = new List<IHotWaterConsumer>();
	public List<LogicBoiler> Boilers = new List<LogicBoiler>();

	public void Clear()
	{
		Risers.Clear();
		HotWaterConsumers.Clear();
		Boilers.Clear();
	}

	public T FindClosest<T>(List<T> lobjects, Vector3 position) where T : ILogicObject
	{
		T closest = null;
		float min = 0;
		foreach(T l in lobjects)
		{
			float dist = Vector3.Distance(position,l.Center);
			if(closest==null || min<dist)
			{
				closest = l;
				min = dist;
			}

		}
		return closest;
	}
}

