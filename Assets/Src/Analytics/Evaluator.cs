using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Segmentator))]
public class Evaluator : BaseController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Launch()
	{
		M.OnProcessed();
	}
}
