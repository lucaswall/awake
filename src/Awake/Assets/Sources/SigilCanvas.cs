using UnityEngine;
using System.Collections;

public class SigilCanvas : MonoBehaviour {

	public GameObject sigilPrefab;
	public EnergyHolder energyHolder;
	public ParticleSystem sigilTrace;

	bool drawing = false;
	int touchId;

	void Update() {
		if ( ! drawing ) CheckForInitialTouch();
		else CheckForMoveTouch();
	}

	public void SigilFragmentComplete(SigilFragment fragment) {
		Debug.Log("sigil fragment complete!");
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
						UnlockTrace();
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
		Debug.Log(-camera.transform.position.z);
		Vector3 vec3 = new Vector3(pos.x, pos.y, -camera.transform.position.z);
		sigilTrace.transform.position = camera.ScreenToWorldPoint(vec3);
	}

	void SigilTraceMove(Touch touch) {
		if ( TouchIsInArea(touch) ) {
			SetSigilTracePosition(touch.position);
		} else {
			UnlockTrace();
		}
	}

}
