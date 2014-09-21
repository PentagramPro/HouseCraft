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

	protected static T Instantiate<T>(Component prefab) where T: Component
	{
		return Instantiate<T>(prefab,null);
	}

	protected static T Instantiate<T>(Component prefab, Transform parent) where T: Component
	{
		GameObject o = (GameObject)GameObject.Instantiate(prefab.gameObject);
		if(parent!=null)
			o.transform.parent = parent;
		return o.GetComponent<T>();
	}

	protected T GetComponentInterface<T>()
	{
		Component[] comps = GetComponents<Component>();
		foreach(Component c in comps)
		{
			if(c is T)
				return (T)(object)c;
		}
		return default(T);
	}
}
