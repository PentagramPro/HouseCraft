using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrokenRule : MonoBehaviour {

	public Text Header;
	public Text Descr;
	public NumericFieldController Cost;

	public string Name{
		set{
			Header.text = value;
		}
		get{
			return Header.text;
		}
	}

	public string Description{
		set{
			Descr.text = value;
		}
		get{
			return Descr.text;
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
