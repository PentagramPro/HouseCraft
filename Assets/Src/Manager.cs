using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	CameraController camCon;
	public bool BlockMouseInput = false;

	void Awake()
	{
		camCon = Camera.main.GetComponent<CameraController>();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Scroll(Vector2 delta)
	{
		camCon.Scroll(delta);
	}
}
