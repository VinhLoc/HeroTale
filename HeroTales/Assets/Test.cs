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

		Player player = AccountMgr.Instance.createNewAccount("BrokenHell");

		Character @char = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "BrokenHell" );

		player.PCharacters.ListCharacter.Add(@char);

		Player.Save(player);
	}
}
