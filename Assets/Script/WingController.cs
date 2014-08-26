using UnityEngine;
using System.Collections;

public class WingController : MonoBehaviour {

	public GameObject WingPartsPrefab;		// 翼パーツのプレハブ
	public int PartsValue = 10;				// 片側あたりのパーツ数
	public float WingWide = 3.0f;			// 翼の幅
	public float PartsDist = 0.4f;
	public float WingRotMax = 80.0f;
	public float PartsRotMax = 5.0f;

	int[] right = new int[]{1,-1};
	GameObject[,] Parts;
	// Use this for initialization
	void Start () {
		Parts = new GameObject[2,PartsValue];
		createWing ();
	}
	
	// Update is called once per frame
	void Update () {
		moveWing ();
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
				// 大きさ
				Vector3 scale = parts.transform.localScale;
				scale.x *= 1.2f * j;
				parts.transform.localScale = scale;

				Parts [i, j] = parts;
			}
		}
	}

	/// <summary>
	/// 翼を制御
	/// </summary>
	void moveWing(){
		// 入力
		float[,] sticks = new float[2,2];
		sticks [0, 0] = Input.GetAxis ("Horizontal_R1");
		sticks [0, 1] = Input.GetAxis ("Vertical_R1");
		sticks [1, 0] = Input.GetAxis ("Horizontal_L1");
		sticks [1, 1] = Input.GetAxis ("Vertical_L1");

		// 位置調整
		for (int i = 0; i < 2; i++) {
			GameObject temp = new GameObject ();
			temp.transform.parent = transform;
			temp.transform.localPosition = Vector3.zero;
			temp.transform.Rotate (WingRotMax*sticks[i,1],(90-30)*right [i],0);
			for (int j = 0; j < PartsValue; j++) {
				GameObject parts = Parts[i,j];
				parts.transform.position = temp.transform.position;
				parts.transform.rotation = temp.transform.rotation;
				parts.transform.position += parts.transform.right * (parts.transform.localScale.x/2) * right[i];
				temp.transform.Rotate (-PartsRotMax*WingRotMax*(sticks[i,0]-1f),-3f*right[i],0);
				temp.transform.position += temp.transform.forward * PartsDist;
			}
		}
	}
}
