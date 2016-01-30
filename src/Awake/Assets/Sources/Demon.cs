using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour {

	public SpriteRenderer leftEye;
	public SpriteRenderer rightEye;
	public float inititalAwakeSpeed;
	public AudioSource audioSource;
	public AudioClip sigilSound;
	public SpriteRenderer sigilImage;
	public float sigilFadeSpeed;
	public float demonFadeSpeed;

	float awakeSpeed = 0.0f;
	float awakePercent = 0.0f;

	void Start() {
		SetAlpha(0.0f);
		SetSigilAlpha(0.0f);
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
		SetSigilAlpha(1.0f);
		StartCoroutine(FadeDemonAway());
		StartCoroutine(FadeSigilAway());
	}

	IEnumerator FadeDemonAway() {
		while ( awakePercent > 0.0f ) {
			awakePercent -= demonFadeSpeed * Time.deltaTime;
			SetAlpha(awakePercent);
			yield return null;
		}
		awakePercent = 0.0f;
		SetAlpha(awakePercent);
	}

	IEnumerator FadeSigilAway() {
		float alpha = 1.0f;
		while ( alpha > 0.0f ) {
			alpha -= sigilFadeSpeed * Time.deltaTime;
			SetSigilAlpha(Mathf.Sin(alpha * Mathf.PI * 0.5f));
			yield return null;
		}
		SetSigilAlpha(0.0f);
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

	void SetSigilAlpha(float a) {
		Color c = sigilImage.color;
		c.a = a;
		sigilImage.color = c;
	}

}
