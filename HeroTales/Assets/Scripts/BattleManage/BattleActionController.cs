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

	// Move Action
	public float StartMoveDeltaPosition = 4;
	public Vector3 MoveObjPositionLeft = new Vector3( -2 , 0 , 0 );
	public Vector3 MoveObjPositionRight = new Vector3( 2 , 0 , 0 );

	public float ActionMoveDuration = 0.4f;
	public GameObject Prefab_ObjMove;

	private const string MOVE_OBJ_NAME = "MoveObj";

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
}
