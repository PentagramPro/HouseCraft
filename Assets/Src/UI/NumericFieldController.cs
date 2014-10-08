using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumericFieldController : BaseController {

	public Color NegativeColor = new Color(1,0.3f,0.3f);
	public Color PositiveColor = new Color(0.3f,1,0.3f);

	float Speed = 1000;
	float TimeToEquality = 1;
	Text text;
	int fieldValue = 0;
	int displayedValue = 0;

	public bool runMode = true;
	public bool useColors = false;
	protected override void Awake ()
	{
		base.Awake ();
		text = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		if(displayedValue!=fieldValue)
		{
			if(runMode)
			{
				int  iDelta = fieldValue - displayedValue;

				iDelta=(int)Mathf.Sign(iDelta)*Mathf.Min( (int)(Speed*Time.smoothDeltaTime),Mathf.Abs(iDelta));

				displayedValue+=iDelta;
			}
			else
			{
				displayedValue = fieldValue;
			}
			UpdateText();
		}
	}

	public int Value{
		get{
			return fieldValue;
		}
		set{
			fieldValue = value;
			UpdateSpeed();
		}
	}
	public void AddValue(int delta)
	{
		fieldValue+=delta;
		UpdateSpeed();
	}

	protected void UpdateSpeed()
	{
		float delta = Mathf.Abs(fieldValue-displayedValue);
		Speed = delta/TimeToEquality;
	}

	protected void UpdateText()
	{
		text.text = displayedValue.ToString("'$'#,##0");
		if(useColors)
		{
			if(displayedValue>=0)
				text.color = PositiveColor;
			else
				text.color = NegativeColor;
		}
	}
}
