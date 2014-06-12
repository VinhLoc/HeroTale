using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class Characters : List<Character>
{
	public Characters ()
	{

	}
}

[System.Serializable]
public class CharacterInfo {

//	[XmlArray("Characters")]
//	[XmlArrayItem("GameObject")]
//	public GameObject[] Characters = new GameObject[10];

	public Characters ListCharacter = new Characters();

	public PointBase OpenCharacter = new PointBase();

	public CharacterInfo()
	{

	}

	public void initialize ( int maxCharacter , int currentChar )
	{
		this.OpenCharacter.Max = maxCharacter;
		this.OpenCharacter.Current = currentChar;
	}
}
