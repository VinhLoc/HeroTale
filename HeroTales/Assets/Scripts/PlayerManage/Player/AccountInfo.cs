using UnityEngine;
using System.Collections;

public class AccountInfo {

	private ulong _userID;
	private string _username;

	public ulong UserID 
	{ 
		get
		{
			return _userID;
		}
		set
		{
			if( value <= 0 )
				return;

			_userID = value;
		}
	}
	public string Username
	{
		get
		{
			return _username;
		}
		set
		{
			if( value == "" )
				return;

			_username = value;
		}
	}

	public void initialize ( ulong uid , string username )
	{
		this.UserID = uid;
		this.Username = username;
	}
}
