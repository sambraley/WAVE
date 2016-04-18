using UnityEngine;
using System.Collections;

public class wallmaster : MonoBehaviour {
	tile[,] maze;
	Vector3 spawn;
	int zones;
	
	//init all
	void Start()
	{
		Debug.Log("calling render");
		maze = new tile[20,20];
		spawn = new Vector3(1,2,-1);
		for (int z = 0; z < maze.GetLength(0); z++)
			for (int x = 0; x < maze.GetLength(0); x++)
				maze[z, x] = new tile(new Vector3(x,0,z));
		invisiblehand renderer = new invisiblehand();
		renderer.render_maze(maze, spawn);
	}
	
	void Update(){
		
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