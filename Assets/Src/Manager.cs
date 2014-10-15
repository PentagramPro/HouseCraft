using UnityEngine;
using System.Collections.Generic;

public class Manager : MonoBehaviour {

	enum Modes{
		Conditions,Build,Process,Verify,Sold
	}

	Modes state = Modes.Conditions;

	CameraController camCon;




	public Stats Statistic;
	public HouseController House;
	public OverlayController Overlay;




	UIController ui;
	public UIController UI{
		get{
			if(ui==null)
			{
				GameObject o = GameObject.Find("HouseCraftUI");
				if(o==null)
				{
					throw new UnityException("Cannot find ui object!");
				}
				ui = o.GetComponent<UIController>();
				if(ui==null)
					throw new UnityException("Cannot find ui component!");
			}

			return ui;
		}
	}



	void Awake()
	{

		GameObject ui = GameObject.Find("HouseCraftUI");
		if(ui==null)
		{
			Application.LoadLevelAdditive("UIScene");
		}


		camCon = Camera.main.GetComponent<CameraController>();


	}
	// Use this for initialization
	void Start () {
		camCon.ShowPoint(House.MapBounds.center);
		camCon.bounds = House.MapBounds;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Scroll(Vector2 delta)
	{
		camCon.Scroll(delta);
	}

	public void OnSale()
	{
		if(state== Modes.Build)
		{
			state = Modes.Process;

			House.SetHouseMode(HouseModes.Sale,null);

		}

	}

	public void OnCancelSale()
	{
		if(state==Modes.Verify)
		{
			state = Modes.Build;
			UI.OnShowBuild();
			Overlay.RemoveOverlay();
			House.SetHouseMode(HouseModes.Idle,null);
		}
	}

	public void OnProcessed(Segmentator s, Evaluator e)
	{
		state = Modes.Verify;
		UI.OnShowVerify();
	}

	public void OnVerified()
	{
		state = Modes.Sold;
		UI.OnShowResults();
	}

	public void OnBuild()
	{
		state = Modes.Build;
		UI.OnShowBuild();
	}

	public void OnCompleteLevel()
	{
		PlayerProfile profile = GetComponent<PlayerProfile>();

		int old = profile.GetProfitForLevel(Application.loadedLevelName);
		if(old<Statistic.Profit)
			profile.SetProfitForLevel(Application.loadedLevelName,Statistic.Profit);

		Application.LoadLevel("main_menu");
	}
}
