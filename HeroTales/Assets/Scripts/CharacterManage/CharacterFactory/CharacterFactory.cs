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

	public GameObject createNewCharacter ( InfoStats.CLASS_TYPE classType , string charName )
	{
		string tag = getResourceTagByClassType(classType);

		GameObject character = GameObject.Instantiate( ResourceMgr.GetPrefab( tag ) ) as GameObject;
		character.name = charName;

		InfoStats info = character.GetComponent<InfoStats>();
		info.UID = _uid++;
		info.Name = charName;

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
