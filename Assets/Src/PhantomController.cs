using UnityEngine;
using System.Collections.Generic;

public class PhantomController : BaseController {

	Dictionary<int,Transform> phantoms = new Dictionary<int,Transform>();
	Dictionary<int,Transform> indicators = new Dictionary<int,Transform>();
	MapRect lastRect = new MapRect(-1,-1,-1,-1);

	public MapPoint LastPhantomPosition;

	public Transform Red,Green;
	public Transform PlaceIndicator;
	public Transform DoorIndicator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//returns true if this phantom already has been placed
	public bool PlacePhantom(MapPoint pos,MapRect rect)
	{
		LastPhantomPosition = pos;
		if(lastRect.Equals(rect))
			return true;

		RemovePhantom();

		rect.Foreach((MapPoint p) => {
			InstantiatePhantom(p,true);
		});

	
		lastRect = new MapRect(rect);
		return false;
	}

	public bool IsDisplayed()
	{
		return phantoms.Count>0;
	}
	public void SetRedPhantom(MapPoint p)
	{
		Transform ph= null;
		if(phantoms.TryGetValue(p.toInt(),out ph))
		{
			Destroy(ph.gameObject);
			phantoms.Remove(p.toInt());
		}

		InstantiatePhantom(p,false);
	}

	void InstantiatePhantom(MapPoint p, bool isGreen)
	{
		Transform ph = Instantiate<Transform>(isGreen?Green:Red);
		ph.parent = transform;
		ph.localPosition = new Vector3(p.X+0.5f,p.Y+0.5f,0);
		phantoms.Add(p.toInt(),ph);
	}
	void InstantiateIndicator(WallPoint p, bool isDoor)
	{
		if(indicators.ContainsKey(p.toInt()))
			return;

		Transform ph = Instantiate<Transform>(isDoor?DoorIndicator:PlaceIndicator);
		ph.parent = transform;
		ph.localPosition = new Vector3(p.X,p.Y,0);
		indicators.Add(p.toInt(),ph);
	}

	public void RemovePhantom()
	{
		foreach(Transform s in phantoms.Values)
		{
			Destroy(s.gameObject);
		}
		phantoms.Clear();
	}


	public void RemoveIndicators()
	{
		foreach(Transform s in indicators.Values)
		{
			Destroy(s.gameObject);
		}
		indicators.Clear();
	}

	public void PlaceIndicators(Dictionary<int, CellController> cells, 
	                            Dictionary<int, WallController> walls, 
	                            WallController wallPrefab)
	{
		bool isDoor =  wallPrefab.PrefabIsDoor();
		foreach(CellController c in cells.Values)
		{
			if(c.IsMultiCell)
			{
				MapRect rect = c.GetCurCellIndexes();
				rect.Foreach((MapPoint p) => {
					WallPoint wp = new WallPoint(p.X,p.Y);
					if(  (isDoor || !walls.ContainsKey(wp.toInt())) && wallPrefab.PrefabValidatePosition(M,wp))
						InstantiateIndicator(wp, isDoor);
				});
			}
			else
			{
				WallPoint wp = new WallPoint(c.Position.X,c.Position.Y);
				if(  (isDoor || !walls.ContainsKey(wp.toInt())) && wallPrefab.PrefabValidatePosition(M,wp))
					InstantiateIndicator(wp, isDoor);
			}
		}
	}
}
