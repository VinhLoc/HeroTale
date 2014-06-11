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

	public Transform DeploymentLeft;
	public Transform DeploymentRight;
	int CurrentTurn = 0;

	Slots ArmyLeft;
	Slots ArmyRight;

	public void AutoDeploy ( Slots lSlots , Slots rSlots )
	{
		if( lSlots != null )
		{
			BattleTurnController.Instance.CloneLSlot( lSlots );
//			Deploy( DEPLOY_TYPE.LEFT , BattleTurnController.Instance.LGroup );
		}

		if( rSlots != null )
		{
			BattleTurnController.Instance.CloneRSlot( rSlots );
//			Deploy( DEPLOY_TYPE.RIGHT , BattleTurnController.Instance.RGroup );
		}

		CalculateResult( );
	}

	private void Deploy ( DEPLOY_TYPE type , Slots slots )
	{
		Transform deployment = type == DEPLOY_TYPE.LEFT ? DeploymentLeft : DeploymentRight;

		if( null == deployment )
			return;

		GameObject character = null;
		Slot slot = null;
		Transform child = null;

		BattleActionController btlActController = BattleActionController.Instance;

		for( int i = 0 , count = Mathf.Min( slots.Count , deployment.childCount ) ; i < count ; ++i )
		{
			child = deployment.GetChild(i);
			slot = slots[i];
			if( !slot.IsEmpty )
			{
				character = slot.Get();
				Transform t = character.transform;

				t.parent = deployment.GetChild(i);
				t.localPosition = Vector3.zero;
				if( type == DEPLOY_TYPE.RIGHT )
				{
					t.localScale = new Vector3(-1 , 1 , 1);
				}

				//TODO run show up action
				btlActController.PlayActionDeployment( t , type );
			}
		}
	}

	public void PlayTurn ( )
	{
		CurrentTurn += 1;
	}

	private int CurrentStep = 0;

	private void PlayStep ( bool bFirstStep = false )
	{
		if( bFirstStep )
		{
			CurrentStep = 1;
		}

		if( this.CurrentStep % 2 != 0 ) // A 1 , 3 , 5 ...step will be deploytment left step
		{

		}
		else // 2 , 4 , 6 ...step will be deployment right step
		{

		}
	}


	// Battle result //

	public class BattleResult
	{
		GameObject Attacker;
		GameObject Defender;
		AttackInfo Info;

		public class AttackInfo
		{
			public int Damage;
			public bool isMiss;
		}
	}

	private void CalculateResult ( )
	{
		BattleTurnController.Instance.CalculateTurn();

		List<Slot> list = BattleTurnController.Instance.ListTurn;

		bool hasEnd = false;

		do
		{
			Slot slot;
			GameObject attacker;
			GameObject defender;
			for( int i = 0 , count = list.Count; i < count ; ++i )
			{
				slot = list[i];
				if( !slot.IsEmpty )
				{
					attacker = slot.Get();
					PointStats point = attacker.GetComponent<PointStats>();
					if ( point != null && !point.HasDie )
					{
						//TODO do attacking
					}
				}
			}
		}while(!hasEnd);
	}
}
