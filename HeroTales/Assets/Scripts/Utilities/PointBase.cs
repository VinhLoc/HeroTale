using UnityEngine;
using System.Collections;

[System.Serializable]
public class PointBase {

	public int _current;
	public int _max;

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
			}else if( value > _max )
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
		}
	}


}
