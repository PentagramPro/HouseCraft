using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class WallGfxController : BaseController {

	public Sprite HorizontalSprite,TSprite,rSprite, DotSprite, HalfSprite, xSprite;

	public bool Top,Bottom,Left,Right;

	SpriteRenderer sRenderer;

	protected override void Awake ()
	{


	}
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateWall()
	{
		if(sRenderer==null)
			sRenderer = GetComponent<SpriteRenderer>();

		int count = (Top?1:0)+(Bottom?1:0)+(Left?1:0)+(Right?1:0);
		if(count==0)
		{
			sRenderer.sprite = DotSprite;
			transform.localRotation = Quaternion.identity;
		}
		else if(count==4)
		{

			sRenderer.sprite = xSprite;
			transform.localRotation = Quaternion.identity;
		}
		else
		{
			if(count==1)
			{
				sRenderer.sprite = HalfSprite;

				if(Right)
					transform.localRotation = Quaternion.identity;
				else if(Left)
					transform.localRotation = Quaternion.Euler(0,0,180);
				else if(Top)
					transform.localRotation = Quaternion.Euler(0,0,90);
				else if(Bottom)
					transform.localRotation = Quaternion.Euler(0,0,-90);
			}
			else if(count==3)
			{
				sRenderer.sprite = TSprite;

				if(!Right)
					transform.localRotation = Quaternion.identity;
				else if(!Left)
					transform.localRotation = Quaternion.Euler(0,0,180);
				else if(!Top)
					transform.localRotation = Quaternion.Euler(0,0,90);
				else if(!Bottom)
					transform.localRotation = Quaternion.Euler(0,0,-90);
			}
			else if(count==2)
			{

				if(Right && Left)
				{
					transform.localRotation = Quaternion.identity;
					sRenderer.sprite = HorizontalSprite;
				}
				else if(Top && Bottom)
				{
					transform.localRotation = Quaternion.Euler(0,0,90);
					sRenderer.sprite = HorizontalSprite;
				}
				else
				{
					sRenderer.sprite = rSprite;
					if(Right && Bottom)
						transform.localRotation = Quaternion.identity;
					else if(Top && Right)
						transform.localRotation = Quaternion.Euler(0,0,90);
					else if(Top && Left)
						transform.localRotation = Quaternion.Euler(0,0,180);
					else if(Left && Bottom)
						transform.localRotation = Quaternion.Euler(0,0,-90);
				}
			}
		}
	}
}
