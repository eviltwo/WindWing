using UnityEngine;
using System.Collections;

public class InputManager : SingletonMonoBehaviour<InputManager> {

	public float[,] Sticks = new float[2,2];
	float[,] OldSticks = new float[2, 2];

	// Use this for initialization
	void Start () {
		checkStick ();
		checkStick ();
	}
	
	// Update is called once per frame
	void Update () {
		checkStick ();
	}

	void checkStick(){
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 2; j++) {
				OldSticks[i,j] = Sticks[i,j];
			}
		}
		// 入力
		Sticks [0, 0] = Input.GetAxis ("Horizontal_R1");
		Sticks [0, 1] = Input.GetAxis ("Vertical_R1");
		Sticks [1, 0] = Input.GetAxis ("Horizontal_L1");
		Sticks [1, 1] = Input.GetAxis ("Vertical_L1");	
	}

	public float[,] getStickSpeed(){
		float[,] diff = new float[2,2];
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 2; j++) {
				diff[i,j] = Sticks[i,j]-OldSticks[i,j];
			}
		}
		return diff;
	}
}
