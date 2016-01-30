using UnityEngine;
using System.Collections;

public class EnergyHolder : MonoBehaviour {

	public SpriteRenderer holderIndicator;
	public float energyDisipation;
	public float pushBackCost;

	float energy;
	int energyProperty;

	void Awake() {
		energyProperty = Shader.PropertyToID("_Energy");
	}

	void Start() {
		SetEnergy(0.0f);
	}

	void Update() {
		if ( energy > 0.0f ) {
			energy -= energyDisipation * Time.deltaTime;
			if ( energy < 0.0f ) energy = 0.0f;
			SetEnergy(energy);
		}
	}

	void SetEnergy(float e) {
		energy = e;
		holderIndicator.material.SetFloat(energyProperty, energy);
	}

	public void AddEnergy(float inc) {
		energy += inc;
		if ( energy > 1.0f ) energy = 1.0f;
		if ( energy < 0.0f ) energy = 0.0f;
		SetEnergy(energy);
		if ( energy == 1.0f ) GameEvents.EnergyHolderFull();
	}

	public bool IsFull() {
		return energy >= 1.0f;
	}

	public void DrainPushBackEnergy() {
		energy -= pushBackCost;
		if ( energy <= 0.0f ) energy = 0.0f;
		SetEnergy(energy);
	}

}
