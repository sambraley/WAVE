using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	//state: wander, follow
	tile[,] maze;
	tile currentPos;

	// Use this for initialization
	void Start () {
		GameObject thePlayer = GameObject.Find("Manager");
		wallmaster wm = thePlayer.GetComponent<wallmaster>();
		maze = wm.show_maze();

		currentPos = maze[(int)((transform.position.z/4) * -1), (int)(transform.position.x/4)];
		Debug.Log((int)((transform.position.z/4) * -1) + " " + (int)(transform.position.x/4));
		Debug.Log(transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move()
	{
		//if wander state
	}


}
