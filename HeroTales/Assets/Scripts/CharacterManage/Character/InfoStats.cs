﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class InfoStats {

	public enum CLASS_TYPE
	{
		NONE = -1,
		PALADIN,
		ASSASSIN,
		ARCHER
	}

	public InfoStats ( )
	{

	}

	public ulong UID;
	public string Name;
	public string Description;
	public CLASS_TYPE ClassType;

	public void initialize ( ulong uid , string name , string desc , InfoStats.CLASS_TYPE classType )
	{
		this.UID = uid;
		this.Name = name;
		this.Description = desc;
		this.ClassType = classType;
	}
}
