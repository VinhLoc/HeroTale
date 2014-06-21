using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpriteFade : CustomFade {


	tk2dBaseSprite sprite;
	float startAlpha ; //this is for save alpha when fade out

	// Use this for initialization
	void Start () {
		if( AutoRun )
		{
			StartAction ( );
		}
	}

	public override IEnumerator updateNoDuration ()
	{
		setupTimer();
		while( GetDelay() )
		{
			yield return null;
		}

		if( sprite && this.Running )
		{
			Color color = sprite.color;
			
			if( Type == FadeType.FadeIn )
			{
				sprite.color = new Color( color.r , color.g , color.b , alphaChanel );
				StopAction( );
				yield break;
			}
			else if ( Type == FadeType.FadeOut )
			{
				sprite.color = new Color( color.r , color.g , color.b , alphaChanel );
				StopAction( );
				yield break;
			}
			
			yield break;
		}
	}

	public override IEnumerator updateColor ( )
	{
        setupTimer();
		while( GetDelay() )
        {
            yield return null;
        }

		while( sprite && this.Running )
		{
			Color color = sprite.color;
			
			if( Type == FadeType.FadeIn )
			{
                color.a += fadePerSec * getTime();
				if( color.a >= alphaChanel )
				{
					sprite.color = new Color( color.r , color.g , color.b , alphaChanel );
					StopAction( );
					if( Loop )
					{
						sprite.color = new Color(sprite.color.r , sprite.color.g , sprite.color.g , startAlpha );
						StartAction( );
					}
					else if ( PingPong )
					{
						Type = FadeType.FadeOut;
						float temp = AlphaFrom;
						AlphaFrom = AlphaTo;
						AlphaTo = temp;
						Alpha = temp;
						StartAction( );
					}
					yield break;
				}
				sprite.color = color;
			}
			else if ( Type == FadeType.FadeOut )
			{
				color.a -= fadePerSec * getTime();
				if( color.a <= alphaChanel )
				{
					sprite.color = new Color( color.r , color.g , color.b , alphaChanel );
					StopAction( );
					if( Loop )
					{
						sprite.color = new Color(sprite.color.r , sprite.color.g , sprite.color.g , startAlpha );
						StartAction( );
					}
					else if ( PingPong )
					{
						Type = FadeType.FadeIn;
						float temp = AlphaFrom;
						AlphaFrom = AlphaTo;
						AlphaTo = temp;
						Alpha = temp;
						StartAction( );
					}
					yield break;
				}
				
				sprite.color = color;
			}

			yield return null;
		}
	}

	public override void StartAction( )
	{
		updateChild( );

		sprite = GetComponent<tk2dBaseSprite>();
		AlphaTo = Alpha;
		alphaChanel = AlphaTo / 255;

		if( sprite )
		{
			sprite.color = new Color(sprite.color.r , sprite.color.g , sprite.color.g , AlphaFrom / 255 );
			startAlpha = sprite.color.a;

			if( Type == FadeType.FadeIn)
			{
//				sprite.color = new Color(sprite.color.r , sprite.color.g , sprite.color.g , AlphaFrom );
//				startAlpha = sprite.color.a;

				if( Duration != 0 )
				{
					fadePerSec = ( alphaChanel ) / Duration;
				}
				else
				{
					fadePerSec = alphaChanel;
				}
			}
			else if( Type == FadeType.FadeOut )
			{
				startAlpha = sprite.color.a;
				if( Duration != 0 )
				{
					fadePerSec = ( sprite.color.a - this.alphaChanel ) / Duration;
				}
				else
				{
					fadePerSec = alphaChanel;
				}
			}
		}

		this.Running = true;
		this.Delaying = this.Delay > 0;

		if( NoDuration )
		{
			StartCoroutine("updateNoDuration");
		}
		else
		{
			StartCoroutine("updateColor");
		}
	}

	public override void StopAction ( )
	{
		this.Running = false;
	}
}
