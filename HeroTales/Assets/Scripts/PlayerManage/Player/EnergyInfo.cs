using UnityEngine;
using System.Collections;

public class EnergyInfo{

	public PointBase Energy;

	public void initialize ( int maxEnergy )
	{
		this.Energy.Max = maxEnergy;
		this.Energy.Current = maxEnergy;
	}
}
