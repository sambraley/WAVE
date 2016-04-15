using UnityEngine;
using System.Collections;

public class CopyScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameObject wall = GameObject.Find ("green_tan_wall");
		GameObject floor = GameObject.Find ("black_white_checkered_floor");
		GameObject post = GameObject.Find ("wall_post");
		for (int i = 1; i < 10; i++) {
			Debug.Log("making floor?");
			GameObject temp = (GameObject)Instantiate (floor, new Vector3 (-2.0f + i * -4.0f, 0, -2), Quaternion.identity);
			Debug.Log ("instance id: " + temp.GetInstanceID());
			temp.transform.Rotate( new Vector3 (270,0,0));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
