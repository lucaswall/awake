using UnityEngine;
using System.Collections;

public class GameEvents {

	public static event EnergyHolderFullAction OnEnergyHolderFull;

	public delegate void EnergyHolderFullAction();

	public static void EnergyHolderFull() {
		if ( OnEnergyHolderFull != null ) OnEnergyHolderFull();
	}

}
