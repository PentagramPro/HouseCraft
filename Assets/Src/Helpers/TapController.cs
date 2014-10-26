using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapController : BaseController {

	public delegate void TapEvent();
	public event TapEvent OnTap;

	protected float panDistance = 10;
	protected float curPanDistance = 0;

	Vector3 lastMousePos;
	bool mouseDown = false;

	public void OnMouseDown()
	{
		if(Input.mousePosition.x>Screen.width-316)
			return;
		if(EventSystem.current.IsPointerOverGameObject())
			return;

		foreach(Touch t in Input.touches)
		{

			if(EventSystem.current.IsPointerOverGameObject(t.fingerId))
				return;
		}
        	
		curPanDistance = 0;
		lastMousePos = Input.mousePosition;
		mouseDown = true;

	}
	
	public void OnMouseDrag()
	{
		if(!mouseDown)
			return;

		//if(EventSystem.current.IsPointerOverGameObject())
		//	return;

		Vector2 touchDeltaPosition = new Vector2(lastMousePos.x-Input.mousePosition.x,
		                                         lastMousePos.y-Input.mousePosition.y);
	
		curPanDistance += touchDeltaPosition.sqrMagnitude;
		if (curPanDistance > panDistance)
		{
			M.Scroll(touchDeltaPosition);
		}
		lastMousePos = Input.mousePosition;
	}
	
	public void OnMouseUp()
	{
		//if(EventSystem.current.IsPointerOverGameObject())
		//	return;

		if(!mouseDown)
			return;

		if (curPanDistance < panDistance && OnTap!=null)
			OnTap();

		mouseDown = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
