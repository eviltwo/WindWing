using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {

	public GameObject ObjPrefab;
	public int Value = 50;
	public Vector3 RandomSize = new Vector3();
	public float ScaleMin = 1.0f;
	public float ScaleMax = 10.0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < Value; i++) {
			spawnObj ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnObj(){
		GameObject obj = (GameObject)Instantiate (ObjPrefab);
		obj.transform.parent = transform;
		// 位置
		Vector3 pos = new Vector3 ();
		pos.x = Random.Range (-RandomSize.x, RandomSize.x);
		pos.y = Random.Range (-RandomSize.y, RandomSize.y);
		pos.z = Random.Range (-RandomSize.z, RandomSize.z);
		obj.transform.localPosition = pos;

		float scale = Random.Range (ScaleMin, ScaleMax);
		obj.transform.localScale = new Vector3 (scale, scale, scale);
	}
}
