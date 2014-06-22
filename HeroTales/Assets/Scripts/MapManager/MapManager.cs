using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class MapManager
{
	#region Singleton
	private static MapManager _instance = null;
	public  static MapManager Instance {
		get {
			if (null == _instance)
			{
				_instance = new MapManager();
				_instance.LoadAllMapData();
			}
			return _instance;
		}	
	}
	#endregion

	private List<MapInfo> _maps;

	private MapManager()
	{
//		_maps = new List<MapInfo> ();
	}

	/// <summary>
	/// Loads all map data.
	/// </summary>
	public void LoadAllMapData()
	{
		_maps = (List<MapInfo>) FileMgr.Load (typeof(List<MapInfo>), ConstantValue.FILE_MAPDATA);
		if (null == _maps)
			_maps = new List<MapInfo> ();

		Debug.Log ("Map size = " + _maps.Count);
	}

	/// <summary>
	/// Saves all map data.
	/// </summary>
	public void SaveAllMapData()
	{
		FileMgr.Save(_maps, typeof(List<MapInfo>), ConstantValue.FILE_MAPDATA);
	}

	/// <summary>
	/// Get MapInfo with id.
	/// </summary>
	/// <returns>The map.</returns>
	/// <param name="id">map id</param>
	public MapInfo GetMap(int id)
	{
		return _maps[id];
	}

	/// <summary>
	/// Clears all data.
	/// </summary>
	public void ClearAllData()
	{
		_maps.Clear ();
	}

	/// <summary>
	/// Updates the map status.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="status">Status.</param>
	public void UpdateMapStatus(int id, TokenStatus status)
	{
		var map = _maps [id];
		if (null != map) {
			map._status = status;
		}
	}

	/// <summary>
	/// Adds the map data.
	/// </summary>
	/// <param name="map">Map.</param>
	public void AddMapData(MapInfo map)
	{
		_maps.Add (map);
	}
}
