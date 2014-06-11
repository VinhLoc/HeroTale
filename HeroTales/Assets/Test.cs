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

		for( int i = 0 ; i < 9 ; ++i )
		{
			GameObject mainChar = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , player.PAccount.Username + i.ToString() );
			player.PCharacters.Characters.Add(mainChar);
			
			GameObject @charInside;
			player.PTactis.SlotsDeployment.TryAddToSlot( i , mainChar , out @charInside );
		}

		Player boss = AccountMgr.Instance.createNewAccount("FinalBoss");

		for( int i = 0 ; i < 9 ; ++i )
		{
			GameObject mainChar = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ARCHER , boss.PAccount.Username + i.ToString() );
			boss.PCharacters.Characters.Add(mainChar);
			
			GameObject @charInside;
			boss.PTactis.SlotsDeployment.TryAddToSlot( i , mainChar , out @charInside );
		}


		BattleController.Instance.AutoDeploy( player.PTactis.SlotsDeployment , boss.PTactis.SlotsDeployment );
	}
}
