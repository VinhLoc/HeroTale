using UnityEngine;
using System.Collections;

public class UIBattleAnimation : MonoBehaviour {

	tk2dBaseSprite Shadow;
	tk2dBaseSprite SpriteChar;
	Transform transformCache;

	void Awake ()
	{
		transformCache = transform;
		Shadow = transformCache.Find("ScaleObject/Shadow").GetComponent<tk2dBaseSprite>();
		SpriteChar = transformCache.Find("ScaleObject/Sprite").GetComponent<tk2dBaseSprite>();
	}

	public void MoveTo ( bool isLeft , Transform defender )
	{
		Vector3 position = transformCache.position;

		position = new Vector3( isLeft ? defender.position.x - 1.5f : defender.position.x + 1.5f ,
		                       defender.position.y , defender.position.z );

		transformCache.position = position;

		iTweenEvent.GetEvent( transformCache.gameObject , isLeft ? "MoveLeftForward" : "MoveRightForward" );
		CustomFade.PlayEvent( Shadow.gameObject , "AttackShadowWhite");
		CustomFade.PlayEvent( Shadow.gameObject , "AttackShadowFadeOut");
		CustomFade.PlayEvent( SpriteChar.gameObject , "AttackFadeOut" );


	}

	private IEnumerator ShowEfx ( Transform defenderPosition )
	{
		yield break;
	}
}
