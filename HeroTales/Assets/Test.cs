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

		AccountMgr.Instance.createNewAccount("BrokenHell");

		Player player = PlayerMgr.Instance.CurrentPlayer;

		GameObject mainChar = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , player.PAccount.Username );

		player.PCharacters.Characters.Add(mainChar);
		GameObject @charInside;
		player.PTactis.SlotsDeployment.TryAddToSlot( index , mainChar , out @charInside );

		BattleController.Instance.AutoDeploy( player.PTactis.SlotsDeployment , null );
	}
}
