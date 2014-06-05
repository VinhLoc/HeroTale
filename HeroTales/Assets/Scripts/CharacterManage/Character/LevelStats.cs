using UnityEngine;
using System.Collections;

public class LevelStats : MonoBehaviour {

	public int Level;
	[SerializeField]
	public PointBase Exp;

	public void initialize ( int level , int nextLVExp )
	{
		this.Level = level;
		this.Exp.Max = nextLVExp;
		this.Exp.Current = 0;
	}
}
