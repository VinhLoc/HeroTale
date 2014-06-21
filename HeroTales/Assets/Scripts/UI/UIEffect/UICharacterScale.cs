using UnityEngine;
using System.Collections;

public class UICharacterScale : MonoBehaviour {

	public bool IgnoreTimeScale = false;
	float _lastDeltaTime = 0;

	bool IsReverse = false;

	Transform transformCached;

	void Awake ( )
	{
		transformCached = transform;
	}

	void Start ( )
	{
		_lastDeltaTime = Time.realtimeSinceStartup;
	}

	void Update ( )
	{

		if( IsReverse )
		{
			UpdateScaleDown ( Time.deltaTime );
		}
		else
		{
			UpdateScaleUp ( Time.deltaTime );
		}
	}

	void UpdateScaleUp ( float delta )
	{
		Vector3 scale = transformCached.localScale;

		scale.y += 0.1f * Time.deltaTime;

		if( scale.y >= 1.05f )
		{
			IsReverse = true;
			scale.y = 1.05f;
		}

		transformCached.localScale = scale;
	}

	void UpdateScaleDown( float delta )
	{
		Vector3 scale = transformCached.localScale;
		
		scale.y -= 0.1f * Time.deltaTime;
		
		if( scale.y <= 1.0f )
		{
			IsReverse = false;
			scale.y = 1.0f;
		}
		
		transformCached.localScale = scale;
	}

	float getDelta ( )
	{
		float delta = 0;

		if( IgnoreTimeScale )
		{
			delta = Time.realtimeSinceStartup - _lastDeltaTime;
			_lastDeltaTime = Time.realtimeSinceStartup;
		}
		else
		{
			delta = Time.deltaTime;
		}

		return delta;
	}
}
