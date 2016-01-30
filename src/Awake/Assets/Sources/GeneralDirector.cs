using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GeneralDirector : MonoBehaviour {

	public Animator animator;
	public BeatController beatController;
	public SigilCanvas sigilCanvas;
	public GameObject tapToRestartText;
	public Text sigilCounterText;
	public Text scoreCurrentText;
	public Text scoreHighestText;

	const string HIGH_SCORE = "HIGH_SCORE";
	const string CURRENT_SCORE_TEXT = "You scored {0} points";
	const string HIGHEST_SCORE_TEXT = "Your highest score is {0}";

	bool restartOnTap = false;

	void Update() {
		if ( restartOnTap && Input.GetMouseButtonDown(0) ) {
			SceneManager.LoadScene("Start");
		}
	}

	public void GameOver() {
		animator.SetTrigger("GameOver");
		beatController.StopBeat();
		sigilCanvas.AbortSigil();
		StartCoroutine(WaitForRestartTap());
	}

	IEnumerator WaitForRestartTap() {
		yield return new WaitForSeconds(1.5f);
		restartOnTap = true;
		tapToRestartText.SetActive(true);
		UpdateScore();
	}

	void UpdateScore() {
		int highest = PlayerPrefs.GetInt(HIGH_SCORE, 0);
		int score = sigilCanvas.SigilCounter;
		if ( score > 0 ) {
			if ( score > highest ) {
				highest = score;
				PlayerPrefs.SetInt(HIGH_SCORE, score);
			}
			scoreCurrentText.text = String.Format(CURRENT_SCORE_TEXT, score);
			scoreHighestText.text = String.Format(HIGHEST_SCORE_TEXT, highest);
			sigilCounterText.enabled = false;
			scoreCurrentText.enabled = true;
			scoreHighestText.enabled = true;
		}
	}

}
