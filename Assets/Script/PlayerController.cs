using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの動きを制御する。
/// 入力は、PlayerInputから受け取る。
/// </summary>
public class PlayerController : MonoBehaviour {

	public GameObject Body;
	public GameObject RotX;
	public GameObject RotY;
	public GameObject RotZ;
	public float FlyUpPower = 2.0f;
	public float FlyForwordPower = 2.0f;
	public float RotPower = 2.0f;
	public float DownStopPower = 2.0f;
	public float RotStopPower = 2.0f;
	float[,] stick = new float[2,2];	// スティック入力位置
	float[,] fly = new float[2,2];	// スティックの早さ

	float ForwardSpeed = 0;
	Vector3 RotSpeed = Vector3.zero;
	Vector3 Rot = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveFly ();
		stopFly ();
	}

	public void setInput(float[,] stickinput, float[,] flyinput){
		stick = stickinput;
		fly = flyinput;
	}

	// 羽ばたいた時の挙動
	void moveFly(){
		float speedR = Mathf.Max (fly [0, 1] * stick [0, 0], 0);
		float speedL = Mathf.Max (fly [1, 1] * stick [1, 0], 0);
		float rot = speedR - speedL;
		RotSpeed.z += rot*RotPower;			// Z軸回転

		float upspeed = speedR * speedL;
		rigidbody.AddForce (RotZ.transform.up*upspeed*FlyUpPower);				// 上昇力
		ForwardSpeed += upspeed*FlyForwordPower; // 推進力
	}

	// 翼を広げた時の挙動
	void stopFly(){
		// どっちに傾いているか
		float rotright = 1;
		if (RotZ.transform.localEulerAngles.z > 180) {
			rotright = -1;
		}
		// 傾きの度合い
		float nowrot = Mathf.Abs (RotZ.transform.localRotation.z);
		float rotpower = Mathf.Abs (nowrot-0.5f)*2;
		// 入力
		float stopperR = Mathf.Max(stick [0, 0],0);
		float stopperL = Mathf.Max(stick [1, 0],0);
		// 回転停止力
		float rotstop = stopperR * stopperL * RotStopPower;
		float angvel = RotSpeed.z;
		if (angvel > 0) {
			angvel = Mathf.Max (angvel - rotstop, 0);
		} else {
			angvel = Mathf.Min (angvel + rotstop, 0);
		}
		RotSpeed.z = angvel;

		// 下降(上昇)停止力
		float downstop = stopperR * stopperL * DownStopPower * rotpower;
		Vector3 vel = rigidbody.velocity;
		if (vel.y > 0) {
			vel.y = Mathf.Max (vel.y - downstop, 0);
		} else {
			vel.y = Mathf.Min (vel.y + downstop, 0);
		}
		rigidbody.velocity = vel;

		// 傾きによるy軸円回転
		//rigidbody.AddTorque (0,-(1-rotpower)*rotright,0);
		Rot.y += -(1 - rotpower) * rotright * stopperR * stopperL;
		if (Rot.y < -180) {
			Rot.y += 360;
		} else if (Rot.y > 180) {
			Rot.y -= 360;
		}
		RotY.transform.localEulerAngles = Rot;
	}


	void FixedUpdate(){
		Vector3 vel = RotZ.transform.forward * ForwardSpeed;
		float vely = rigidbody.velocity.y;
		vel.y = vely;
		rigidbody.velocity = vel;
		//rigidbody.AddForce (transform.forward * ForwardSpeed);
		RotZ.transform.Rotate (0,0,RotSpeed.z);

		ForwardSpeed *= 0.999f;
		RotSpeed *= 0.995f;
	}
}
