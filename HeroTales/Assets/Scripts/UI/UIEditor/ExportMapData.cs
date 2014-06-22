using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExportMapData : MonoBehaviour
{
	public List<MapCharacters> _maps;

	void Start()
	{
		MapManager manager = MapManager.Instance;
		manager.ClearAllData ();
		foreach (var item in _maps) {
			manager.AddMapData(item._mapInfo);
		}
		manager.SaveAllMapData ();
	}
}

