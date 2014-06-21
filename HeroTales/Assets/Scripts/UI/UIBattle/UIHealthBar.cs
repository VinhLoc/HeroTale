using UnityEngine;
using System.Collections;

public class UIHealthBar : MonoBehaviour {

	public tk2dUIProgressBar ProgressBar;
	public tk2dBaseSprite ProgressCore;
	public int MaxHeath;
	public int CurrentHeath;
	
	float HPLostDelta = 0;
	float HPLostDeltaPerSecond = 0;
	
	float Current;
	float End;
	bool IsIncrease = false;
	
	float MaxDuration = 1.0f; // In Second for Full HP

	Color BarGreen = new Color( 0 , 255 , 0 , 255 );
	Color BarRed = new Color( 255 , 0 , 0 , 255 );
	Color BarYellow = new Color( 255 , 255 , 0 , 255 );
	
	// Use this for initialization
	void Start () {
		//		init ( 100 );
	}
	
	public void init( int MaxHeath )
	{
		this.CurrentHeath = 0;
		this.MaxHeath = MaxHeath;
		ProgressBar.Value = 0;
		ProgressCore.color = BarGreen;

		updateHP(MaxHeath);
	}
	
	public void updateHP( int CurrentHeath )
	{
		float delta = this.CurrentHeath - CurrentHeath;
		this.CurrentHeath = CurrentHeath;
		if( delta == 0 )
			return;
		HPLostDelta = delta / this.MaxHeath;
		Current = ProgressBar.Value;
		End = Current - HPLostDelta;
		HPLostDeltaPerSecond =  1 /(MaxDuration);

		IsIncrease = End > Current;

		HPLostDeltaPerSecond = IsIncrease ? -HPLostDeltaPerSecond : HPLostDeltaPerSecond;
		
		if (gameObject.activeInHierarchy)
			StartCoroutine("updateBar");
	}
	
	public IEnumerator updateBar ( )
	{
		while(true)
		{
			ProgressBar.Value -= HPLostDeltaPerSecond * Time.deltaTime;
			
			if( ProgressBar.Value > 0.15f && ProgressBar.Value < 0.4f )
			{
				ProgressCore.color = BarYellow;
			}
			if( ProgressBar.Value < 0.15f )
			{
				ProgressCore.color = BarRed;
			}
			if( ProgressBar.Value > 0.4f )
			{
				ProgressCore.color = BarGreen;
			}

			if( !IsIncrease )
			{
				if( ProgressBar.Value <= End )
				{
					ProgressBar.Value = End;
				
					yield break;
				}
			}else
			{
				if( ProgressBar.Value >= End )
				{
					ProgressBar.Value = End;

					yield break;
				}
			}
			
			yield return null;
		}
	}
	
	public float getHealthPercent()
	{
		return (float)CurrentHeath / (float)MaxHeath;
	}

}
