using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot("Player")]
public class Player
{

	public Player ( )
	{
		PAccount = new AccountInfo ();
		PBalance = new BalanceInfo ();
		PEnergy = new EnergyInfo ();
		PCharacters = new CharacterInfo ();
		PTactis = new Tactics ();
	}

	[XmlElement("AccountInfo")]
	public AccountInfo PAccount { get; set; }

	[XmlElement("BalanceInfo")]
	public BalanceInfo PBalance { get; set; }

	[XmlElement("Energy")]
	public EnergyInfo PEnergy { get; set; }

	[XmlElement("CharacterInfo")]
	public CharacterInfo PCharacters { get; set; }

	[XmlElement("Tactics")]
	public Tactics PTactis { get; set; }

	public static bool Save (Player player)
	{
		string sPath = string.Format( ConstantValue.FILE_NAME_PLAYER , player.PAccount.UserID );

		PlayerPrefs.SetInt(ConstantValue.PREFS_LAST_USER_ID , (int)player.PAccount.UserID );
		PlayerPrefs.Save();

		return FileMgr.Save( player , typeof(Player) , sPath );
	}

	public static Player Load (int userId)
	{
		string sPath = string.Format( ConstantValue.FILE_NAME_PLAYER , userId );

		return FileMgr.Load( typeof(Player) , sPath ) as Player;
	}
}
