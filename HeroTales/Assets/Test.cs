using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public int index = 0;

	// Use this for initialization
	void Start () {
//		ResourceMgr.LoadPrefab( ResourceMgr.PREFABS_TAG_CHARACTER );

		ResourceMgr.LoadResource( ConstantValue.RES_TYPE_SPRITE_COLLECTION_DATA , 
		                         ConstantValue.PREFABS_TAG_CHARACTER_COLLECTION );

//		ResourceMgr.LoadResource( ConstantValue.RES_TYPE_GAME_OBJECT ,
//		                         ConstantValue.PREFABS_TAG_BATTLE );

//		ResourceMgr.LoadResource( ConstantValue.RES_TYPE_TEXT , ConstantValue.XML_TEMPLATE_CHARACTER );
//
//		TextAsset ss = ResourceMgr.GetResource( ConstantValue.RES_TYPE_TEXT , 
//		                                    ConstantValue.XML_TEMPLATE_CHARACTER ,
//		                                    ConstantValue.TAG_TEMPLATE_ASS_0 ) as UnityEngine.TextAsset;
//
//		Debug.Log (ss.ToString());

		StartCoroutine(Spam());
	}

	IEnumerator Spam () 
	{
		yield return new WaitForSeconds(1);

		Player broken = Player.Load(0);
		Player devil  = Player.Load(1);

		BattleController.Instance.CalculateResult( broken.PTactis.SlotsDeployment ,
		                                          devil.PTactis.SlotsDeployment );


		UIBattleController.Instance.Deploy( broken.PTactis.SlotsDeployment , 
		                                   devil.PTactis.SlotsDeployment );

//		Player player = AccountMgr.Instance.createNewAccount("BrokenHell");
//
//		Character @char = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "BrokenHell" );
//		Character @char2 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.PALADIN , "BrokenHell2");
//		Character @char3 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ARCHER , "BrokenHell3");
//
//		player.PCharacters.ListCharacter.Add(@char);
//		player.PCharacters.ListCharacter.Add(@char2);
//		player.PCharacters.ListCharacter.Add(@char3);
//		player.PTactis.SlotsDeployment.TryAddToSlot( 0 , @char);
//		player.PTactis.SlotsDeployment.TryAddToSlot( 2 , @char2);
//		player.PTactis.SlotsDeployment.TryAddToSlot( 4 , @char3);
//
//
//		Player player2 = AccountMgr.Instance.createNewAccount("Devil");
//		
//		Character @char4 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "Devil" );
//		Character @char5 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "Devil2" );
//		Character @char6 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ARCHER , "Devil3" );
//		
//		player2.PCharacters.ListCharacter.Add(@char4);
//		player2.PCharacters.ListCharacter.Add(@char5);
//		player2.PCharacters.ListCharacter.Add(@char6);
//		player2.PTactis.SlotsDeployment.TryAddToSlot( 0 , @char4);
//		player2.PTactis.SlotsDeployment.TryAddToSlot( 6 , @char5);
//		player2.PTactis.SlotsDeployment.TryAddToSlot( 8 , @char6);
//
//		Player.Save(player);
//		Player.Save(player2);
	}
}
