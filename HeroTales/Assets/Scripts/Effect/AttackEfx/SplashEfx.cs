using UnityEngine;
using System.Collections;

public class SplashEfx : EffectBase {

	public tk2dBaseSprite Splash_1;
	public tk2dBaseSprite Splash_2;

	void Start()
	{
		Play();
	}

	public override void Play ()
	{
		base.Play ();

		StartCoroutine( PlayEfx() );
	}

	IEnumerator PlayEfx ( )
	{
		Splash_1.gameObject.SetActive(true);
		Splash_2.gameObject.SetActive(true);

		yield return new WaitForSeconds(0.6f);

		Splash_1.gameObject.SetActive(false);
		Splash_2.gameObject.SetActive(false);
	}
}
