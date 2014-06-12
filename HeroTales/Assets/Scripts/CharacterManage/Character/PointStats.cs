using UnityEngine;
using System.Collections;

[System.Serializable]
public class PointStats {

	[SerializeField]
	public PointBase Health = new PointBase();
	[SerializeField]
	public PointBase Rage = new PointBase();
	
	public bool HasDie
	{
		get{
			return Health.Current <= 0;
		}
	}

	/// <summary>
	/// Deal damage to the Point stats 
	/// NOTE : "+" positive is healing, "-" negative is dealing
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void OnDamage ( int damage )
	{
		Health.Current += damage;
	}

	public void OnIncreaseRage ( int rage )
	{
		Rage.Current += rage;
	}

	public void Reset ( )
	{
		Health.Reset( );
		Rage.ResetToZero( );
	}

	public void initialize ( int maxHp , int maxRage )
	{
		this.Health.Max = maxHp;
		this.Health.Current = maxHp;
		this.Rage.Max = maxRage;
		this.Rage.Current = 0;
	}
}
