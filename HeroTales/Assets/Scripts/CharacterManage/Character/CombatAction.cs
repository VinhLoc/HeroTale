using UnityEngine;
using System.Collections;

public class CombatAction  {

	public enum WEAPON_TYPE
	{
		Melee,
		Range
	}

	public enum ACTION_TYPE
	{
		//TODO
		None
	}

	public WEAPON_TYPE WeaponType;
	public ACTION_TYPE ActionType;

	public void initialize ( CombatAction.WEAPON_TYPE weaponType , CombatAction.ACTION_TYPE actionType )
	{
		this.WeaponType = weaponType;
		this.ActionType = actionType;
	}
}
