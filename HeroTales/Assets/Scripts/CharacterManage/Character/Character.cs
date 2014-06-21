using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("Character")]
public class Character {

	[XmlElement("InfoStats")]
	public InfoStats PInfoStats { get; set; }

	[XmlElement("LevelStats")]
	public LevelStats PLevelStats { get; set; }

	[XmlElement("PointStats")]
	public PointStats PPointStats { get; set; }

	[XmlElement("CombatStats")]
	public CombatStats PCombatStats { get; set; }

	[XmlElement("SpriteLinkerTag")]
	public SpriteIdLinkerTag SpriteIdTag { get; set; }

	public Character ()
	{
		PInfoStats = new InfoStats( );
		PLevelStats = new LevelStats( );
		PPointStats = new PointStats( );
		PCombatStats = new CombatStats( );
	}

	public void Revive ( )
	{
		PPointStats.Health.Reset( );
		PPointStats.Rage.ResetToZero( );
	}

	public static bool Save ( Character character , string sForceFilePath = "" )
	{
		string sPath = "";
		bool bForcePath = !string.IsNullOrEmpty(sForceFilePath);
		if( !bForcePath )
			sPath = string.Format( ConstantValue.FILE_NAME_CHARACTER , character.PInfoStats.UID  );
		else
			sPath = sForceFilePath;

		return FileMgr.Save( character , typeof(Character) , sPath , bForcePath );
	}

	public static Character Load ( int uid , string sForceFilePath = "" )
	{
		string sPath = "";
		bool bForcePath = !string.IsNullOrEmpty(sForceFilePath);

		if( !bForcePath )
			sPath = string.Format( ConstantValue.FILE_NAME_CHARACTER , uid  );
		else
			sPath = sForceFilePath;

		return FileMgr.Load( typeof(Character) , sPath , bForcePath ) as Character;
	}
}
