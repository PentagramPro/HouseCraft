using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	enum Modes{
		Build,Process,Verify,Sold
	}

	Modes state = Modes.Build;

	CameraController camCon;

	int mouseBlockCount = 0;

	public HouseController House;
	public OverlayController Overlay;

	public bool BlockMouseInput 
	{
		get
		{
			return mouseBlockCount>0;
		}
		set
		{
			if(value)
				mouseBlockCount++;
			else
				mouseBlockCount--;
			if(mouseBlockCount<0)
				mouseBlockCount=0;
		}
	}

	void Awake()
	{
		camCon = Camera.main.GetComponent<CameraController>();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Scroll(Vector2 delta)
	{
		camCon.Scroll(delta);
	}

	public void Sale()
	{
		if(state== Modes.Build)
		{
			state = Modes.Process;
			House.SetHouseMode(HouseModes.Sale,null);

		}

	}
}
