using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeatIndicator : MonoBehaviour {

	public SpriteRenderer sprite;
	public float alphaPerSec;
	public Text listenText;
	public Text tapText;

	bool hasBeenHit = false;
	Text currentText;
	int textNum = 0;
	bool showText = true;

	void Start() {
		SetAlpha(0.0f);
		SetAlphaToText(listenText, 0.0f);
		SetAlphaToText(tapText, 0.0f);
		currentText = tapText;
	}

	void OnEnable() {
		GameEvents.OnEnergyHolderFull += EnergyHolderFull;
	}

	void OnDisable() {
		GameEvents.OnEnergyHolderFull -= EnergyHolderFull;
	}

	void Update() {
		if ( sprite.color.a > 0.0f ) {
			SetAlpha(sprite.color.a - alphaPerSec * Time.deltaTime);
			if ( sprite.color.a < 0.0f ) {
				SetAlpha(0.0f);
			}
		}
		if ( currentText.color.a > 0.0f ) {
			SetAlphaToText(currentText, currentText.color.a - alphaPerSec * Time.deltaTime);
			if ( currentText.color.a < 0.0f ) {
				SetAlphaToText(currentText, 0.0f);
			}
		}
	}

	void SetAlpha(float a) {
		Color color = sprite.color;
		color.a = a;
		sprite.color = color;
	}

	void EnergyHolderFull() {
		showText = false;
	}

	public void Beat() {
		SetAlpha(1.0f);
		hasBeenHit = false;
		if ( showText ) {
			SetAlphaToText(currentText, 0.0f);
			currentText = textNum++ % 4 == 0 ? listenText : tapText;
			SetAlphaToText(currentText, 1.0f);
		}
	}

	public float GetEnergy() {
		if ( hasBeenHit ) return 0.0f;
		hasBeenHit = true;
		//Debug.Log("beat hit! "+sprite.color.a);
		return sprite.color.a;
	}

	void SetAlphaToText(Text text, float a) {
		Color c = text.color;
		c.a = a;
		text.color = c;
	}

}
