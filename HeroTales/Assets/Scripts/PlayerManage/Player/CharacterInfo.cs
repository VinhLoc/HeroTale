using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInfo {
	
	public List<GameObject> Characters = new List<GameObject>();

	public PointBase OpenCharacter = new PointBase();

	public void initialize ( int maxCharacter , int currentChar )
	{
		this.OpenCharacter.Max = maxCharacter;
		this.OpenCharacter.Current = currentChar;
	}
}
