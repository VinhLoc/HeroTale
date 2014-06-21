using UnityEngine;
using System.Collections;

public class SelectCharControl : MonoBehaviour
{
	public InfoStats.CLASS_TYPE mainClass { get; set; }

	public tk2dUIToggleButton[] toggleButtons;

	public tk2dUIProgressBar UIAttack;
	public tk2dUIProgressBar UIDefense;
	public tk2dUIProgressBar UIBlock;
	public tk2dUIProgressBar UICritical;
	public tk2dUIProgressBar UIDodge;

	public tk2dSprite UIHeroName;

	public void SelectChar(InfoStats.CLASS_TYPE charClass, tk2dUIToggleButton button)
	{
		if (mainClass == charClass) {
			button.IsOn = true;
			return;
		}

		foreach (var item in toggleButtons) {
			if (item != button)
				item.IsOn = false;
		}

		mainClass = charClass;

		//TODO: refactor these code
		GetClassStats ();
		FillClassInfoStats ();
	}

	void GetClassStats()
	{
		//TODO: return info stats of class
	}

	//TODO: pass info stats parameter
	void FillClassInfoStats()
	{
		UIAttack.Value = 1;
		UIDefense.Value = 1;
		UIBlock.Value = 1;
		UICritical.Value = 1;
		UIDodge.Value = 1;

		switch (mainClass) {
		case InfoStats.CLASS_TYPE.PALADIN:
			UIHeroName.SetSprite("Beast-Master");
			break;
		case InfoStats.CLASS_TYPE.ARCHER:
			UIHeroName.SetSprite("Text_Gunner");
			break;
		case InfoStats.CLASS_TYPE.ASSASSIN:
			UIHeroName.SetSprite("Text_Sorceress");
			break;
		}	
	}

	public void CreateMainCharacter()
	{
		//TODO: create character with mainClass
	}
}

