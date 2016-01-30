using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeatIndicator : MonoBehaviour {

	public SpriteRenderer sprite;
	public float alphaPerSec;

	void Start() {
		SetAlpha(0.0f);
	}

	void Update() {
		if ( sprite.color.a > 0.0f ) {
			SetAlpha(sprite.color.a - alphaPerSec * Time.deltaTime);
			if ( sprite.color.a < 0.0f ) SetAlpha(0.0f);
		}
	}

	void SetAlpha(float a) {
		Color color = sprite.color;
		color.a = a;
		sprite.color = color;
	}

	public void Beat() {
		SetAlpha(1.0f);
	}

	public float Intensity() {
		return sprite.color.a;
	}

}
