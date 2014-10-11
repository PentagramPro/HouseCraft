using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FamilyIndicatorController : BaseController {

	public RectTransform ChildPrefab;

	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();
		int children = M.House.LevelConditions.FamilySize-2;

		if(children<=0)
			return;


		for(int i=0;i<children;i++)
		{
			RectTransform child = Instantiate<RectTransform>(ChildPrefab);
			child.parent = transform;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
