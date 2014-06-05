using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ResourceMgr.LoadPrefab( ResourceMgr.PREFABS_TAG_CHARACTER );

		StartCoroutine(Spam());
	}

	IEnumerator Spam () 
	{
		yield return new WaitForSeconds(1);

		GameObject pre = CharacterFactory.Instance.createNewCharacter( InfoStats.CLASS_TYPE.ASSASSIN , "BrokenHell");
		pre.transform.position = Vector3.zero;
	}
}
