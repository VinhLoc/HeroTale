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

		Character.Save( character , this.FilePath );
	}
}
