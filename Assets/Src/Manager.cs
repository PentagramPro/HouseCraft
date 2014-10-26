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

	public PlayerProfile Profile{get;internal set;}
	List<System.Action> ActionsOnUpdate = new List<System.Action>();

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
		//Input.simulateMouseWithTouches = true;
		Profile = GetComponent<PlayerProfile>();
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
		if(ActionsOnUpdate.Count>0)
		{
			foreach(System.Action action in ActionsOnUpdate)
			{
				action();
			}
			ActionsOnUpdate.Clear();
		}
	}

	public void InvokeOnUpdate(System.Action action)
	{
		ActionsOnUpdate.Add(action);
	}

	public void Scroll(Vector2 delta)
	{
		camCon.Scroll(delta);
	}

	public void OnSale()
	{
		if(state== Modes.Build)
		{
			UI.OnShowWait(true);
			InvokeOnUpdate(() => {
				state = Modes.Process;
				House.SetHouseMode(HouseModes.Sale,null);
			});


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
		UI.OnShowWait(false);
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

		int old = Profile.GetProfitForLevel(Application.loadedLevelName);
		if(old<Statistic.Profit)
			Profile.SetProfitForLevel(Application.loadedLevelName,Statistic.Profit);

		Application.LoadLevel("main_menu");
	}
}
