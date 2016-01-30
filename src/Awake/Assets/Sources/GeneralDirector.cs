using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GeneralDirector : MonoBehaviour {

	public Animator animator;
	public BeatController beatController;
	public SigilCanvas sigilCanvas;
	public GameObject tapToRestartText;

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
	}

}
