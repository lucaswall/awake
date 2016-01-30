using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour {

	public SpriteRenderer leftEye;
	public SpriteRenderer rightEye;

	void Start() {
		SetAlpha(0.0f);
	}

	void Update() {
		SetAlpha(GetAlpha() + 0.1f * Time.deltaTime);
	}

	void SetAlpha(float a) {
		Color c = leftEye.color;
		c.a = a;
		leftEye.color = c;
		rightEye.color = c;
	}

	float GetAlpha() {
		return leftEye.color.a;
	}

}
