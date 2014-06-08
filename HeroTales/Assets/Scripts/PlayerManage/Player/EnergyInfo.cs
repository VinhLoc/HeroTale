using UnityEngine;
using System.Collections;

public class EnergyInfo{

	public PointBase Energy = new PointBase();

	public void initialize ( int maxEnergy )
	{
		this.Energy.Max = maxEnergy;
		this.Energy.Current = maxEnergy;
	}
}
