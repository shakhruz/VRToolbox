using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour {

	public GameObject head;
	public float forwardSpeed = 100.0f;
	public bool canJump = true;
	
	float rotationSpeed = 1.0f;
	Rigidbody rr;
	Vector3 axis;
	float rotationY, rotationX, rotationZ;
	
	void Start() {
		rr = GetComponent<Rigidbody>();
	}
	
	void Update() {
		FlightMode();
		if (canJump && Cardboard.SDK.Triggered) {
			Jump();
		}
	}
	
	void FlightMode() {
		rotationX = head.transform.localRotation.x / 2;
		rotationY = head.transform.localRotation.y / 2;
		rotationZ = head.transform.localRotation.z;
		axis = new Vector3(rotationX, rotationY, rotationZ);
		transform.Rotate(axis * Time.deltaTime * rotationSpeed);
		rr.velocity = head.transform.forward * forwardSpeed;
	}
	
	void Jump() {
		rr.AddForce(Vector3.up * forwardSpeed);
	}
}
