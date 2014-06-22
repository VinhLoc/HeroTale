using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapCharacters : MonoBehaviour
{
	public List<Character> _charactes { get; private set; }
	public MapInfo _mapInfo { get; private set; }

	void Awake()
	{
		_charactes = new List<Character> ();
		MapCharacterInfo[] characters = GetComponents<MapCharacterInfo> ();

		foreach (var item in characters) {
			Character character = new Character();
			character.PInfoStats = item.Info;
			character.PPointStats = item.PointInfo;
			character.PCombatStats = item.CombatInfo;
			character.SpriteIdTag = item.SpriteIdTag;

			_charactes.Add(character);
		}

		_mapInfo = new MapInfo ();
		_mapInfo._enemies = _charactes;
		_mapInfo._status = TokenStatus.TokenCompleted;
	}
}