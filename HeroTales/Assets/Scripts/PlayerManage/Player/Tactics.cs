using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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


public class Slot
{
	public int Index = -1;
	public bool IsEmpty = true;
	private GameObject Character = null;

	public void Add ( GameObject @char )
	{
		Character = @char;
		IsEmpty = false;
	}

	public void Remove ( )
	{
		if( null != Character )
		{
			Character = null;
		}

		IsEmpty = true;
	}

	public GameObject Get ( )
	{
		return IsEmpty ? null : Character;
	}
}

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

	public bool TryAddToSlot ( int index , GameObject @char , out GameObject @charInside )
	{
		@charInside = null;
		if( index >= this.Count || index < 0 )
			return false;

		if( this.CurrentFilled >= this.NumAllow )
			return false;

		if( !this[index].IsEmpty )
			@charInside = this[index].Get();

		this[index].Add(@char);

		return true;
	}

	public void TryRemoveFromSlot ( int index )
	{
		if( index >= this.Count || index < 0 )
			return;

		this[index].Remove();
	}

	public void Clone ( Slots slots ) 
	{
		Slot slot = null;
		GameObject charInsd;
		for( int i = 0 , count = this.Count ; i < count ; ++i )
		{
			slot = this[i];
			if( !slot.IsEmpty )
			{
				slots.TryAddToSlot( i , GameObject.Instantiate( slot.Get() ) as GameObject , out charInsd );
			}
		}
	}
}