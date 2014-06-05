using UnityEngine;
using System.Collections;

public class BalanceInfo {

	private int _gold;
	private int _ruby;
	private int _diamond;

	public int Gold
	{
		get{
			return _gold;
		}
		set{
			if( value < 0 )
				_gold = 0;
			else
				_gold = value;
		}
	}

	public int Ruby
	{
		get{
			return _ruby;
		}
		set{
			if( value < 0 )
				_ruby = 0;
			else
				_ruby = value;
		}
	}

	public int Diamond
	{
		get{
			return _diamond;
		}
		set{
			if( value < 0 )
				_diamond = 0;
			else
				_diamond = value;
		}
	}

	public void initialize ( int gold , int diamond , int ruby )
	{
		this.Gold = gold;
		this.Diamond = diamond;
		this.Ruby = ruby;
	}
}
