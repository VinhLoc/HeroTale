using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public int index = 0;

	// Use this for initialization
	void Start () {
//		ResourceMgr.LoadPrefab( ResourceMgr.PREFABS_TAG_CHARACTER );

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

//		Player player = AccountMgr.Instance.createNewAccount("BrokenHell");
//
//		Character @char = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "BrokenHell" );
//
//		player.PCharacters.ListCharacter.Add(@char);
//		player.PTactis.SlotsDeployment.TryAddToSlot( 0 , @char);
//
//
//		Player player2 = AccountMgr.Instance.createNewAccount("Devil");
//		
//		Character @char2 = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "Devil" );
//		
//		player2.PCharacters.ListCharacter.Add(@char2);
//		player2.PTactis.SlotsDeployment.TryAddToSlot( 0 , @char2);
//
//		Player.Save(player);
//		Player.Save(player2);
	}
}
