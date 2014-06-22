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

	public void PlayMove () 
	{
		CustomFade.PlayEvent( Shadow.gameObject , "AttackShadowWhite");
		CustomFade.PlayEvent( Shadow.gameObject , "AttackShadowFadeOut");
		CustomFade.PlayEvent( SpriteChar.gameObject , "AttackFadeOut" );
	}

	public void Reset ( )
	{
		CustomFade.StopEvent( Shadow.gameObject , "AttackShadowWhite");
		CustomFade.StopEvent( Shadow.gameObject , "AttackShadowFadeOut");
		CustomFade.StopEvent( SpriteChar.gameObject , "AttackFadeOut" );

		Shadow.color = new Color( 1f , 1f , 1f , 0f );
		SpriteChar.color = Color.white;
	}

	public void PlayOnDamage (bool isLeft)
	{
		iTween.MoveBy( gameObject , iTween.Hash("time" , 0.4f ,
		                                        "amount" , new Vector3( isLeft ? 0.6f : -0.6f , 0 , 0 ),
		                                        "space" , Space.World ) );
		iTween.MoveTo( gameObject , iTween.Hash("position" , Vector3.zero ,
		                                        "islocal" , true ,
		                                        "time" , 0.4f ,
		                                        "delay" , 0.4f ,
		                                        "oncompletetarget" , this.gameObject ,
		                                        "oncomplete" , "OnCompleteOnDamage" ) );

		CustomFade.PlayEvent( Shadow.gameObject , "OnDamageShadowWhite" );
		CustomFade.PlayEvent( Shadow.gameObject , "OnDamageShadowUnWhite" );
	}

	private void OnCompleteOnDamage ( )
	{
		CustomFade.StopEvent( Shadow.gameObject , "OnDamageShadowWhite" );
		CustomFade.StopEvent( Shadow.gameObject , "OnDamageShadowUnWhite" );

		BattleActionController.Instance.ShowDamage( );
	}
}
