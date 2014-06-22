using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EffectType
{
	Attack_Sword
}

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

	public List<EffectBase> ListEfx = new List<EffectBase>( );

	public void CreateEffect ( EffectType type , Vector3 Position , EffectDelegate Delegate , bool isLeft )
	{
		Debug.Log(isLeft);
		EffectBase efxBase = ListEfx[(int)type];

		efxBase.gameObject.SetActive(true);
		efxBase.transform.position = Position;
		
		Vector3 scale = efxBase.transform.localScale;

		if( !isLeft )
		{
			scale = new Vector3( -Mathf.Abs(scale.x) , scale.y , scale.z );
		}
		else
		{
			scale = new Vector3( Mathf.Abs(scale.x) , scale.y , scale.z );
		}

		efxBase.transform.localScale = scale;
		efxBase.Delegate = Delegate;
		efxBase.Play ( );
	}
}
