using UnityEngine;
using System.Collections;

public class EnergyHolder : MonoBehaviour {

	public SpriteRenderer holderIndicator;

	float energy;

	void Start() {
		SetAlpha(0.0f);
	}

	void SetAlpha(float a) {
		Color c = holderIndicator.color;
		c.a = a;
		holderIndicator.color = c;
	}

	public void AddEnergy(float inc) {
		energy += inc;
		if ( energy > 1.0f ) energy = 1.0f;
		SetAlpha(energy);
	}

	public bool IsFull() {
		return energy >= 1.0f;
	}

	public void DrainAllEnergy() {
		energy = 0;
		SetAlpha(energy);
	}

}
