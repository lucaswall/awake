using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	public Text tapToStartText;
	public float tapToStartSpeed;
	public Animator animator;
	public AudioSource audioSource;

	float textCount;
	bool started = false;

	void Start() {
		textCount = tapToStartSpeed;
	}

	void Update() {
		if ( Input.GetMouseButtonDown(0) && ! started ) {
			started = true;
			animator.SetTrigger("Intro");
		}
		textCount -= Time.deltaTime;
		if ( textCount <= 0.0f ) {
			textCount = tapToStartSpeed;
			tapToStartText.enabled = ! tapToStartText.enabled;
		}
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}

}
