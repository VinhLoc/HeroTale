using UnityEngine;
using System.Collections;

public class SelectCharControl : MonoBehaviour
{
	public InfoStats.CLASS_TYPE mainClass { get; set; }

	public tk2dUIProgressBar UIAttack;
	public tk2dUIProgressBar UIDefense;
	public tk2dUIProgressBar UIBlock;
	public tk2dUIProgressBar UICritical;
	public tk2dUIProgressBar UIDodge;

	public tk2dTextMesh UIHeroName;

	public void SelectChar(InfoStats.CLASS_TYPE charClass)
	{
		if (mainClass == charClass)
			return;

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

		UIHeroName.text = "PALADIN";
	}

	public void CreateMainCharacter()
	{
		//TODO: create character with mainClass
	}
}

