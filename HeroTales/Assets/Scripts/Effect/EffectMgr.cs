using UnityEngine;
using System.Collections;

public class EffectMgr : MonoBehaviour {

	private static EffectMgr _instance = null;

	public static EffectMgr Instance
	{
		get{
			if( null == _instance )
			{
				GameObject go = new GameObject("EffectMgr");
				_instance = go.AddComponent<EffectMgr>();
			}

			return _instance;
		}
	}

	void Awake ( )
	{
		_instance = this;
	}


}
