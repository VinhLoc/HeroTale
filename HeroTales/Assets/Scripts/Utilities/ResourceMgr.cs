using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResourceMgr {

	public class ResourceObject : Dictionary<string,Dictionary<string,UnityEngine.Object>>
	{
		public void AddTo ( string tagGroup , string tagResource , UnityEngine.Object resObj )
		{
			Dictionary<string,UnityEngine.Object> dicObj;
			
			if( this.TryGetValue( tagGroup , out dicObj ) )
			{
				dicObj.Add( tagResource , resObj );
			}
			else
			{
				Dictionary<string,UnityEngine.Object> map = new Dictionary<string, UnityEngine.Object>();
				map.Add(tagResource,resObj);
				
				this.Add( tagGroup , map );
			}
		}

		public UnityEngine.Object Get ( string tagGroup , string tagResource )
		{
			Dictionary<string,UnityEngine.Object> dicObj;
			UnityEngine.Object obj;

			if( this.TryGetValue( tagGroup , out dicObj ) )
			{
				if( dicObj.TryGetValue( tagResource , out obj ) )
				{
					return obj;
				}
			}

			return null;
		}
	}

	public class ResourceDictionary : Dictionary<string,Dictionary<string,string>>
	{
		public ResourceDictionary(Type resourceType)
		{
			this.ResourceType = resourceType;
		}

		public Type ResourceType;
	}
	
	// This is the TAG => Object
	public static Dictionary<string, ResourceObject> GameResourcesLoaded = new Dictionary<string, ResourceObject>();


	private static Dictionary<string , ResourceDictionary> ResourcesMap = new Dictionary<string, ResourceDictionary>()
	{
		{
			ConstantValue.RES_TYPE_GAME_OBJECT , new ResourceDictionary(typeof(GameObject))
			{
				{
					ConstantValue.PREFABS_TAG_BATTLE , new Dictionary<string,string>()
					{
						{ ConstantValue.TAG_BATTLE_MOVE , @"Battle/Move" }
					}
				}
			}
		},
		{
			ConstantValue.RES_TYPE_TEXT , new ResourceDictionary(typeof(TextAsset))
			{
//				{
//					ConstantValue.XML_TEMPLATE_CHARACTER , new Dictionary<string,string >()
//					{
//						{ ConstantValue.TAG_TEMPLATE_PAL_0 , @"MainChar/Char_Template_Pal" },
//						{ ConstantValue.TAG_TEMPLATE_ASS_0 , @"MainChar/Char_Template_Ass" },
//						{ ConstantValue.TAG_TEMPLATE_ARC_0 , @"MainChar/Char_Template_Arc" }
//					}
//				}
			}
		}
	};

	public static void LoadResource ( string resType , params string[] resGroups )
	{
		ResourceDictionary resDic = null;
		ResourcesMap.TryGetValue( resType , out resDic );
		if( resDic != null )
		{
			Dictionary<string,string> listResources = null;
			for( int i = 0 , count = resGroups.Length ; i < count ; ++i )
			{
				if( resDic.TryGetValue( resGroups[i] , out listResources ) )
				{
					Load( resType, resGroups[i] , listResources );
				}
			}
		}
	}

	public static UnityEngine.Object GetResource( string resType , string resGroup , string resTag )
	{
		ResourceObject resObj;

		if( GameResourcesLoaded.TryGetValue( resType , out resObj ) )
		{
			return resObj.Get( resGroup , resTag );
		}

		return null;
	}
 
	private static void Load ( string resType , string resGroup , Dictionary<string,string> dicResource )
	{
		UnityEngine.Object res;
		foreach( var pair in dicResource )
		{
			res = Resources.Load(pair.Value);

			if( res != null )
			{
				AddToGameResources( resType , resGroup , pair.Key , res );
			}
		}
	}

	private static void AddToGameResources ( string resType , string resGroup , string resTag , UnityEngine.Object res )
	{
		ResourceObject resObj;

		if( GameResourcesLoaded.TryGetValue( resType , out resObj ) )
		{
			resObj.AddTo( resGroup , resTag , res );
		}
		else
		{
			resObj = new ResourceObject( );
			resObj.AddTo( resGroup , resTag , res );

			GameResourcesLoaded.Add( resType , resObj );
		}
	}
}
