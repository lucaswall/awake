using UnityEngine;
using System.Collections;

public class GeneralDirector : MonoBehaviour {

	public Animator animator;
	public BeatController beatController;
	public SigilCanvas sigilCanvas;

	public void GameOver() {
		animator.SetTrigger("GameOver");
		beatController.StopBeat();
		sigilCanvas.AbortSigil();
	}

}
