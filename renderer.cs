using UnityEngine;
using System.Collections;

public class renderer : MonoBehaviour {
    static float floor_offset = -2.0f;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void make_maze(tile[][] maze)
    {
        GameObject wall = GameObject.Find("green_tan_wall");
        GameObject floor = GameObject.Find("black_white_checkered_floor");
        GameObject post = GameObject.Find("wall_post");
        for (int z = 0; z < maze.Length; z++)
        {
            for(int x = 0;x < maze[0].Length; x++)
            {
                Debug.Log("making tile at (" + x + "," + z + ")");
                GameObject temp_floor = (GameObject)Instantiate(floor, new Vector3(floor_offset + -4.0f * x, 0, floor_offset + -4.0f * z), Quaternion.identity);
                temp_floor.transform.Rotate(new Vector3(270, 0, 0));

                //north wall decision
                if(z == 0) //only place on the edge
                {
                    GameObject temp_north_wall = (GameObject)Instantiate(wall, new Vector3(-2.0f + -4.0f * x, 2.0f, -4.0f * z), Quaternion.identity);
                    temp_north_wall.transform.Rotate(new Vector3(270, 0, 0));
                }
                //west wall decision
                if (x == 0) //only place on the edge
                {
                    GameObject temp_west_wall = (GameObject)Instantiate(wall, new Vector3(-4.0f * x, 2.0f, -2.0f + -4.0f * z), Quaternion.identity);
                    temp_west_wall.transform.Rotate(new Vector3(270, 0, 0));
                }
                //east wall decision
                if (x == maze[0].Length) //place on edge and when it is needed
                {
                    GameObject temp_east_wall = (GameObject)Instantiate(wall, new Vector3(-2.0f + -4.0f * x, 2.0f, -4.0f + -4.0f * z), Quaternion.identity);
                    temp_east_wall.transform.Rotate(new Vector3(270, 0, 0));
                }
                //south wall decision
                if(z == maze.Length) //place on edge and when it is needed
                {
                    GameObject temp_south_wall = (GameObject)Instantiate(wall, new Vector3(-2.0f + -4.0f * x, 2.0f, -4.0f + -4.0f * z), Quaternion.identity);
                    temp_south_wall.transform.Rotate(new Vector3(270, 0, 0));
                }
            }
        }
    }
}
