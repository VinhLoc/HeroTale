using UnityEngine;
using System.Collections;

public class EditorCharacterStats : MonoBehaviour {

	public string FilePath = "DefaultName";
	public InfoStats Info;
	public LevelStats LevelInfo;
	public PointStats PointInfo;
	public CombatStats CombatInfo;


	public void Start ( )
	{
		Character character = new Character();
		character.PInfoStats = Info;
		character.PPointStats = PointInfo;
		character.PCombatStats = CombatInfo;
		character.PLevelStats.initialize ( LevelInfo.Level , 
		                                  LevelCharacterController
		                                  	.Instance
		                                  		.GetExpBaseOnLevel(LevelInfo.Level) );

		Character.Save( character , this.FilePath );
	}
}
