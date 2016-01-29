using UnityEngine;
using System.Collections;

public class BeatController : MonoBehaviour {

	public AudioSource beatSource;
	public BeatIndicator beatIndicator;

	public void Start() {
		StartCoroutine(PlayOneBeat());
	}

	IEnumerator PlayOneBeat() {
		beatSource.Play();
		beatIndicator.Beat();
		yield return new WaitForSeconds(0.6f);
		beatIndicator.Beat();
		yield return new WaitForSeconds(0.3f);
		beatIndicator.Beat();
		yield return new WaitForSeconds(0.3f);
		beatIndicator.Beat();
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(PlayOneBeat());
	}

}
