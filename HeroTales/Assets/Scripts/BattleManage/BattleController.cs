using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour {
	
	private static BattleController _instance = null;

	public static BattleController Instance
	{
		get{
			if( null == _instance )
			{
				_instance = new BattleController( );
			}

			return _instance;
		}
	}

	void Awake ( )
	{
		_instance = this;
	}

	public enum DEPLOY_TYPE
	{
		LEFT,
		RIGHT
	}

//	private bool EndGame = false;
	private int TurnCount = 0;

	public void CalculateResult ( Slots lSlot , Slots rSlot )
	{
		CalculateTurn ( lSlot , rSlot );

		int i = 0;
		int count1st = SlotTurn1st.Count;
		int count2nd = SlotTurn2nd.Count;
		int index1st = 0;
		int index2nd = 0;

		bool bFirstTurn = true;
		bool bEndGame = false;
		bool bOutOfMove = false;

		Character attacker;
		Character defender;
		Slot slot;

		TurnCount = 1;

		BattleRecordController.Instance.Reset( );

		while(!bEndGame)
		{
			// record TURN
			BattleRecordController.Instance.CreateRecord();
			BattleRecordController.RecordTurn(TurnCount);

			if(bFirstTurn)
			{
				bFirstTurn = !DoTurn( ref index1st , ref index2nd , count1st , SlotTurn1st , SlotTurn2nd , out bOutOfMove , out bEndGame );

				if( index2nd >= SlotTurn2nd.Count ) // they had reach max
				{
					bFirstTurn = true;
				}
			}
			else
			{
				bFirstTurn = DoTurn( ref index2nd , ref index1st , count2nd , SlotTurn2nd , SlotTurn1st , out bOutOfMove , out bEndGame );

				if( index1st >= SlotTurn1st.Count )
				{
					bFirstTurn = false;
				}
			}

			Debug.Log("Next 1st : " + index1st );
			Debug.Log("Next 2nd : " + index2nd );

			if( index1st >= SlotTurn1st.Count &&
			   index2nd >= SlotTurn2nd.Count && !bEndGame )
			{
				Debug.Log("NEW TURN");
				index1st = 0;
				index2nd = 0;
				bFirstTurn = true;
				TurnCount += 1;
			}

			if(bEndGame)
			{
				Debug.Log("Endgame here");
			}
		}

		Debug.Log("Turn count : " + TurnCount );

		BattleRecords records = BattleRecordController.Instance.PBattleRecords;

		foreach( var record in records )
		{
			Debug.Log("Turn : " + record.Turn );
			Debug.Log("Attacker : " + record.AttackerSlot.Charname );
			Debug.Log("Defender : " + record.DefenderSlot.Charname);
			Debug.Log("Damage dealed : " + record.Damage );
			Debug.Log("Has dodgle : " + record.Dodged );
			Debug.Log("Has blocked : " + record.Blocked );
			Debug.Log("Has crit : " + record.Crit );
			Debug.Log("-------------------------");
			Debug.Log("Attacker HP : " + record.AttackerSlot.CharHealth + "/" + record.AttackerSlot.CharMaxHealth );
			Debug.Log("Defender HP : " + record.DefenderSlot.CharHealth + "/" + record.DefenderSlot.CharMaxHealth );
			Debug.Log("-------------------------");
		}
	}
	
	#region Calculate Turn helper

	private Slots SlotTurn1st;
	private Slots SlotTurn2nd;

	private void CalculateTurn ( Slots lSlot , Slots rSlot )
	{
		SlotTurn1st = null;
		SlotTurn2nd = null;

		bool bFirst = false; //Random.Range(0,2) == 0 ? true : false;

		SlotTurn1st = bFirst ? lSlot : rSlot ;
		SlotTurn2nd = bFirst ? rSlot : lSlot ;
	}

	#endregion

	#region Find Target helper

	private Slot FindNextTarget ( int slotIndex , Slots slotsToFind ) 
	{
		int indexToFind = slotIndex;

		while( indexToFind - 3 >= 0 )
		{
			indexToFind -= 3;
		}

		int i;

		bool bZeroFirst = indexToFind == 0;
		int nInitialIndex = indexToFind;

		do
		{
			// Fisrt find on the same lane is there any targets ?
			for( i = indexToFind ; i < 9 ; i+=3 )
			{
				if( !slotsToFind[i].IsEmpty )
				{
					// We find 1 let's kill it !
					return slotsToFind[i];
				}
			}

			if( bZeroFirst )
			{
				indexToFind += 1;
				if( indexToFind > 2 )
					break;
			}
			else
			{
				indexToFind -= 1;
				if( indexToFind < 0 )
				{
					indexToFind = 2;
				}
				if( indexToFind == nInitialIndex )
				{
					break;
				}
			}

		}while(true);

		// Cant find anythings , end game here ?
		return null;
	}
	#endregion

	#region Battle Calculation helper

	private void InflictDamage ( Character attacker , Character defender )
	{
		int damage = 300; //(attaker.PCombatStats.Attack - defender.PCombatStats.Defend) * 10;

		Debug.Log(attacker.PInfoStats.Name + " has deal " + damage + " to " + defender.PInfoStats.Name );

		defender.PPointStats.OnDamage( Mathf.Min( 0, -damage ) );

		BattleRecordController.RecordAttackInfo( damage , false , false , false );
	}

	private bool DoTurn ( ref int currentIndex , 
	                     ref int currentDefenderIndex,
	                     int slotCount , 
	                     Slots attackSlot , Slots defendSlot ,
	                     out bool bOutOfMove , out bool bEndGame )
	{
		Slot slotAttacker;
		Slot slotDefender;

		bOutOfMove = true;
		bEndGame = false;

		for( int i = currentIndex ; i < slotCount ; ++i )
		{
			slotAttacker = attackSlot[i];
			if(!slotAttacker.IsEmpty)
			{
				bOutOfMove = false;
				slotDefender = FindNextTarget(slotAttacker.Index , defendSlot);
				if( slotDefender != null )
				{
					InflictDamage( slotAttacker.Character , slotDefender.Character );

					BattleRecordController.RecordSlot( slotAttacker , slotDefender );

					currentIndex = FindNextTurnIndex( i + 1 , slotCount , attackSlot );

					if( slotDefender.Character.PPointStats.HasDie )
					{
						bEndGame = CheckEndGame( defendSlot );
						if( currentDefenderIndex == slotDefender.Index )
						{
							currentDefenderIndex = FindNextTurnIndex( currentDefenderIndex + 1 , 
						                                         defendSlot.Count , 
						                                         	defendSlot );
						}
					}

					return true; // next turn
				}
				else
				{
					currentIndex = slotCount;
					bEndGame = true;
					return true;
				}
			}
		}

		if( bOutOfMove )
		{
			currentIndex = slotCount;
			return true;
		}

		return true;
	}

	private int FindNextTurnIndex ( int current , int slotCount , Slots slots )
	{
		Slot slot;
		for( int i = current ; i < slotCount ; ++i )
		{
			slot = slots[i];
			if( !slot.IsEmpty )
			{
				return i;
			}
		}

		return slotCount;
	}

	private bool CheckEndGame ( Slots slots )
	{
		for( int i = 0 , count = slots.Count ; i < count ; ++i )
		{
			if( !slots[i].IsEmpty )
			{
				return false;
			}
		}

		return true;
	}

	#endregion

	#region Others helper

	#endregion

//	public Transform DeploymentLeft;
//	public Transform DeploymentRight;
//	int CurrentTurn = 0;
//
//	Slots ArmyLeft;
//	Slots ArmyRight;
//
//	public void AutoDeploy ( Slots lSlots , Slots rSlots )
//	{
//		if( lSlots != null )
//		{
//			BattleTurnController.Instance.CloneLSlot( lSlots );
////			Deploy( DEPLOY_TYPE.LEFT , BattleTurnController.Instance.LGroup );
//		}
//
//		if( rSlots != null )
//		{
//			BattleTurnController.Instance.CloneRSlot( rSlots );
////			Deploy( DEPLOY_TYPE.RIGHT , BattleTurnController.Instance.RGroup );
//		}
//
//		CalculateResult( );
//	}
//
//	private void Deploy ( DEPLOY_TYPE type , Slots slots )
//	{
////		Transform deployment = type == DEPLOY_TYPE.LEFT ? DeploymentLeft : DeploymentRight;
////
////		if( null == deployment )
////			return;
////
////		GameObject character = null;
////		Slot slot = null;
////		Transform child = null;
////
////		BattleActionController btlActController = BattleActionController.Instance;
////
////		for( int i = 0 , count = Mathf.Min( slots.Count , deployment.childCount ) ; i < count ; ++i )
////		{
////			child = deployment.GetChild(i);
////			slot = slots[i];
////			if( !slot.IsEmpty )
////			{
////				character = slot.Get();
////				Transform t = character.transform;
////
////				t.parent = deployment.GetChild(i);
////				t.localPosition = Vector3.zero;
////				if( type == DEPLOY_TYPE.RIGHT )
////				{
////					t.localScale = new Vector3(-1 , 1 , 1);
////				}
////
////				//TODO run show up action
////				btlActController.PlayActionDeployment( t , type );
////			}
////		}
//	}
//
//	public void PlayTurn ( )
//	{
//		CurrentTurn += 1;
//	}
//
//	private int CurrentStep = 0;
//
//	private void PlayStep ( bool bFirstStep = false )
//	{
//		if( bFirstStep )
//		{
//			CurrentStep = 1;
//		}
//
//		if( this.CurrentStep % 2 != 0 ) // A 1 , 3 , 5 ...step will be deploytment left step
//		{
//
//		}
//		else // 2 , 4 , 6 ...step will be deployment right step
//		{
//
//		}
//	}
//
//
//	// Battle result //
//
//	public class BattleResult
//	{
//		GameObject Attacker;
//		GameObject Defender;
//		AttackInfo Info;
//
//		public class AttackInfo
//		{
//			public int Damage;
//			public bool isMiss;
//		}
//	}
//
//	private void CalculateResult ( )
//	{
//		BattleTurnController.Instance.CalculateTurn();
//
//		List<Slot> list = BattleTurnController.Instance.ListTurn;
//
//		bool hasEnd = false;
//
//		do
//		{
//			Slot slot;
//			GameObject attacker;
//			GameObject defender;
//			for( int i = 0 , count = list.Count; i < count ; ++i )
//			{
//				slot = list[i];
//				if( !slot.IsEmpty )
//				{
////					attacker = slot.Get();
////					PointStats point = attacker.GetComponent<PointStats>();
////					if ( point != null && !point.HasDie )
////					{
////						//TODO do attacking
////					}
//				}
//			}
//		}while(!hasEnd);
//	}
}
