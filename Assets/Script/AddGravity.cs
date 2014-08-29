using UnityEngine;
using System.Collections;

public class AddGravity : MonoBehaviour {

	public float Gravity;
	void FixedUpdate(){
		rigidbody.AddForce (-Vector3.up * Gravity * rigidbody.mass);
	}
}
