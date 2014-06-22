using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBattleController : MonoBehaviour {

	private static UIBattleController _instance = null;

	public static UIBattleController Instance
	{
		get{
			if( null == _instance )
			{
				GameObject go = new GameObject("UIBattleController");
				_instance = go.AddComponent<UIBattleController>();
			}

			return _instance;
		}
	}

	void Awake()
	{
		_instance = this;
	}
	

	#region Deploy
	public GameObject Prefab_Character;
	public Transform DeploymentLeft;
	public Transform DeploymentRight;
	public void Deploy( Slots lSlot , Slots rSlot )
	{
		Deploy( BattleController.DEPLOY_TYPE.LEFT , lSlot );
		Deploy( BattleController.DEPLOY_TYPE.RIGHT , rSlot );
	}

	private void Deploy ( BattleController.DEPLOY_TYPE type , Slots slots )
	{
		Transform deployment = type == BattleController.DEPLOY_TYPE.LEFT ? DeploymentLeft : DeploymentRight;

		if( null == deployment || null == slots )
			return;

		Transform child;
		Slot slot;
		GameObject go;
		UICharacter characterUIScriptHandler;
		UIHealthBar healthbarUIScriptHandler;

		BattleActionController actionController = BattleActionController.Instance;

		for( int i = 0 , count = Mathf.Min( slots.Count , deployment.childCount ) ; i < count ; ++i )
		{
			child = deployment.GetChild(i);
			slot = slots[i];

			if( !slot.IsEmpty )
			{
				go = GameObject.Instantiate( Prefab_Character ) as GameObject;
				go.name = slot.Character.PInfoStats.Name;
				go.transform.parent = child;
				go.transform.localPosition = Vector3.zero;

				//TODO get UICharacter script to do initialize
				// like sprite.Init( Character character , int deployType );
				characterUIScriptHandler = go.GetComponent<UICharacter>();
				if( null != characterUIScriptHandler )
				{
					characterUIScriptHandler.Init( slot.Index , slot.Character , type );
				}

				healthbarUIScriptHandler = go.GetComponent<UIHealthBar>();
				if( null != healthbarUIScriptHandler )
				{
					healthbarUIScriptHandler.init( slot.Character.PPointStats.Health.Max );
				}

				actionController.PlayActionDeployment( go.transform , type );
			}
		}

		StartCoroutine( StartBattle() );
	}
	#endregion

	#region Battle

	private List<BattleRecord> Records;
	private int CurrentRecordIndex = 0;

	private IEnumerator StartBattle ( )
	{
		yield return new WaitForSeconds(BattleActionController.Instance.ActionMoveDuration + 1 + 0.2f);

		Records = BattleRecordController.Instance.PBattleRecords;
		CurrentRecordIndex = 0;

		NextAction ( );
	}

	public void NextAction ( )
	{
		if( CurrentRecordIndex >= Records.Count )
		{
			//on END GAME
			return;
		}

		BattleRecord record = Records[CurrentRecordIndex];

		Transform attackerDeployment = record.IsLeft ? DeploymentLeft : DeploymentRight;
		Transform defenderDeployment = record.IsLeft ? DeploymentRight : DeploymentLeft;

		Transform attacker = GetCharacterInCell( attackerDeployment , record.AttackerSlot.Index ,
		                                        record.AttackerSlot.Charname );

		Transform defender = GetCharacterInCell( defenderDeployment , record.DefenderSlot.Index ,
		                                        record.DefenderSlot.Charname );

		BattleActionController.Instance.StartAction( attacker , defender , record.IsLeft );
	}

	public void IncreaseRecordIndex ()
	{
		CurrentRecordIndex += 1;
	}

	public BattleRecord GetCurrentRecord ()
	{
		return Records[CurrentRecordIndex];
	}

	private Transform GetCharacterInCell ( Transform deployment , int cellIndex , string characterName )
	{
		Transform Cell = deployment.GetChild(cellIndex);

		return Cell.FindChild(characterName);
	}

	#endregion
}
