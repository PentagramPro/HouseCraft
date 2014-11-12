using UnityEngine;
using System.Collections.Generic;
public class LogicCache
{
	public List<ILogicObject> Objects = new List<ILogicObject>();
	public List<LogicRiser> Risers = new List<LogicRiser>();
	public List<IHotWaterConsumer> HotWaterConsumers = new List<IHotWaterConsumer>();
	public List<LogicBoiler> Boilers = new List<LogicBoiler>();
	public List<LogicHeatingPipe> HeatingPipes = new List<LogicHeatingPipe>();
	public List<LogicHeater> Heaters = new List<LogicHeater>();
	public List<LogicFireplace> Fireplaces = new List<LogicFireplace>();
	public List<LogicVentshaft> Vents = new List<LogicVentshaft>();
	public List<LogicBathtub> Bathtubs = new List<LogicBathtub>();

	public int BedroomsCount=0;
	public int ToiletsCount=0;

	public void Clear()
	{
		BedroomsCount = 0;
		ToiletsCount = 0;
		Risers.Clear();
		HotWaterConsumers.Clear();
		Boilers.Clear();
		Objects.Clear();
		HeatingPipes.Clear();
		Heaters.Clear();
		Vents.Clear();
		Fireplaces.Clear();
		Bathtubs.Clear();
	}

	public T FindClosest<T>(List<T> lobjects, Vector3 position) where T : ILogicObject
	{
		T closest = null;
		float min = 0;
		foreach(T l in lobjects)
		{
			float dist = Vector3.Distance(position,l.Center);
			if(closest==null || min>dist)
			{
				closest = l;
				min = dist;
			}

		}
		return closest;
	}
}

