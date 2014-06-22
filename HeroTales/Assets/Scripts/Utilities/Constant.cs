using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum SpriteIdLinkerTag
{
	Paladin,
	Assasin,
	Archer
}

[System.Serializable]
public enum TokenStatus
{
	TokenBlocked,
	TokenOpened,
	TokenCompleted
}

public class ConstantValue {

	public const string RES_TYPE_GAME_OBJECT = "GameObject";
	public const string RES_TYPE_TEXT ="Text";
	public const string RES_TYPE_SPRITE_COLLECTION_DATA = "SpriteCollectionData";

	// Prefabs tags
	public const string PREFABS_TAG_CHARACTER = "Tag_Pre_Character";
	public const string PREFABS_TAG_BATTLE = "Tag_Pre_Battle";
	public const string PREFABS_TAG_ATK_NORMAL_EFX = "Tag_Pre_Atk_Normal_Efx";
	public const string PREFABS_TAG_CHARACTER_COLLECTION = "Tag_Sprite_Collection_Character";

	// Xml character template tags
	public const string XML_TEMPLATE_CHARACTER = "Xml_Template_Character";
	
	
	// Main character tags
	public const string TAG_TEMPLATE_PAL_0 = "Tag_Char_Template_Pal";
	public const string TAG_TEMPLATE_ASS_0 = "Tag_Char_Template_Ass";
	public const string TAG_TEMPLATE_ARC_0 = "Tag_Char_Template_Arc";
	
	// Battle resouce tags
	public const string TAG_BATTLE_MOVE = "Tag_Battle_Move";
	public const string TAG_CHAR_COLLECTION_0 = "Tag_Char_Collection_0";
	
	// Files //
	public const string FILE_NAME_PLAYER = "Player{0}.xml";
	public const string FILE_NAME_CHARACTER = "Character{0}.xm";
	public const string FILE_NAME_NEXTLVEXP = "Data/NextLevelExp.xml";

	public const string FILE_TEMPALTE_PAL = "Data/Char_Template_Pal.xml";
	public const string FILE_TEMPALTE_ASS = "Data/Char_Template_Ass.xml";
	public const string FILE_TEMPALTE_ARC = "Data/Char_Template_Arc.xml";

	public const string FILE_MAPDATA = "Data/Map.xml";
	
	// PlayerPrefs //
	public const string PREFS_LAST_USER_ID = "UserID";

	public const float COMBAT_BASE_VALUE = 30f;

	// Sprite ID Map //
	public class SpriteIdLinker
	{
		public string CollectionTag;
		public int SpriteId;
		public int ShadowId;

		public tk2dSpriteCollectionData GetCollectionData () 
		{
			return ResourceMgr.GetResource( ConstantValue.RES_TYPE_SPRITE_COLLECTION_DATA , 
			                               ConstantValue.PREFABS_TAG_CHARACTER_COLLECTION ,
			                               CollectionTag ) as tk2dSpriteCollectionData;
		}
	}

	private static Dictionary<SpriteIdLinkerTag , SpriteIdLinker> SpriteIdMap = new Dictionary<SpriteIdLinkerTag, SpriteIdLinker>( )
	{
		{
			SpriteIdLinkerTag.Paladin , new SpriteIdLinker() { CollectionTag = ConstantValue.TAG_CHAR_COLLECTION_0 , SpriteId = 0 , ShadowId = 3 }
		},
		{
			SpriteIdLinkerTag.Assasin , new SpriteIdLinker() { CollectionTag = ConstantValue.TAG_CHAR_COLLECTION_0 , SpriteId = 2 , ShadowId = 5 }
		},
		{
			SpriteIdLinkerTag.Archer , new SpriteIdLinker() { CollectionTag = ConstantValue.TAG_CHAR_COLLECTION_0 , SpriteId = 1 , ShadowId = 4 }
		}
	};

	public static SpriteIdLinker GetCollectionData ( SpriteIdLinkerTag tag )
	{
		SpriteIdLinker linker;

		if( SpriteIdMap.TryGetValue( tag , out linker ) )
		{
			return linker;
		}

		return null;
	}
}
