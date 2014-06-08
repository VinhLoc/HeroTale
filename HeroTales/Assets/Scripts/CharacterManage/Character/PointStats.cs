using UnityEngine;
using System.Collections;

public class PointStats : MonoBehaviour {

	[SerializeField]
	public PointBase Health = new PointBase();
	[SerializeField]
	public PointBase Rage = new PointBase();

	public void initialize ( int maxHp , int maxRage )
	{
		this.Health.Max = maxHp;
		this.Health.Current = maxHp;
		this.Rage.Max = maxRage;
		this.Rage.Current = 0;
	}
}
