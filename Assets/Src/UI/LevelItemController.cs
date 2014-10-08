using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelItemController : MonoBehaviour {

	Image image;
	Color baseColor;
	public Color selectedColor = new Color(0.6f,0.1f,0.1f);


	public string SceneName;
	public LevelSelectorController LevelSelector{get;internal set;}

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
		image = GetComponent<Image>();
		baseColor = image.color;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnClick()
	{
		LevelSelector.OnLevelSelected(this);
		image.color = selectedColor;
	}

	public void Deselect()
	{
		image.color = baseColor;
	}
}
