using UnityEngine;
using System.Collections;

public class CharacterBoardInfo : MonoBehaviour
{
	public tk2dSprite avatar;

	public tk2dTextMesh text_level;
	public tk2dTextMesh text_diamond;
	public tk2dTextMesh text_fire;
	public tk2dTextMesh text_gold;

	public tk2dUIProgressBar pro_energy;
	public tk2dUIProgressBar pro_exp;

	void Start()
	{
		// TODO: we need to get infor of player to update board
		UpdateBoard (InfoStats.CLASS_TYPE.ASSASSIN,
		            20, 2000, 200, 2000, 0.5f, 0.7f);
	}

	public void UpdateBoard(InfoStats.CLASS_TYPE class_name, 
	                        int level, int diamond, int fire, int gold,
	                        float energy, float exp)
	{
		text_level.text = level.ToString ();
		text_diamond.text = diamond.ToString ();
		text_fire.text = fire.ToString ();
		text_gold.text = gold.ToString ();

		pro_exp.Value = exp;
		pro_energy.Value = energy;

		switch (class_name) {
		case InfoStats.CLASS_TYPE.PALADIN:
			avatar.SetSprite("beastmaster_ava");
			break;
		case InfoStats.CLASS_TYPE.ARCHER:
			avatar.SetSprite("gunner_ava");
			break;
		case InfoStats.CLASS_TYPE.ASSASSIN:
			avatar.SetSprite("sorceress_ava");
			break;
		}
	}
}

