using UnityEngine;
using System.Collections;

public class SwordAttackEfx : EffectBase {

	tk2dSpriteAnimator animation;

	void Awake ()
	{
		animation = transform.FindChild("Sprite").GetComponent<tk2dSpriteAnimator>();
	}


	public override void Play ()
	{
		base.Play ();

		animation.Play ( );
		animation.AnimationCompleted = AnimationEnd;
	}

	private void AnimationEnd ( tk2dSpriteAnimator sprite , tk2dSpriteAnimationClip clip )
	{
		this.gameObject.SetActive(false);

		base.End( );
	}
}