using UnityEngine;
using System.Collections;

public class ButtonSelectChar : MonoBehaviour
{
	public InfoStats.CLASS_TYPE buttonClass;
	public SelectCharControl mainControl;

	public void OnClick()
	{
		mainControl.SelectChar (buttonClass);
	}
}

