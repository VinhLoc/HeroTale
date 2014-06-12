using UnityEngine;
using System.Collections;

public class BattleActionController : MonoBehaviour {

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

	void Start ( )
	{
//		ResourceMgr.LoadPrefab( ResourceMgr.PREFABS_TAG_BATTLE );

//		PreMove = ResourceMgr.GetPrefab(ResourceMgr.TAG_BATTLE_MOVE);
	}

	// Move Action
	public float StartMoveDeltaPosition = 4;
	public Vector3 MoveObjPosition = new Vector3( -2 , 0 , 0 );

	public float ActionMoveDuration = 0.4f;
	private GameObject PreMove;

	private const string MOVE_OBJ_NAME = "MoveObj";

	public void PlayActionDeployment ( Transform character , BattleController.DEPLOY_TYPE type )
	{
		bool isLeft = type == BattleController.DEPLOY_TYPE.LEFT;

		GameObject moveObj = GameObject.Instantiate( PreMove ) as GameObject;
		moveObj.name = MOVE_OBJ_NAME;
		moveObj.transform.parent = character;
		moveObj.transform.localPosition = MoveObjPosition;

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
		            				"oncompleteparams" , character) );

		StartCoroutine( onStartBattle() );
	}

	void onEndMoveAction ( object character )
	{
		Transform @char = character as Transform;

		Transform moveObj = @char.FindChild(MOVE_OBJ_NAME);
		if( moveObj )
		{
			Destroy(moveObj.gameObject);
		}
	}

	IEnumerator onStartBattle ( )
	{
		yield return new WaitForSeconds( this.ActionMoveDuration );


	}
}
