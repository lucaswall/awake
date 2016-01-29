using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeatIndicator : MonoBehaviour {

	public SpriteRenderer sprite;

	void Start() {
		SetAlpha(0.0f);
	}

	void Update() {
		SetAlpha(sprite.color.a - 5.0f * Time.deltaTime);
	}

	void SetAlpha(float a) {
		Color color = sprite.color;
		color.a = a;
		sprite.color = color;
	}

	public void Beat() {
		SetAlpha(1.0f);
	}

}
