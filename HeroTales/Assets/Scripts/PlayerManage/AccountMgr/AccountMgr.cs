﻿using UnityEngine;
using System.Collections;

public class AccountMgr {

	private static AccountMgr instance = null;
	private AccountMgr ( )
	{

	}


	public static AccountMgr Instance
	{
		get{
			if( instance == null )
			{
				instance = new AccountMgr ( );
			}

			return instance;
		}
	}


	private ulong _uid = 0;

	public Player createNewAccount ( string username )
	{
		if( _uid == ulong.MaxValue )
		{
			Debug.LogError("ID is out of range");
			return null;
		}

		Player player = new Player( );

		player.PAccount.initialize( _uid++ , username );
		player.PCharacters.initialize( 9 , 1 );
		player.PEnergy.initialize(250);
		player.PTactis.Initialize(25 , 9 , 9 );

		return player;
	}
}
