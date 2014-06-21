using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelStats {

	public int Level;
	[SerializeField]
	public PointBase Exp = new PointBase();

	public LevelStats ()
	{

	}

	public void initialize ( int level , int nextLVExp )
	{
		this.Level = level;
		this.Exp.Max = nextLVExp;
		this.Exp.Current = 0;
	}
}
