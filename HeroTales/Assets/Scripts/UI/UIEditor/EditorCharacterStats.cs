using UnityEngine;
using System.Collections;

public class EditorCharacterStats : MonoBehaviour {

	public string FilePath = "DefaultName";
	public InfoStats Info;
	public LevelStats LevelInfo;
	public PointStats PointInfo;
	public CombatStats CombatInfo;
	public SpriteIdLinkerTag SpriteIdTag;


	public void Start ( )
	{
		PointInfo.Health.Reset();
		PointInfo.Rage.ResetToZero();
		LevelInfo.Exp.ResetToZero();

		int level = LevelInfo.Level;
		int exp = LevelCharacterController.Instance.GetExpBaseOnLevel(level);

		Character character = new Character();
		character.PInfoStats = Info;
		character.PPointStats = PointInfo;
		character.PCombatStats = CombatInfo;
		character.PLevelStats.initialize ( level , exp );
		character.SpriteIdTag = this.SpriteIdTag;

		Character.Save( character , this.FilePath );
	}
}
