using UnityEngine;
using System.Collections;

public class ConstantValue {

	public const string RES_TYPE_GAME_OBJECT = "GameObject";
	public const string RES_TYPE_TEXT ="Text";

	// Prefabs tags
	public const string PREFABS_TAG_CHARACTER = "Tag_Pre_Character";
	public const string PREFABS_TAG_BATTLE = "Tag_Pre_Battle";

	// Xml character template tags
	public const string XML_TEMPLATE_CHARACTER = "Xml_Template_Character";
	
	
	// Main character tags
	public const string TAG_TEMPLATE_PAL_0 = "Tag_Char_Template_Pal";
	public const string TAG_TEMPLATE_ASS_0 = "Tag_Char_Template_Ass";
	public const string TAG_TEMPLATE_ARC_0 = "Tag_Char_Template_Arc";
	
	// Battle resouce tags
	public const string TAG_BATTLE_MOVE = "Tag_Battle_Move";
	
	// Files //
	public const string FILE_NAME_PLAYER = "Player{0}.xml";
	public const string FILE_NAME_CHARACTER = "Character{0}.xm";
	public const string FILE_NAME_NEXTLVEXP = "NextLevelExp.xml";

	public const string FILE_TEMPALTE_PAL = "Resources/MainChar/Char_Template_Pal.xml";
	public const string FILE_TEMPALTE_ASS = "Resources/MainChar/Char_Template_Ass.xml";
	public const string FILE_TEMPALTE_ARC = "Resources/MainChar/Char_Template_Arc.xml";
	
	// PlayerPrefs //
	public const string PREFS_LAST_USER_ID = "UserID";
}
