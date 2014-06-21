using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml.Serialization;

public class FileMgr {

	public static bool Save ( object _object , Type type , string path , bool bForcePath = false )
	{
		try
		{
			string sDataFile = "";
			if( !bForcePath )
				sDataFile = System.IO.Path.Combine (Application.persistentDataPath, path);
			else
				sDataFile = Application.dataPath + "/" + path;
			
			Debug.Log(sDataFile);
			
			using (var fs = new FileStream( sDataFile , FileMode.OpenOrCreate )) {
				var serializer = new XmlSerializer (type);
				serializer.Serialize (fs, _object );
				
				fs.Close();
			}
		}
		catch(Exception e )
		{
			Debug.Log("Failed to save : " + e.Message);
			return false;
		}

		return true;
	}

	public static object Load ( Type type , string path , bool bForcePath = false )
	{
		object _object = null;
		try
		{
			string sDataFile = ""; 

			if( !bForcePath )
				sDataFile = System.IO.Path.Combine (Application.persistentDataPath, path);
			else
				sDataFile = Application.dataPath + "/" + path;
			
			Debug.Log(sDataFile);
			
			using (var fs = new FileStream( sDataFile , FileMode.Open )) {
				var serializer = new XmlSerializer (type);
				_object = serializer.Deserialize(fs);
				
				fs.Close();
			}
		}
		catch(Exception e )
		{
			Debug.Log("Failed to load : " + e.Message);
			return null;
		}
		
		return _object;
	}
}
