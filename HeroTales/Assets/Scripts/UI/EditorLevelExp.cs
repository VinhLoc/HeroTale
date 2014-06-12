using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorLevelExp : MonoBehaviour {

	[SerializeField]
	public List<LevelExp> ExperiencePointMap = new List<LevelExp>( );

	void Start ()
	{
		LevelExpMap map = LevelCharacterController.Instance.NextLvExpMap;
		map.Clear( );
		for( int i = 0 , count = this.ExperiencePointMap.Count ; i < count ; ++i )
		{
			map.Add( ExperiencePointMap[i] );
		}

		LevelCharacterController.Save();
	}
}
