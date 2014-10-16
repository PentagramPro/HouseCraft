using UnityEngine;
using System.Collections.Generic;

public class StringLoaderController : MonoBehaviour {

	public List<TextAsset> stringsFiles;
	public TextAsset defaultStringFile;

	public Strings S{get;internal set;}
	void Awake()
	{
		TextAsset curStringFile = null;
		foreach(TextAsset sf in stringsFiles)
		{
			if(System.IO.Path.GetFileNameWithoutExtension(sf.name)
			   == System.Enum.GetName(typeof(SystemLanguage),Application.systemLanguage) )
				curStringFile = sf;
		}
		if(curStringFile==null)
			curStringFile = defaultStringFile;
		S = Strings.Load(curStringFile.text);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
