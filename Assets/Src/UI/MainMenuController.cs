using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public LevelSelectorController LevelSelector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartGame()
	{
		LevelSelector.gameObject.SetActive(true);
	}

	public void OnInstructions()
	{

	}

	public void OnExitGame()
	{
		Application.Quit();
	}
}
