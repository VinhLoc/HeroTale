using UnityEngine;
using System.Collections;

public interface EffectDelegate{
	void OnEndCallback ( );
}

public class EffectBase : MonoBehaviour {

	public EffectDelegate Delegate;

	public virtual void Play ( )
	{

	}

	public virtual void End ( )
	{
		if( null != Delegate )
		{
			Delegate.OnEndCallback( );
		}
	}
}
