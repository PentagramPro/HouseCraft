using UnityEngine;
using System.Collections.Generic;

public class PhantomController : BaseController {

	Dictionary<int,Transform> phantoms = new Dictionary<int,Transform>();
	MapRect lastRect = new MapRect(-1,-1,-1,-1);

	public Transform Red,Green;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//returns true if this phantom already has been placed
	public bool Place(MapRect rect)
	{

		if(lastRect == rect)
			return true;

		Remove();

		rect.Foreach((MapPoint p) => {
			Create(p,true);
		});

	
		lastRect = new MapRect(rect);
		return false;
	}

	public void SetRed(MapPoint p)
	{
		Transform ph= null;
		if(phantoms.TryGetValue(p.toInt(),out ph))
		{
			Destroy(ph.gameObject);
			phantoms.Remove(p.toInt());
		}

		Create(p,false);
	}

	void Create(MapPoint p, bool isGreen)
	{
		Transform ph = Instantiate<Transform>(isGreen?Green:Red);
		ph.parent = transform;
		ph.localPosition = new Vector3(p.X+0.5f,p.Y+0.5f,0);
		phantoms.Add(p.toInt(),ph);
	}
	public void Remove()
	{
		foreach(Transform s in phantoms.Values)
		{
			Destroy(s.gameObject);
		}
		phantoms.Clear();
	}

}
