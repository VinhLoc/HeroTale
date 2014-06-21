using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tactics {
	
	public Slots SlotsDeployment = new Slots();

	private int _numDeploy = 0;

	public int NumDeploy
	{
		get
		{
			return _numDeploy;
		}
		set{
			_numDeploy = value;

			SlotsDeployment.NumAllow = _numDeploy;
		}
	}

	public void Initialize ( int charSlot , int deploySlot , int numDeploy )
	{
		this.NumDeploy = numDeploy;

		SlotsDeployment.Initialize( deploySlot , this.NumDeploy );
	}
}

[System.Serializable]
public class Slot
{
	private Character _character = null;
	public int Index = -1;
	public bool IsEmpty
	{
		get{
			if( _character == null )
				return true;
			if( _character.PPointStats.HasDie )
				return true;

			return false;
		}
	}

	public Character Character
	{
		set{
			if( value == null )
			{
				return;
			}
			else
			{
				_character = value;
			}
		}
		get{
			return _character;
		}
	}

	public void Remove ( )
	{
		if( null != Character )
		{
			Character = null;
		}
	}
}

[System.Serializable]
public class Slots : List<Slot>
{
	public int NumAllow;
	public int NumSlot;
	private int CurrentFilled
	{
		get
		{
			int result = 0;
			for( int i = 0 , count = this.Count ; i < count ; ++i )
			{
				result += this[i].IsEmpty ? 0 : 1;
			}

			return result;
		}
	}

	public void Initialize ( int numSlot , int numAllow )
	{
		this.Clear( );
		numAllow = Mathf.Min( numSlot , numAllow );

		this.NumSlot = numSlot;
		this.NumAllow = numAllow;

		for( int i = 0 ; i < this.NumSlot ; ++i )
		{
			Slot slot = new Slot();
			slot.Index = i;
			this.Add(slot);
		}
	}

	/// <summary>
	/// Tries to add character to the specific slot.
	/// </summary>
	/// <returns>The Character which is had been inside this slot</returns>
	/// <param name="index">Index.</param>
	/// <param name="char">Char.</param>
	public Character TryAddToSlot ( int index , Character @char )
	{
		if( index >= this.Count || index < 0 )
			return null;

		if( this.CurrentFilled >= this.NumAllow )
			return null;

		Character @charInside = null;

		if( !this[index].IsEmpty )
			@charInside = this[index].Character;

		this[index].Character = @char;

		return @charInside;
	}

	public void TryRemoveFromSlot ( int index )
	{
		if( index >= this.Count || index < 0 )
			return;

		this[index].Remove();
	}

	public void Reset () 
	{
		Slot slot;
		for( int i = 0 , count = this.Count ; i < count ; ++i )
		{
			slot = this[i];
			if( null != slot.Character )
			{
				slot.Character.Revive ( );
			}
		}
	}
}