using UnityEngine;
using System.Collections;

public class SelectCharControl : MonoBehaviour
{
	public InfoStats.CLASS_TYPE mainClass { get; set; }

	public tk2dUIToggleButton[] toggleButtons;

	public tk2dSprite charSprite;

	public tk2dUIProgressBar UIAttack;
	public tk2dUIProgressBar UIDefense;
	public tk2dUIProgressBar UIBlock;
	public tk2dUIProgressBar UICritical;
	public tk2dUIProgressBar UIDodge;

	public tk2dSprite UIHeroName;

	void Start()
	{
		mainClass = InfoStats.CLASS_TYPE.PALADIN;
		Character info = CharacterFactory.Instance.getCharacterTemplateByClass (mainClass);
		FillClassInfoStats (info.PCombatStats);
	}

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
		Character info = CharacterFactory.Instance.getCharacterTemplateByClass (mainClass);
		FillClassInfoStats (info.PCombatStats);
	}

	//TODO: pass info stats parameter
	void FillClassInfoStats(CombatStats info)
	{
		float baseValue = ConstantValue.COMBAT_BASE_VALUE;

		UIAttack.Value = info.Attack / baseValue;
		UIDefense.Value = info.Defend / baseValue;
		//UIBlock.Value = 1;
		UICritical.Value = info.Critical / baseValue;
		UIDodge.Value = info.Dodge / baseValue;

		switch (mainClass) {
		case InfoStats.CLASS_TYPE.PALADIN:
			UIHeroName.SetSprite("Beast-Master");
			charSprite.SetSprite("beastmaster");
			break;
		case InfoStats.CLASS_TYPE.ARCHER:
			UIHeroName.SetSprite("Text_Gunner");
			charSprite.SetSprite("gunner");
			break;
		case InfoStats.CLASS_TYPE.ASSASSIN:
			UIHeroName.SetSprite("Text_Sorceress");
			charSprite.SetSprite("sorceress");
			break;
		}	
	}

	public void CreateMainCharacter()
	{
		//TODO: create character with mainClass
	}
}

