using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleTurnController : MonoBehaviour {

	private static BattleTurnController _instance = null;

	public static BattleTurnController Instance
	{
		get{
			if( null == _instance )
			{
				_instance = new BattleTurnController( );
			}

			return _instance;
		}
	}

	void Awake ( )
	{
		_instance = this;
	}

	public Slots LGroup = new Slots();
	public Slots RGroup = new Slots();

	private int CurrentTurn;
	private int LStep;
	private int RStep;

	public List<Color> color = new List<Color>()
	{
		Color.white,
		Color.red,
		Color.green,
		Color.blue,
		Color.yellow,
		Color.gray,
		Color.magenta
	};

	public void PlayNewTurn ( )
	{
		CurrentTurn = 0;
		LStep = 0;
		RStep = 0;
	}


	public List<Slot> ListTurn = new List<Slot>( );

	private Slots Slot1stTurn;
	private Slots Slot2ndTurn;

	public void CalculateTurn ( )
	{
		bool bLFirst = Random.Range(0,2) == 0 ? true : false;

		Slot1stTurn = bLFirst ? LGroup : RGroup;
		Slot2ndTurn = bLFirst ? RGroup : LGroup;

		int current1stIdx = 0;
		int current2ndIdx = 0;

		for( int i = 0 , count = Slot1stTurn.Count + Slot2ndTurn.Count ; i < count ; ++i )
		{
			ListTurn.Add( i%2 == 0 ? Slot1stTurn[current1stIdx++] : Slot2ndTurn[current2ndIdx++] );
		}
	}

	public GameObject getTargetForCurrentSlotIndex ( int turnIndex , int index )
	{
		Slots defenderSlot = turnIndex%2 == 0 ? Slot2ndTurn : Slot1stTurn;

		GameObject defender;
		Slot slot;

		for ( int i = index ; i < 9 ; i+=3 )
		{
			slot = defenderSlot[i];
			if( !slot.IsEmpty && slot.Get() != null )
			{
				return slot.Get();
			}
		}

		return null;
	}

	public void CloneLSlot ( Slots sslot )
	{
		LGroup.Initialize( sslot.NumSlot , sslot.NumAllow );
		LGroup.Clone( sslot );
	}

	public void CloneRSlot ( Slots slot )
	{
		RGroup.Initialize( slot.NumSlot , slot.NumAllow );
		RGroup.Clone( slot );
	}
}
