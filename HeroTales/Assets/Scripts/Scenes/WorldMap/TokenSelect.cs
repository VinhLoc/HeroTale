using UnityEngine;
using System.Collections;

public class TokenSelect : MonoBehaviour
{
	public int map_id;
	public tk2dSprite title;

	private tk2dSprite tokenSprite;

	void Awake()
	{
		tokenSprite = GetComponent<tk2dSprite> ();
	}

	void Start()
	{
		MapInfo info = MapManager.Instance.GetMap (map_id);
		status = info._status;
	}

	public TokenStatus status {
		get 
		{
			return _status;
		}

		set 
		{
			_status = value;
			OnChangeStatus();
		}
	}
	private TokenStatus _status;

	private void OnChangeStatus()
	{
		switch (status) {
		case TokenStatus.TokenBlocked:
			title.gameObject.SetActive(false);
			tokenSprite.SetSprite("block_map");
			break;

		case TokenStatus.TokenCompleted:
			title.gameObject.SetActive(true);
			title.SetSprite("compete_text");
			tokenSprite.SetSprite("map_new");
			break;

		case TokenStatus.TokenOpened:
			title.gameObject.SetActive(true);
			title.SetSprite("battle_text");
			tokenSprite.SetSprite("battle_map");
			break;
		}	
	}

	public void OnClick()
	{
		if (status != TokenStatus.TokenBlocked) {
			// TODO: change to batte scene with map_id, don't change status here
			status = TokenStatus.TokenCompleted;
		}
	}
}

