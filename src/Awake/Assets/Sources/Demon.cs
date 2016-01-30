using UnityEngine;
using System.Collections;

public class Demon : MonoBehaviour {

	public SpriteRenderer leftEye;
	public SpriteRenderer rightEye;
	public SpriteRenderer leftEyebrow;
	public SpriteRenderer rightEyebrow;
	public Transform leftEyeLid;
	public Transform rightEyeLid;
	public float eyeLidMoveY;
	public float inititalAwakeSpeed;
	public float incrementAwakeSpeed;
	public float maxAwakeSpeed;
	public AudioSource sigilAudioSource;
	public AudioClip sigilSound;
	public AudioSource demonAudioSource;
	public AudioClip demonPushBackSound;
	public AudioClip demonAttackSound;
	public SpriteRenderer sigilImage;
	public float sigilFadeSpeed;
	public float demonFadeSpeed;
	public CameraShake cameraShake;
	public float sigilShakeTime;
	public float sigilShakeAmplitude;
	public GeneralDirector generalDirector;

	float awakeSpeed = 0.0f;
	float awakePercent = 0.0f;
	bool awake = false;
	float eyeLidY;

	void Start() {
		eyeLidY = leftEyeLid.position.y;
		SetSigilAlpha(0.0f);
		UpdateAwakeState();
	}

	void Update() {
		AwakeStep();
	}

	public void Wakeup() {
		awakeSpeed = inititalAwakeSpeed;
	}

	public void PushBack() {
		sigilAudioSource.PlayOneShot(sigilSound);
		demonAudioSource.PlayOneShot(demonPushBackSound);
		awakeSpeed = 0.0f;
		SetSigilAlpha(1.0f);
		cameraShake.Shake(sigilShakeTime, sigilShakeAmplitude);
		StartCoroutine(FadeDemonAway());
		StartCoroutine(FadeSigilAway());
		ProgressLevel();
	}

	void ProgressLevel() {
		inititalAwakeSpeed += incrementAwakeSpeed;
		if ( inititalAwakeSpeed > maxAwakeSpeed ) inititalAwakeSpeed = maxAwakeSpeed;
	}

	IEnumerator FadeDemonAway() {
		while ( awakePercent > 0.0f ) {
			awakePercent -= demonFadeSpeed * Time.deltaTime;
			UpdateAwakeState();
			yield return null;
		}
		awakePercent = 0.0f;
		UpdateAwakeState();
	}

	IEnumerator FadeSigilAway() {
		float alpha = 1.0f;
		while ( alpha > 0.0f ) {
			alpha -= sigilFadeSpeed * Time.deltaTime;
			SetSigilAlpha(Mathf.Sin(alpha * Mathf.PI * 0.5f));
			yield return null;
		}
		SetSigilAlpha(0.0f);
	}

	void AwakeStep() {
		awakePercent += awakeSpeed * Time.deltaTime;
		if ( awakePercent > 1.0f ) awakePercent = 1.0f;
		UpdateAwakeState();
		if ( awakePercent >= 1.0f && ! awake ) {
			Attack();
		}
	}

	void Attack() {
		awake = true;
		demonAudioSource.PlayOneShot(demonAttackSound);
		Destroy(leftEyeLid.gameObject);
		Destroy(rightEyeLid.gameObject);
		generalDirector.GameOver();
	}

	void SetAlpha(float a) {
		Color c = leftEye.color;
		c.a = a;
		leftEye.color = c;
		rightEye.color = c;
		leftEyebrow.color = c;
		rightEyebrow.color = c;
	}

	void SetEyeLidPosition(Transform eyelid) {
		Vector3 pos = eyelid.position;
		pos.y = eyeLidY + eyeLidMoveY * awakePercent;
		eyelid.position = pos;
	}

	void SetSigilAlpha(float a) {
		Color c = sigilImage.color;
		c.a = a;
		sigilImage.color = c;
	}

	void UpdateAwakeState() {
		if ( awake ) return;
		SetAlpha(awakePercent);
		SetEyeLidPosition(leftEyeLid);
		SetEyeLidPosition(rightEyeLid);
	}

}
