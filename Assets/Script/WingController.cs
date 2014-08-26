using UnityEngine;
using System.Collections;

public class WingController : MonoBehaviour {

	public GameObject WingPartsPrefab;		// 翼パーツのプレハブ
	public int PartsValue = 10;				// 片側あたりのパーツ数
	public float WingWide = 3.0f;			// 翼の幅

	int[] right = new int[]{1,-1};
	// Use this for initialization
	void Start () {
		createWing ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 翼を生成
	/// </summary>
	void createWing(){
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < PartsValue; j++) {
				GameObject parts = (GameObject)Instantiate (WingPartsPrefab);
				parts.transform.parent = transform;
				// 位置
				Vector3 pos = Vector3.zero;
				pos.x = WingWide / PartsValue * j * right [i];
				parts.transform.localPosition = pos;
			}
		}
	}
}
