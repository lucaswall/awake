using UnityEngine;
using System.Collections;

public class EnergyHolder : MonoBehaviour {

	public SpriteRenderer holderIndicator;
	public float energyDisipation;
	public float pushBackCost;

	float energy;

	void Start() {
		SetAlpha(0.0f);
	}

	void Update() {
		if ( energy > 0.0f ) {
			energy -= energyDisipation * Time.deltaTime;
			if ( energy < 0.0f ) energy = 0.0f;
			SetAlpha(energy);
		}
	}

	void SetAlpha(float a) {
		Color c = holderIndicator.color;
		c.a = a;
		holderIndicator.color = c;
	}

	public void AddEnergy(float inc) {
		energy += inc;
		if ( energy > 1.0f ) energy = 1.0f;
		if ( energy < 0.0f ) energy = 0.0f;
		SetAlpha(energy);
		if ( energy == 1.0f ) GameEvents.EnergyHolderFull();
	}

	public bool IsFull() {
		return energy >= 1.0f;
	}

	public void DrainPushBackEnergy() {
		energy -= pushBackCost;
		if ( energy <= 0.0f ) energy = 0.0f;
		SetAlpha(energy);
	}

}
