using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TapController : BaseController, IPointerClickHandler, IDragHandler  {

	public delegate void TapEvent();
	public event TapEvent OnTap;


	public void OnPointerClick(PointerEventData eventData)
	{
		if(OnTap!=null)
			OnTap();
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		M.Scroll(-eventData.delta);
	}
}
