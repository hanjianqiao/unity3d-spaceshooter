using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;

	public float speed;

	public float touch_speed;

	public Boundary bounds;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void FixedUpdate(){
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			float mh = Input.GetAxis ("Horizontal");
			float mv = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (mh, 0, mv);

			rb.velocity = movement * speed;

		} else {
			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
				// Get movement of the finger since last frame
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

				// Move object across XY plane
				transform.Translate (touchDeltaPosition.x * touch_speed, 0, touchDeltaPosition.y * touch_speed);
			}
		}
		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, bounds.xMin, bounds.xMax), 0, Mathf.Clamp (rb.position.z, bounds.zMin, bounds.zMax));
	}
}
