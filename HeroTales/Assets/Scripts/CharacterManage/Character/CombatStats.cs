using UnityEngine;
using System.Collections;

[System.Serializable]
public class CombatStats {

	public int Attack;
	public int Defend;
	public int SpAttack;
	public int SpDefend;
	public int MagAttack;
	public int MagDefend;
	public int Dodge;
	public int Hit;
	public int Critical;

	public void initialize ( int attack ,
	                        int defend ,
	                        int spAttack,
	                        int spDefend,
	                        int magAttack,
	                        int magDefend,
	                        int dodge,
	                        int hit,
	                        int critical )
	{
		this.Attack = attack;
		this.Defend = defend;
		this.SpAttack = spAttack;
		this.SpDefend = spDefend;
		this.MagAttack = magAttack;
		this.MagDefend = magDefend;
		this.Dodge = dodge;
		this.Hit = hit;
		this.Critical = critical;
	}

}
