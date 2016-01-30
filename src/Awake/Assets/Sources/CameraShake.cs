using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	Vector3 originalPosition;
	float shakeTime;
	float shakeAmplitude;

	void Start() {
		originalPosition = transform.position;
	}

	void Update() {
		if ( shakeTime > 0.0f ) {
			shakeTime -= Time.deltaTime;
			transform.position = originalPosition + (Vector3) Random.insideUnitCircle * shakeAmplitude;
			if ( shakeTime <= 0.0f ) {
				shakeTime = 0.0f;
				transform.position = originalPosition;
			}
		}
	}

	public void Shake(float time, float amplitude) {
		shakeTime = time;
		shakeAmplitude = amplitude;
	}

}
