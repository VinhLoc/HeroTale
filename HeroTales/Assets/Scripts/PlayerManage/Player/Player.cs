using UnityEngine;
using System.Collections;

public class Player {

	public AccountInfo PAccount { get; set; }
	public BalanceInfo PBalance { get; set; }
	public EnergyInfo PEnergy { get; set; }
	public CharacterInfo PCharacters { get; set; }
	public Tactics PTactis { get; set; }

	public void Initialize ( )
	{
		PAccount = new AccountInfo( );
		PBalance = new BalanceInfo( );
		PEnergy = new EnergyInfo( );
		PCharacters = new CharacterInfo( );
		PTactis = new Tactics( );
	}
}
