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

	[SerializeField]
	public int Level;
	[SerializeField]
	public int RequireExp;
}

[System.Serializable]
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

	public int GetExpBaseOnLevel ( int level )
	{
		LevelExp levelExp;
		for( int i = 0 , count = NextLvExpMap.Count ; i < count ; ++i )
		{
			levelExp = NextLvExpMap[i];
			if( levelExp.Level == level )
			{
				return levelExp.RequireExp;
			}
		}

		return 0;
	}

	public static bool Save ( )
	{
		return FileMgr.Save( LevelCharacterController._instance , 
		                    typeof(LevelCharacterController) , 
		                    ConstantValue.FILE_NAME_NEXTLVEXP , true );
	}

	public static LevelCharacterController Load ( )
	{
		return FileMgr.Load( typeof(LevelCharacterController) , 
		                    ConstantValue.FILE_NAME_NEXTLVEXP , true ) as LevelCharacterController;
	}
}
