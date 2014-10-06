using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsPanel : BaseController {


	float t=0;
	public Animator animator;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		t+=Time.smoothDeltaTime;
		if(t>4)
		{
			t=0;
			animator.SetTrigger("Reset");
		}
	}
}
