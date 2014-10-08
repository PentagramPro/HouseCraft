using UnityEngine;
using System.Collections;

public class PlayerProfile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int TotalProfit{
		get{
			return PlayerPrefs.GetInt("TotalProfit",0);
		}
		set{
			PlayerPrefs.SetInt("TotalProfit",value);
			PlayerPrefs.Save();
		}
	}

	public int GetProfitForLevel(string sceneName)
	{
		return PlayerPrefs.GetInt("Level:"+sceneName,0);
	}

	public void SetProfitForLevel(string sceneName, int value)
	{
		PlayerPrefs.SetInt("Level:"+sceneName,value);
		PlayerPrefs.Save();
	}

}
