using UnityEngine;
using System.Collections;

public class BeatController : MonoBehaviour {

	public AudioSource beatSource;
	public BeatIndicator beatIndicator;
	public float[] times;
	public float loopTime;
	public EnergyHolder energyHolder;
	public float energyTransferFactor;
	public float energyBeatCost;

	float totalTimes = 0.0f;
	bool beatActive = true;

	void Start() {
		StartCoroutine(PlayOneBeat());
		for ( int i = 0; i < times.Length; i++ ) {
			totalTimes += times[i];
		}
	}

	void Update() {
		if ( Input.GetMouseButtonDown(0) && beatActive ) {
			if ( IsValidTouch(Input.mousePosition) ) {
				ProcessTouch();
			}
		}
	}

	IEnumerator PlayOneBeat() {
		beatSource.Play();
		for ( int i = 0; i < times.Length; i++ ) {
			yield return new WaitForSeconds(times[i]);
			beatIndicator.Beat();
		}
		yield return new WaitForSeconds(loopTime - totalTimes);
		if ( beatActive ) StartCoroutine(PlayOneBeat());
	}

	bool IsValidTouch(Vector2 position) {
		Vector2 vpPos = Camera.main.ScreenToViewportPoint(position);
		return vpPos.x <= 0.5f && vpPos.y <= 0.5f;
	}

	void ProcessTouch() {
		float energyInc = beatIndicator.GetEnergy() - energyBeatCost;
		energyHolder.AddEnergy(energyInc * energyTransferFactor);
	}

	public void StopBeat() {
		beatActive = false;
		beatSource.Stop();
	}

}
