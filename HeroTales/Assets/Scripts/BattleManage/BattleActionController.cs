using UnityEngine;
using System.Collections;

public class BattleActionController : MonoBehaviour , EffectDelegate {

	private static BattleActionController _instance = null;

	public static BattleActionController Instance
	{
		get{
			if( null == _instance )
			{
				_instance = new BattleActionController();
			}

			return _instance;
		}
	}

	void Awake ( )
	{
		_instance = this;
	}

	// Move Action
	public float StartMoveDeltaPosition = 4;
	public Vector3 MoveObjPositionLeft = new Vector3( -2 , 0 , 0 );
	public Vector3 MoveObjPositionRight = new Vector3( 2 , 0 , 0 );

	public float ActionMoveDuration = 0.4f;
	public GameObject Prefab_ObjMove;
	public tk2dTextMesh DamageText;

	private const string MOVE_OBJ_NAME = "MoveObj";

	#region Deployment
	public void PlayActionDeployment ( Transform character , BattleController.DEPLOY_TYPE type )
	{
		bool isLeft = type == BattleController.DEPLOY_TYPE.LEFT;

		GameObject moveObj = GameObject.Instantiate( Prefab_ObjMove ) as GameObject;
		moveObj.name = MOVE_OBJ_NAME;
		moveObj.transform.parent = character;
		moveObj.transform.localPosition = isLeft ? MoveObjPositionLeft : MoveObjPositionRight;
		moveObj.transform.localScale = new Vector3( isLeft ? 1 : -1 , 1 , 1 );

		Vector3 endPos = character.localPosition;
		Vector3 startPos = new Vector3( isLeft ?
		                                -StartMoveDeltaPosition : StartMoveDeltaPosition
		                               		, character.localPosition.y , character.localPosition.z );

		character.localPosition = startPos;

		iTween.MoveTo( character.gameObject , 
		              iTween.Hash( "position" , endPos ,
		            				"time" , ActionMoveDuration , 
		            			"delay" , 1 ,
		            				"islocal" , true ,
		            				"oncomplete" , "onEndMoveAction" ,
		            				"oncompletetarget" , this.gameObject,
		            				"oncompleteparams" , moveObj) );
	}

	void onEndMoveAction ( object move )
	{
		GameObject moveObj = move as UnityEngine.GameObject;

		Destroy(moveObj);
	}
	#endregion

	#region Battle Move

	Transform CurrentAttacker;
	Transform CurrentDefender;
	Vector3 SavedAttackerPosition;
	bool CurrentAnimationLeftSide = false;

	public void StartAction ( Transform attacker , Transform defender , bool isLeft )
	{
		CurrentAttacker = attacker;
		CurrentDefender = defender;
		CurrentAnimationLeftSide = isLeft;

		Vector3 position = new Vector3( isLeft ? defender.position.x - 1.5f : defender.position.x + 1.5f ,
		                               defender.position.y , defender.position.z );
		SavedAttackerPosition = attacker.position;
		attacker.position = position;

		iTween.MoveBy( attacker.gameObject , iTween.Hash("time" , 0.4f ,
		                                                 "amount" , new Vector3( isLeft ? 0.6f : -0.6f , 0 , 0 ) ,
		                                                 "space" , Space.World ) );
		attacker.GetComponent<UIBattleAnimation>().PlayMove();

		EffectMgr.Instance.CreateEffect( EffectType.Attack_Sword , defender.position , this , isLeft );
	}


	public void OnEndCallback ()
	{
		CurrentAttacker.localPosition = Vector3.zero;
		CurrentAttacker.GetComponent<UIBattleAnimation>().Reset();

		CurrentDefender.GetComponent<UIBattleAnimation>().PlayOnDamage(CurrentAnimationLeftSide);
	}

	public void ShowDamage ( )
	{
		BattleRecord record = UIBattleController.Instance.GetCurrentRecord();

		ShowText( record.Damage );

		//Reduce health
		UIHealthBar hpBar = CurrentDefender.GetComponent<UIHealthBar>();
		hpBar.updateHP(record.DefenderSlot.CharHealth);

		if( record.DefenderSlot.CharHealth <= 0 )
		{
			// PLay die animation
			StartCoroutine( WaitToDie(CurrentDefender.gameObject) );
		}

		StartCoroutine( WaitForNextTurn() );
	}

	private IEnumerator WaitToDie ( GameObject death )
	{
		yield return new WaitForSeconds(0.3f);

		death.SetActive(false);
	}

	private IEnumerator WaitForNextTurn ( )
	{
		yield return new WaitForSeconds(0.5f);

		NextAction( );
	}

	public void NextAction ( )
	{
		UIBattleController.Instance.IncreaseRecordIndex();
		UIBattleController.Instance.NextAction();
	}

	public void ShowText ( int Damage )
	{
		GameObject go = DamageText.gameObject;
		go.SetActive(true);
		DamageText.text = Damage.ToString();

		Vector3 position = new Vector3( CurrentDefender.position.x , CurrentDefender.position.y + 2 ,
		                               CurrentDefender.position.z );

		go.transform.position = position;
		go.transform.localScale = Vector3.zero;

		iTween.Stop(go);
		
		CustomFade.StopEvent( go , "TextShowFade" );
		CustomFade.StopEvent(go , "TextHideFade" );

		iTween.ScaleTo( go , iTween.Hash("scale" , new Vector3( 5 , 5 , 5 ) ,
		                                                    "time" , 0.7f ,
		                                                    "easetype" , iTween.EaseType.easeOutElastic ) );

		CustomFade.PlayEvent( go , "TextShowFade" );

		iTween.MoveBy( go , iTween.Hash("amount" , new Vector3( 0 , 1 , 0 ),
		                                                   "time" , 0.5f ,
		                                                   "delay" , 0.65f ,
		                                					"oncompletetarget" , go,
		                                                   "oncomplete" , "RemoveText" ) );
		CustomFade.PlayEvent(go , "TextHideFade" );
	}

	private void RemoveText ( )
	{
		DamageText.gameObject.SetActive(false);
	}

	#endregion
}
