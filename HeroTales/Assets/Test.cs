using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public int index = 0;

	// Use this for initialization
	void Start () {
		ResourceMgr.LoadPrefab( ResourceMgr.PREFABS_TAG_CHARACTER );

		StartCoroutine(Spam());
	}

	IEnumerator Spam () 
	{
		yield return new WaitForSeconds(1);

		Player player = AccountMgr.Instance.createNewAccount("BrokenHell");

		Character @char = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "BrokenHell" );

		player.PCharacters.ListCharacter.Add(@char);

		Player.Save(player);

		LevelCharacterController.Instance.NextLvExpMap.Add( new LevelExp(1 , 100) );
		LevelCharacterController.Instance.NextLvExpMap.Add(  new LevelExp(2 , 500) );
		LevelCharacterController.Instance.NextLvExpMap.Add(  new LevelExp(3 , 700) );

		LevelCharacterController.Save( );
	}
}
