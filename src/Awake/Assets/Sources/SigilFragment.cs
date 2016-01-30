using UnityEngine;
using System.Collections;

public class SigilFragment : MonoBehaviour {

	Collider touchCollider;
	SpriteRenderer sprite;
	SigilCanvas sigilCanvas;

	void Awake() {
		touchCollider = GetComponent<Collider>();
		sprite = GetComponent<SpriteRenderer>();
		DisableFragment();
	}

	public void OnCanvas() {
		sigilCanvas = GetComponentInParent<SigilCanvas>();
	}

	void DisableFragment() {
		touchCollider.enabled = false;
		sprite.enabled = false;
	}

	public void EnableFragment() {
		touchCollider.enabled = true;
		sprite.enabled = true;
	}

	void Update() {
		for ( int i = 0; i < Input.touchCount; i++ ) {
			Touch t = Input.GetTouch(i);
			if ( IsValidTouch(t) ) ProcessTouch(t);
		}
	}

	bool IsValidTouch(Touch touch) {
		if ( touch.phase != TouchPhase.Began && touch.phase != TouchPhase.Moved ) 
			return false;
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		RaycastHit hit;
		return touchCollider.Raycast(ray, out hit, 100.0f);
	}

	void ProcessTouch(Touch touch) {
		DisableFragment();
		sigilCanvas.SigilFragmentComplete(this);
	}

}
