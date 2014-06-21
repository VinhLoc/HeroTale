using UnityEngine;
using System.Collections;

public class UICharacter : MonoBehaviour {

	public int SlotIndex;

	Transform transformCache;
	tk2dBaseSprite sprite;
	tk2dBaseSprite shadow;

	Vector3 ScaleLeft = new Vector3( -1 , 1 , 1 );

	void Awake ( )
	{
		transformCache = transform;
		sprite = transformCache.Find("ScaleObject/Sprite").GetComponent<tk2dBaseSprite>();
		shadow = transformCache.Find("ScaleObject/Shadow").GetComponent<tk2dBaseSprite>();
	}

	public void Init ( int slotIndex , Character character , BattleController.DEPLOY_TYPE deployType )
	{
		if( sprite == null )
			return;

		if( deployType == BattleController.DEPLOY_TYPE.RIGHT )
		{
			sprite.transform.localScale = ScaleLeft;
			shadow.transform.localScale = ScaleLeft;
		}

		this.SlotIndex = slotIndex;

		ConstantValue.SpriteIdLinker linker = ConstantValue.GetCollectionData( character.SpriteIdTag );

		tk2dSpriteCollectionData data = linker.GetCollectionData();

		sprite.SetSprite( data , linker.SpriteId );
		shadow.SetSprite( data , linker.ShadowId );
	}
}
