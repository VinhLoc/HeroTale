using UnityEngine;
using System.Collections;

public class CharacterFactory {

	private static CharacterFactory _instance = null;

	private CharacterFactory ( )
	{

	}

	public static CharacterFactory Instance
	{
		get{
			if( _instance == null )
			{
				_instance = new CharacterFactory( );
			}

			return _instance;
		}
	}

	private ulong _uid;

	public Character createNewCharacter ( InfoStats.CLASS_TYPE classType , string charName )
	{
		if( _uid == ulong.MaxValue )
		{
			Debug.LogError("ID is out of range");
			return null;
		}

		Character character = new Character( );

		character.PInfoStats.initialize( _uid++ , charName , "This is description" , classType );
		character.PLevelStats.initialize( 1 , 200 );

		return character;
	}


	private string getResourceTagByClassType ( InfoStats.CLASS_TYPE type )
	{
		switch ( type )
		{
		case InfoStats.CLASS_TYPE.PALADIN :
			return ResourceMgr.TAG_MAIN_PAL_0;

		case InfoStats.CLASS_TYPE.ASSASSIN :
			return ResourceMgr.TAG_MAIN_ASS_0;

		case InfoStats.CLASS_TYPE.ARCHER :
			return ResourceMgr.TAG_MAIN_ARC_0;
		}

		return null;
	}
}
