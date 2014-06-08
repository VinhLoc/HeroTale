using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceMgr {

	public class ResourceObject{

		public string Path;
		public object RObject;
	}

	public const string PREFABS_TAG_CHARACTER = "Tag_Pre_Character";
	public const string PREFABS_TAG_BATTLE = "Tag_Pre_Battle";


	// Main character tags
	public const string TAG_MAIN_PAL_0 = "Tag_Main_Pal_0";
	public const string TAG_MAIN_ASS_0 = "Tag_Main_Ass_0";
	public const string TAG_MAIN_ARC_0 = "Tag_Main_Arc_0";

	// Battle resouce tags
	public const string TAG_BATTLE_MOVE = "Tag_Battle_Move";

	// This is the TAG => Object
	public static Dictionary<string, ResourceObject> GamePrefabs = new Dictionary<string, ResourceObject>();

	private static Dictionary<string,Dictionary<string,string>> dicResourcePrefabs = new Dictionary<string, Dictionary<string,string>>()
	{
		{ PREFABS_TAG_CHARACTER , new Dictionary<string,string>(){ 
				{ TAG_MAIN_PAL_0 , @"MainChar/Char_Main_Pal_0" },
				{ TAG_MAIN_ASS_0 , @"MainChar/Char_Main_Ass_0" },
				{ TAG_MAIN_ARC_0 , @"MainChar/Char_Main_Arc_0" } }
		},
		{ PREFABS_TAG_BATTLE , new Dictionary<string,string>(){
				{ TAG_BATTLE_MOVE , @"Battle/Move" } }
		}
	};

	public static GameObject GetPrefab ( string tag )
	{
		ResourceObject resObj = null;

		if( GamePrefabs.TryGetValue( tag , out resObj ) )
		{
			return resObj.RObject as GameObject;
		}

		return null;
	}

	public static void LoadPrefab ( params string[] tags )
	{
		Dictionary<string,string> listPrefabsPaths = null;
		for( int i = 0 , count = tags.Length ; i < count ; ++i )
		{
			if( dicResourcePrefabs.TryGetValue( tags[i] , out listPrefabsPaths ) )
			{
				LoadPrefab( listPrefabsPaths );
			}
		}
	}


	private static void LoadPrefab ( Dictionary<string,string> dicPaths )
	{
		GameObject pre = null;

		foreach( KeyValuePair<string,string> pair in dicPaths )
		{
			pre = Resources.Load( pair.Value ) as GameObject;

			if( pre )
			{
				GamePrefabs.Add( pair.Key , new ResourceObject() { Path = pair.Value , RObject = pre } );
			}
		}
	}
}
