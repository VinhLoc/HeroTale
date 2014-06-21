using UnityEngine;
using System.Collections;

public class ButtonSelectChar : MonoBehaviour
{
	public InfoStats.CLASS_TYPE buttonClass;
	public SelectCharControl mainControl;

	private tk2dUIToggleButton toggle;

	void Awake()
	{
		toggle = GetComponent<tk2dUIToggleButton> ();
	}

	public void OnClick()
	{
		mainControl.SelectChar (buttonClass, toggle);
	}
}

