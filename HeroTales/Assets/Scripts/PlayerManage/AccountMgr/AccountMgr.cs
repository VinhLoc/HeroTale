using UnityEngine;
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

	public void createNewAccount ( string username )
	{
		if( _uid == ulong.MaxValue )
		{
			Debug.LogError("ID is out of range");
			return;
		}

		Player player = new Player( );
		player.Initialize( );

		player.PAccount.initialize( _uid++ , username );
		player.PCharacters.initialize( 9 , 1 );
		player.PEnergy.initialize(250);
		player.PTactis.Initialize(9,1);

		PlayerMgr.Instance.CurrentPlayer = player;
	}
}
