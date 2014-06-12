using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("Character")]
public class Character {

	[XmlElement("InfoStats")]
	public InfoStats PInfoStats { get; set; }

	[XmlElement("LevelStats")]
	public LevelStats PLevelStats { get; set; }

	public Character ()
	{
		PInfoStats = new InfoStats( );
		PLevelStats = new LevelStats( );
	}

	public static bool Save ( Character character )
	{
		string sPath = string.Format( ResourceMgr.FILE_NAME_CHARACTER , character.PInfoStats.UID  );
		
		return FileMgr.Save( character , typeof(Character) , sPath );
	}

	public static Character Load ( int uid )
	{
		string sPath = string.Format( ResourceMgr.FILE_NAME_CHARACTER , uid  );

		return FileMgr.Load( typeof(Character) , sPath ) as Character;
	}
}
