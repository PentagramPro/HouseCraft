using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OverlayController : BaseController {

	public Transform OverlayCellPrefab;
	public Canvas OverlayCanvas;
	public Text OverlayTextPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RemoveOverlay()
	{
		var children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		children.Clear();
		foreach (Transform child in OverlayCanvas.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}

	public void DrawRoom(Room room)
	{
		foreach(CellController cell in room.Cells)
		{
			Transform c = Instantiate<Transform>(OverlayCellPrefab);
			c.position = cell.transform.position;
			c.parent = transform;
			c.GetComponent<SpriteRenderer>().material.SetColor("_TintColor",room.RoomColor);


		}

		Text txt = Instantiate<Text>(OverlayTextPrefab);
		txt.rectTransform.parent = OverlayCanvas.transform;
		txt.rectTransform.localScale = new Vector3(1,1,1);
		txt.text = room.Name;
		txt.rectTransform.position = new Vector3(room.LabelPos.X, room.LabelPos.Y,0);
	}
}
