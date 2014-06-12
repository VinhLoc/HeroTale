using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class LevelExp
{
	public LevelExp ( )
	{

	}

	public LevelExp( int level , int exp )
	{
		this.Level = level;
		this.RequireExp = exp;
	}

	public int Level;
	public int RequireExp;
}

public class LevelExpMap : List<LevelExp>
{
	public LevelExpMap ( )
	{

	}
}

[XmlRoot("LevelCharacterController")]
public class LevelCharacterController {

	private static LevelCharacterController _instance = null;
	
	public static LevelCharacterController Instance
	{
		get{
			if ( null == _instance )
			{
				_instance = LevelCharacterController.Load( );
				if( _instance == null )
				{
					_instance = new LevelCharacterController( );
					LevelCharacterController.Save();
				}
			}

			return _instance;
		}
	}

	public LevelExpMap NextLvExpMap = new LevelExpMap ( );

	public static bool Save ( )
	{
		return FileMgr.Save( LevelCharacterController._instance , typeof(LevelCharacterController) , ResourceMgr.FILE_NAME_NEXTLVEXP );
	}

	public static LevelCharacterController Load ( )
	{
		return FileMgr.Load( typeof(LevelCharacterController) , ResourceMgr.FILE_NAME_NEXTLVEXP ) as LevelCharacterController;
	}
}
