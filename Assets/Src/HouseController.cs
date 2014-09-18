using UnityEngine;
using System.Collections.Generic;

public class HouseController : BaseController {

	public CellController CellPrefab;

	Dictionary<int,CellController> cells = new Dictionary<int, CellController>();

	Vector3 markerPos;
	public Vector3 MarkerPosition{
		set{
			markerPos = value;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();

		CellController[] c = GetComponentsInChildren<CellController>();
		
		foreach(CellController cell in c)
		{
			cells.Add(cell.Position.toInt(),cell);
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmosSelected()
	{

		
		Gizmos.color = Color.red;    
		Gizmos.DrawWireCube(markerPos, new Vector3(1,1, 1) * 1.1f);
	}

	public void EditorCheckCells()
	{
		List<int> toRemove = new List<int>();
		foreach(int k in cells.Keys)
		{
			if(cells[k]==null)
				toRemove.Add(k);
		}

		Debug.LogWarning("To remove: "+ toRemove.Count);
		foreach(int k in toRemove)
			cells.Remove(k);
	}

	public CellController GetCell(MapPoint point)
	{
		int key = point.toInt();
		if(cells.ContainsKey(key))
			return cells[key];

		return null;
	}

	public void SetCell(MapPoint point)
	{
		int key = point.toInt();
		if(!cells.ContainsKey(key))
		{
			CellController newCell = Instantiate<CellController>(CellPrefab);
			newCell.transform.parent = transform;
			newCell.Position = point;
			cells.Add(key,newCell);
		}
	}
}
