using UnityEngine;
using System.Collections;

[System.Serializable]
public class PointBase {

	[SerializeField]
	private int _current;
	[SerializeField]
	private int _max;

	public PointBase()
	{

	}

	public int Current
	{
		get{
			return _current;
		}
		set
		{
			if( value < 0 )
			{
				_current = 0;
			}
			else if( _max != 0 && _current > _max )
			{
				_current = _max;
			}
			else
				_current = value;
		}
	}

	public int Max
	{
		get{
			return _max;
		}
		set{
			_max = value;
			if( _current > _max )
			{
				_current = _max;
			}
		}
	}

	public void Reset ( )
	{
		_current = _max;
	}

	public void ResetToZero ( )
	{
		_current = 0;
	}
}
