using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrokenRule : MonoBehaviour {

	public Text Header;
	public NumericFieldController Cost;

	public string Name{
		set{
			Header.text = value;
		}
	}

	public int Value{
		set{
			Cost.Value = value;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
