using UnityEngine;
using System.Collections;

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

	public void PlayNewTurn ( )
	{
		CurrentTurn = 0;
		LStep = 0;
		RStep = 0;
	}

	public void CloneLSlot ( Slots slot )
	{
		LGroup.Initialize( slot.NumSlot , slot.NumAllow );
		LGroup.Clone( slot );
	}

	public void CloneRSlot ( Slots slot )
	{
		RGroup.Initialize( slot.NumSlot , slot.NumAllow );
		RGroup.Clone( slot );
	}
}
