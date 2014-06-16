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

		Character character = this.getCharacterTemplateByClass( classType );

		if( character != null )
		{
			character.PInfoStats.UID = _uid++;
			character.PInfoStats.Name = charName;
		}

		return character;
	}


	private Character getCharacterTemplateByClass ( InfoStats.CLASS_TYPE type )
	{
		switch ( type )
		{
		case InfoStats.CLASS_TYPE.PALADIN :
			return Character.Load( 0 , ConstantValue.FILE_TEMPALTE_PAL );

		case InfoStats.CLASS_TYPE.ASSASSIN :
			return Character.Load( 0 , ConstantValue.FILE_TEMPALTE_ASS );

		case InfoStats.CLASS_TYPE.ARCHER :
			return Character.Load( 0 , ConstantValue.FILE_TEMPALTE_ARC );
		}

		return null;
	}
}
