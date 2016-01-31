using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	public Text tapToStartText;
	public float tapToStartSpeed;
	public Animator animator;

	public Transform gameTitle;
	public float shakeAmplitude;
	public float minShakeInterval;
	public float maxShakeInterval;
	public int shakeLoops;
	public float shakeInterval;

	float textCount;
	bool started = false;
	float timeToShake;

	void Start() {
		textCount = tapToStartSpeed;
		timeToShake = Random.Range(minShakeInterval, maxShakeInterval);
	}

	void Update() {
		CheckForTouch();
		TapToStartBlink();
		CheckTitleShake();
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}

	void CheckForTouch() {
		if ( Input.GetMouseButtonDown(0) && ! started ) {
			started = true;
			animator.SetTrigger("Intro");
		}
	}

	void TapToStartBlink() {
		textCount -= Time.deltaTime;
		if ( textCount <= 0.0f ) {
			textCount = tapToStartSpeed;
			tapToStartText.enabled = ! tapToStartText.enabled;
		}
	}

	void CheckTitleShake() {
		timeToShake -= Time.deltaTime;
		if ( timeToShake <= 0.0f ) {
			timeToShake = Random.Range(minShakeInterval, maxShakeInterval);
			StartCoroutine(MakeTitleShake());
		}
	}

	IEnumerator MakeTitleShake() {
		Vector3 pos = gameTitle.position;
		for ( int i = 0; i < shakeLoops; i++ ) {
			gameTitle.position = pos + (Vector3) Random.insideUnitCircle * shakeAmplitude;
			yield return new WaitForSeconds(shakeInterval);
		}
	}

}
