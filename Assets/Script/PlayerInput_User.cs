using UnityEngine;
using System.Collections;

public class PlayerInput_User : MonoBehaviour {

	PlayerController pController;
	public float[,] stick = new float[2,2];	// スティック入力位置
	public float[,] fly = new float[2,2];	// スティックの早さ

	// Use this for initialization
	void Start () {
		pController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		stick = InputManager.Instance.Sticks;
		fly = InputManager.Instance.getStickSpeed();
		pController.setInput (stick, fly);
	}
}
