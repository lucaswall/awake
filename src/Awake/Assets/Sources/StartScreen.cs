using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

	public Text tapToStartText;
	public float tapToStartSpeed;
	public Image black;
	public float blackEnterSpeed;
	public Text introText;
	public float introTextEnterSpeed;
	public float scaleIncrement;

	float textCount;
	bool started = false;

	void Start() {
		textCount = tapToStartSpeed;
	}

	void Update() {
		if ( Input.GetMouseButtonDown(0) && ! started ) {
			started = true;
			StartCoroutine(StartSequence());
		}
		textCount -= Time.deltaTime;
		if ( textCount <= 0.0f ) {
			textCount = tapToStartSpeed;
			tapToStartText.enabled = ! tapToStartText.enabled;
		}
	}

	IEnumerator StartSequence() {
		while ( black.color.a < 1.0f ) {
			Color c = black.color;
			c.a += blackEnterSpeed * Time.deltaTime;
			black.color = c;
			yield return null;
		}
		while ( introText.color.a < 1.0f ) {
			Color c = introText.color;
			c.a += introTextEnterSpeed * Time.deltaTime;
			introText.color = c;
			Vector3 scale = introText.transform.localScale;
			scale.x += scaleIncrement * Time.deltaTime;
			scale.y += scaleIncrement * Time.deltaTime;
			introText.transform.localScale = scale;
			yield return null;
		}
		SceneManager.LoadScene("Main");
	}

}
