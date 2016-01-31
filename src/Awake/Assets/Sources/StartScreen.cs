using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	public Text tapToStartText;
	public float tapToStartSpeed;

	float textCount;

	void Start() {
		textCount = tapToStartSpeed;
	}

	void Update() {
		if ( Input.GetMouseButtonDown(0) ) {
			SceneManager.LoadScene("Main");
		}
		textCount -= Time.deltaTime;
		if ( textCount <= 0.0f ) {
			textCount = tapToStartSpeed;
			tapToStartText.enabled = ! tapToStartText.enabled;
		}
	}

}
