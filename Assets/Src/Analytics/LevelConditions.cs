
using System;

[Serializable]
public class LevelConditions
{
	public int PricePerCell = 500;
	public int ExpectedIncome = 40000;

	public bool HotWater = true;

	// Per meter
	public int PlumbingCost = 50;
	public int VentsCost = 30;
	public int HeatingCost = 20;

	public SceneryType NorthScenery,
					SouthScenery,
					EastScenery,
					WestScenery;

	public string LevelName = "Level name";

}


