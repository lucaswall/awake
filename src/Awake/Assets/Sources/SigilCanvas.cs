using UnityEngine;
using System.Collections;

public class SigilCanvas : MonoBehaviour {

	public GameObject sigilPrefab;
	public EnergyHolder energyHolder;
	public ParticleSystem sigilTrace;
	public Demon demon;
	public AudioSource audioSource;
	public AudioClip sigiltouchedSound;
	public AudioClip sigilCompleteSound;
	public AudioClip sigilFailedSound;

	bool drawing = false;
	int touchId;
	GameObject activeGroup = null;
	SigilFragment[] fragments;
	int currentFragment;

	void Update() {
		if ( activeGroup != null ) {
			if ( ! drawing ) CheckForInitialTouch();
			else CheckForMoveTouch();
		} else {
			if ( energyHolder.IsFull() ) InstantiateNewSigil();
		}
	}

	void InstantiateNewSigil() {
		Debug.Log("New sigil!");
		activeGroup = Instantiate(sigilPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		activeGroup.transform.SetParent(transform, false);
		fragments = activeGroup.GetComponentsInChildren<SigilFragment>();
		currentFragment = 0;
		foreach ( SigilFragment fragment in fragments ) {
			fragment.OnCanvas();
		}
		fragments[currentFragment].EnableFragment();
		demon.Wakeup();
	}

	public void SigilFragmentComplete(SigilFragment fragment) {
		audioSource.PlayOneShot(sigiltouchedSound);
		if ( IsSigilComplete() ) {
			StartCoroutine(SigilCompleteSequence());
		} else {
			EnableNextFragment();
		}
	}

	IEnumerator SigilCompleteSequence() {
		yield return new WaitForSeconds(0.15f);
		audioSource.PlayOneShot(sigilCompleteSound);
		DestroySigil();
		yield return new WaitForSeconds(0.15f);
		demon.PushBack();
	}

	void DestroySigil() {
		Destroy(activeGroup);
		activeGroup = null;
		energyHolder.DrainAllEnergy();
		UnlockTrace();
	}

	void EnableNextFragment() {
		if ( IsSigilComplete() ) return;
		fragments[++currentFragment].EnableFragment();
	}

	bool IsSigilComplete() {
		return currentFragment >= fragments.Length - 1;
	}

	void CheckForInitialTouch() {
		for ( int i = 0; i < Input.touchCount; i++ ) {
			Touch touch = Input.GetTouch(i);
			if ( touch.phase == TouchPhase.Began && TouchIsInArea(touch) ) {
				LockOnTouch(touch);
				return;
			}
		}
	}

	void CheckForMoveTouch() {
		for ( int i = 0; i < Input.touchCount; i++ ) {
			Touch touch = Input.GetTouch(i);
			if ( touch.fingerId == touchId ) {
				switch ( touch.phase ) {
					case TouchPhase.Moved:
					case TouchPhase.Stationary:
						SigilTraceMove(touch);
						break;
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
						FailedSigilReset();
						break;
				}
				return;
			}
		}
	}

	bool TouchIsInArea(Touch touch) {
		Vector2 vpPos = Camera.main.ScreenToViewportPoint(touch.position);
		return vpPos.x >= 0.5;
	}

	void LockOnTouch(Touch touch) {
		drawing = true;
		touchId = touch.fingerId;
		SetSigilTracePosition(touch.position);
		sigilTrace.Play();
	}

	void UnlockTrace() {
		drawing = false;
		sigilTrace.Stop();
	}

	void SetSigilTracePosition(Vector2 pos) {
		Camera camera = Camera.main;
		Vector3 vec3 = new Vector3(pos.x, pos.y, -camera.transform.position.z);
		sigilTrace.transform.position = camera.ScreenToWorldPoint(vec3);
	}

	void SigilTraceMove(Touch touch) {
		if ( TouchIsInArea(touch) ) {
			SetSigilTracePosition(touch.position);
		} else {
			FailedSigilReset();
		}
	}

	void FailedSigilReset() {
		audioSource.PlayOneShot(sigilFailedSound);
		UnlockTrace();
		fragments[currentFragment].DisableFragment();
		currentFragment = 0;
		fragments[currentFragment].EnableFragment();
	}

}
