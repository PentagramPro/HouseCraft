using UnityEngine;
using System.Collections;

public class BaseController : MonoBehaviour {

	private Manager manager;
	protected Manager M
	{
		get{
			return manager;
		}
	}


	public void PrepareManager()
	{
		if (manager == null) 
		{
			GameObject m = GameObject.Find("SceneObject");
			if(m==null)
				throw new UnityException("Cannot find manager object!");
			manager = m.GetComponent<Manager>();
			if(manager==null)
				throw new UnityException("Cannot find manager component!");
		}
	}

	protected virtual void Awake()
	{
		PrepareManager();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected T Instantiate<T>(Component prefab) where T: Component
	{
		GameObject o = (GameObject)GameObject.Instantiate(prefab.gameObject);
		return o.GetComponent<T>();
	}
}
