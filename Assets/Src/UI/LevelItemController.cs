using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelItemController : MonoBehaviour {

	Image image;
	Color baseColor;
	public Color selectedColor = new Color(0.6f,0.1f,0.1f);
	public NumericFieldController ProfitRecord;

	public string SceneName;
	public LevelSelectorController LevelSelector{get;internal set;}
	public Text LevelNameLabel;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
		image = GetComponent<Image>();
		baseColor = image.color;

		PlayerProfile profile = GetComponentInParent<PlayerProfile>();
		int profit = profile.GetProfitForLevel(SceneName);
		if(profit==0)
			ProfitRecord.gameObject.SetActive(false);
		else
		{
			ProfitRecord.gameObject.SetActive(true);
			ProfitRecord.Value = profit;
		}
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
		if(image==null)
			return;
		image.color = baseColor;
	}
}
