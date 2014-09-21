using UnityEngine;
using System.Collections.Generic;

public class PhantomController : BaseController {

	List<Transform> phantoms = new List<Transform>();
	int lastMinX=-1, lastMinY=-1, lastMaxX=-1, lastMaxY=-1;

	public Transform Red,Green;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//returns true if this phantom already has been placed
	public bool Place(int minX, int minY, int maxX, int maxY)
	{
		Remove();
		if(minX==lastMinX && maxX == lastMaxX && minY== lastMinY && maxY==lastMaxY)
			return true;

		for(int x=minX;x<=maxX;x++)
		{
			for(int y=minY;y<=maxY;y++)
			{
				Transform p = Instantiate<Transform>(Green);
				p.parent = transform;
				p.localPosition = new Vector3(x+0.5f,y+0.5f,0);

			}
		}
	
		lastMinX = minX;
		lastMaxX = maxX;
		lastMinY = minY;
		lastMaxY = maxY;
		return false;
	}

	public void Remove()
	{
		foreach(Transform s in phantoms)
		{
			Destroy(s.gameObject);
		}
	}

}
