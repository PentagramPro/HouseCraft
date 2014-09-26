using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(EventTrigger))]
public class ClickBlocker : BaseController {

	protected override void Awake ()
	{
		base.Awake ();
		EventTrigger ev = GetComponent<EventTrigger>();

		EventTrigger.Entry e = new EventTrigger.Entry();
		e.eventID = EventTriggerType.PointerEnter;
		e.callback.AddListener( OnPointerEnter);
		ev.delegates.Add(e);

		e = new EventTrigger.Entry();
		e.eventID = EventTriggerType.PointerExit;
		e.callback.AddListener( OnPointerLeave);
		ev.delegates.Add(e);
	}
	bool blocked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter(object obj)
	{
		Block();
		//Debug.Log("Pointer enter");
	}

	public void OnPointerLeave(object obj)
	{
		Unblock();
		//Debug.Log("Pointer leave");
	}

	void OnDisable()
	{
		Unblock();
	}

	void Block()
	{
		if(blocked==false)
		{
			M.BlockMouseInput = true;
			blocked = true;
		}
	}

	void Unblock()
	{
		if(blocked==true)
		{
			M.BlockMouseInput = false;
			blocked = false;
		}
	}
}
