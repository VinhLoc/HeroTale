using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnergyInfo{

	public PointBase Energy = new PointBase();

	public EnergyInfo()
	{

	}

	public void initialize ( int maxEnergy )
	{
		this.Energy.Max = maxEnergy;
		this.Energy.Current = maxEnergy;
	}
}
