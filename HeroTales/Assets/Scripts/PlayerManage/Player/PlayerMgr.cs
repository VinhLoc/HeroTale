using UnityEngine;
using System.Collections;

public class PlayerMgr {

	private static PlayerMgr instance = null;
	private PlayerMgr ( )
	{
		
	}
	
	
	public static PlayerMgr Instance
	{
		get{
			if( instance == null )
			{
				instance = new PlayerMgr ( );
			}
			
			return instance;
		}
	}

	public Player CurrentPlayer { get; set; }
}
