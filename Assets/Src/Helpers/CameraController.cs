using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UnityEngine.EventSystems.Physics2DRaycaster))]
public class CameraController : BaseController {
	enum Modes{
		Idle,Moving,Blocked
	}
	public delegate void ScrollDelegate(Vector3 delta);
	public event ScrollDelegate OnScrolled;

	public Vector3 targetPosition = new Vector3();
	public Rect bounds = new Rect();

	Vector3 newTargetPos;
	float moveSpeed = 16;

	float ScrollFactor = 0.3f;
	Modes state = Modes.Idle;
//	float dragSpeed=5;
	// Use this for initialization
	void Start () {
	
		ScrollFactor = Mathf.Abs(
			(Camera.main.ScreenToWorldPoint(new Vector3(0,0,transform.position.y))
		                -Camera.main.ScreenToWorldPoint(new Vector3(1,0,transform.position.y))).x
			);

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(targetPosition+transform.position,0.5f);
	}

	public void ShowPoint(Vector3 mapPoint)
	{
		state = Modes.Moving;
		newTargetPos = mapPoint-targetPosition;
		newTargetPos.z = transform.position.z;
	}


	public void Scroll(Vector2 delta)
	{
		if(state==Modes.Idle)
		{
			Vector3 vec = new Vector3(delta.x,delta.y,0);


			vec*=ScrollFactor;

			if(bounds.Contains(new Vector2(transform.position.x+vec.x,transform.position.y+vec.y)))
				transform.position+=vec;

			if(OnScrolled!=null)
				OnScrolled(vec);
		}
	}
	// Update is called once per frame
	void Update () {
		if(state==Modes.Moving)
		{
			transform.position = Vector3.MoveTowards(transform.position,newTargetPos,
			                                         moveSpeed*Time.smoothDeltaTime);
			if(transform.position==newTargetPos)
				state = Modes.Idle;
		}

	}
}
