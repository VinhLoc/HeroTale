using UnityEngine;
using System.Collections;

public class CustomFade : MonoBehaviour {

	public enum FadeType {
		FadeIn,
		FadeOut
	};

	public string EventName;
	
	/// <summary>
	/// Type of fade
	/// </summary>
	public FadeType Type = FadeType.FadeIn;
	
	/// <summary>
	/// Duration of the effect
	/// </summary>
	public float Duration;

	public bool NoDuration;

	public bool Loop;
	public bool PingPong;
	
	/// <summary>
	/// The delay of effect.
	/// </summary>
	public float Delay;
	
	public bool IsUpdateChildAlso = false;
	
	public bool Running = false;
	public bool AutoRun = false;
	
	public bool Delaying = false;
	public float Alpha = 0;
	public float AlphaFrom = 0;
	public float AlphaTo = 0;

    public bool IgnoreTimeScale = false;
	
	protected float fadePerSec = 0;
	protected float alphaChanel = 0;

	public virtual void StartAction ( ) { }
	public virtual IEnumerator updateColor ( ) { yield break; }
	public virtual void StopAction ( ) { }
	public virtual IEnumerator updateNoDuration ( ) { yield break; }

    float lastTime = 0;
    protected float getTime ( )
    {
        float delta = Time.deltaTime;
        if( IgnoreTimeScale )
        {
            delta = Time.realtimeSinceStartup - lastTime;
        }

        lastTime = Time.realtimeSinceStartup;

        return delta;
    }

	protected void updateChildArray( Transform transform )
	{
		int count = transform.childCount;
		
		//Debug.Log("Child count of " + transform.gameObject.name + " : " + count);
		
		Transform childTransform = null;
		CustomFade fadeScript = null;
		Component component = null;
		
		for( int i = 0 ; i < count ; ++i )
		{
			childTransform = transform.GetChild(i);
			
			if( childTransform.gameObject.activeInHierarchy )
			{
				//Debug.LogError("Object : " + childTransform.gameObject.name );
				
				component = childTransform.GetComponent<tk2dBaseSprite>();
				if( component )
				{
					fadeScript = childTransform.gameObject.GetComponent<SpriteFade>();
					if ( null == fadeScript )
						fadeScript = childTransform.gameObject.AddComponent<SpriteFade>();
				}
				
				if( fadeScript )
				{
					//Debug.Log("Name : " + fadeScript.name) ;
					fadeScript.Type = this.Type;
					fadeScript.Duration = this.Duration;
					fadeScript.Delay = this.Delay;
					fadeScript.IsUpdateChildAlso = this.IsUpdateChildAlso;
					fadeScript.Alpha = this.Alpha;
					fadeScript.NoDuration = this.NoDuration;
					fadeScript.AlphaFrom = this.AlphaFrom;
					fadeScript.AlphaTo = this.AlphaTo;
                    fadeScript.IgnoreTimeScale = this.IgnoreTimeScale;
					
					fadeScript.StartAction( );
				}
				else
				{
					updateChildArray( childTransform );
				}
			}
			
			childTransform = null;
			fadeScript = null;
			component = null;
		}
	}
	
	protected void updateChild( )
	{
		if( IsUpdateChildAlso )
		{
			updateChildArray(transform);
		}
	}

    float curTime = 0;
    protected void setupTimer ( )
    {
        lastTime = Time.realtimeSinceStartup;
    }
    protected bool GetDelay ( )
    {
        if( this.Delaying )
        {
            float delta = getTime();
            //Debug.Log("Delta : " + delta );
            curTime += delta;
            //Debug.Log("Cur : " + curTime);
            //Debug.Log("Delay : " + this.Delay );
            if( curTime >= this.Delay )
            {
               // Debug.Log("WTF");
                curTime = 0;
                this.Delaying = false;
                return false;
            }

            return true;
        }

        return false;
    }

	public static void PlayEvent ( GameObject go , string eventName )
	{
		CustomFade[] fades = go.GetComponents<CustomFade>();

		foreach( var fade in fades )
		{
			if( fade.EventName == eventName )
			{
				fade.StartAction( );
				return;
			}
		}
	}

	public static void StopEvent ( GameObject go , string eventName )
	{
		CustomFade[] fades = go.GetComponents<CustomFade>();
		
		foreach( var fade in fades )
		{
			if( fade.EventName == eventName )
			{
				fade.StopAction();
				return;
			}
		}
	}
}
