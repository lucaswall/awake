using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour {

	public SpriteRenderer leftEye;
	public SpriteRenderer rightEye;
	public float inititalAwakeSpeed;
	public AudioSource audioSource;
	public AudioClip sigilSound;

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

	public void PushBack() {
		audioSource.PlayOneShot(sigilSound);
		awakeSpeed = 0.0f;
		awakePercent = 0.0f;
		SetAlpha(awakePercent);
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
