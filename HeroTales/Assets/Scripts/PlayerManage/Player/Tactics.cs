using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tactics {


	public Slots SlotsCharacter = new Slots();

	public PointBase OpenSlot;

	public void Initialize ( int maxSlot , int currentSlot )
	{
		this.OpenSlot.Max = maxSlot;
		this.OpenSlot.Current = currentSlot;

		SlotsCharacter.NumOpen = OpenSlot.Current;
		SlotsCharacter.MaxOpen = OpenSlot.Max;

		SlotsCharacter.Initialize( );
	}
}


public class Slot
{
	public int Index = -1;
	public bool IsOpened = false;
	public GameObject Character = null;

	public void Add ( GameObject @char )
	{
		if( !IsOpened )
			return;

		Character = @char;
	}

	public void Remove ( )
	{
		if( null != Character )
		{
			Character = null;
		}
	}
}

public class Slots : List<Slot>
{
	public int NumOpen;
	public int MaxOpen;

	public void Initialize ( )
	{
		NumOpen = Mathf.Min ( NumOpen , MaxOpen );

		for( int i = 0 ; i < MaxOpen ; ++i )
		{
			Slot slot = new Slot();
			slot.Index = i;
			slot.IsOpened = i < NumOpen;
			this.Add(slot);
		}
	}

	public bool TryAddToSlot ( int index , GameObject @char , out GameObject @charInside )
	{
		@charInside = null;
		if( index >= this.NumOpen )
			return false;

		if( !this[index].IsOpened )
			return false;

		@charInside = this[index].Character;

		this[index].Add(@char);

		return true;
	}

	public bool TryRemoveFromSlot ( int index )
	{
		if( index >= this.NumOpen )
			return false;
		if( !this[index].IsOpened )
			return false;

		this[index].Remove();

		return true;
	}
}