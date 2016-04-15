using UnityEngine;
using System.Collections;

public class wallmaster : MonoBehaviour {
	tile[][] maze;
	Vector3 spawn;
	int zones;

	//init all
	void start(){
		maze = new tile[20][20];
		spawn = new Vector3(0,0,0);
	}

	void update(){

	}

	//finds all zones
	//also counts total number of zones
	void find_zones(){

	}

	//find a singular zone; subfunction of zone
	void find_zone(){

	}

	//make sure all zones are connected through doors; place doors if not
	void check_access(){

	}

}