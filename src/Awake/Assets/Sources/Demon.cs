using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour {

	public SpriteRenderer leftEye;
	public SpriteRenderer rightEye;
	public float inititalAwakeSpeed;

	float awakeSpeed = 0.0f;
	float awakePercent = 0.0f;

	void Start() {
		SetAlpha(0.0f);
	}

	void Update() {
		AwakeStep();
	}

	public void Wakeup() {
		awakeSpeed = inititalAwakeSpeed;
	}

	void AwakeStep() {
		awakePercent += awakeSpeed * Time.deltaTime;
		if ( awakePercent > 1.0f ) awakePercent = 1.0f;
		SetAlpha(awakePercent);
	}

	void SetAlpha(float a) {
		Color c = leftEye.color;
		c.a = a;
		leftEye.color = c;
		rightEye.color = c;
	}

}
