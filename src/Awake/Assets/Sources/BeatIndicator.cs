using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeatIndicator : MonoBehaviour {

	public SpriteRenderer sprite;
	public float alphaPerSec;

	bool hasBeenHit = false;

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
		hasBeenHit = false;
	}

	public float GetEnergy() {
		if ( hasBeenHit ) return 0.0f;
		hasBeenHit = true;
		//Debug.Log("beat hit! "+sprite.color.a);
		return sprite.color.a;
	}

}
