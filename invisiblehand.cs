using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class invisiblehand : MonoBehaviour
{
	static float offset = 2.0f;
	static float scale = 4.0f;
	static float wall_height = 2.0f;
	static float post_height = 2.25f;

    static Dictionary<string, GameObject> resources;

    public Material[] materials;

	GameObject enemy;
    GameObject player;
	
	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F1)){
			SceneManager.LoadScene("asset zone");
		}
	}

	public void render_maze(tile[,] maze, Vector3 spawn)
	{
        resources = new Dictionary<string, GameObject>();

        resources["wall"] = (Resources.Load("green_pink_wall") as GameObject);
		resources["floor"] =  (Resources.Load("black_white_checkered_floor") as GameObject);
		resources["grass"] = (Resources.Load("grass_tile") as GameObject);
		resources["post"] = (Resources.Load("white_wall_post") as GameObject);
        resources["flamingo"] = (Resources.Load("flamingo") as GameObject);
        resources["column_tower"] = (Resources.Load("column_ladder_combo") as GameObject);
        resources["hedge"] = (Resources.Load("hedge") as GameObject);
        resources["key"] = (Resources.Load("key") as GameObject);
        resources["door"] = (Resources.Load("door") as GameObject);
        resources["door_frame"] = (Resources.Load("door_frame") as GameObject);
        resources["grass_blade"] = (Resources.Load("grass_blade") as GameObject);
        resources["butt_column"] = (Resources.Load("butt_column") as GameObject);

        player = (GameObject)Instantiate((Resources.Load("player_character") as GameObject), spawn, Quaternion.identity);
        player.transform.LookAt(new Vector3(2, 2, -2));
//		enemy = (GameObject) Instantiate((Resources.Load("bust_column") as GameObject), new Vector3(20, 0, -20), Quaternion.identity);

		Debug.Log("begin render");
		for (int z = 0; z < maze.GetLength(0); z++)
		{
            //assuming max z == max x (maze is a square)
            GameObject temp_north_wall = (GameObject)Instantiate(resources["wall"], new Vector3(offset + scale * z, wall_height, 0), Quaternion.identity);
            temp_north_wall.transform.Rotate(new Vector3(-90, 0, 0));
            //north west pole
            GameObject temp_nw_post = (GameObject)Instantiate(resources["post"], new Vector3(scale * z, post_height, 0), Quaternion.identity);
            temp_nw_post.transform.Rotate(new Vector3(-90, 0, 0));
            //north east pole
            GameObject temp_ne_post_1 = (GameObject)Instantiate(resources["post"], new Vector3(scale + scale * z, post_height, 0), Quaternion.identity);
            temp_ne_post_1.transform.Rotate(new Vector3(-90, 0, 0));
            GameObject temp_west_wall = (GameObject)Instantiate(resources["wall"], new Vector3(0, wall_height, -offset + -scale * z), Quaternion.identity);
            temp_west_wall.transform.Rotate(new Vector3(-90, 90, 0));
            //south west pole
            GameObject temp_sw_post_1 = (GameObject)Instantiate(resources["post"], new Vector3(0, post_height, -scale + -scale * z), Quaternion.identity);
            temp_sw_post_1.transform.Rotate(new Vector3(-90, 0, 0));

            for (int x = 0; x < maze.GetLength(0); x++)
			{
                //Debug.Log("making tile at (" + x + "," + z + ")");
                tile current = maze[z, x];
                if (current.get_status() == tile.Status.maze)
                {
					if(current.get_type() == tile.Type.normal || current.get_type() == tile.Type.special)
					{
						GameObject temp_floor = (GameObject)Instantiate(resources["floor"], new Vector3(offset + scale * x, 0, -offset + -scale * z), Quaternion.identity);
						temp_floor.transform.Rotate(new Vector3(-90, 0, 0));
					}
					else if (current.get_type() == tile.Type.alcove)
					{
						GameObject temp_floor = (GameObject)Instantiate(resources["grass"], new Vector3(offset + scale * x, 0, -offset + -scale * z), Quaternion.identity);
						temp_floor.transform.Rotate(new Vector3(-90, 0, 0));
					}
                    //else if (current.get_type() == tile.Type.special)
                    //{
                    //    GameObject temp_floor = (GameObject)Instantiate(resources["floor"], new Vector3(offset + scale * x, 0, -offset + -scale * z), Quaternion.identity);
                    //    temp_floor.transform.Rotate(new Vector3(-90, 0, 0));
                    //}
                }
                //check to see if we need to place something
                if (current.get_contains() != null)
                {
                    GameObject thing = (GameObject)Instantiate(resources[current.get_contains()], new Vector3(offset + scale * x, 0, -offset + -scale * z), Quaternion.identity);
                    if (current.get_contains() == "column_tower")
                    {
                        if (current.get_northwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0,90,0));
                        }
                        else if (current.get_eastwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0,180,0));
                        }
                        else if(current.get_southwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0,270, 0));
                        }
                    }
                    else if (current.get_contains() == "flamingo") 
                    {
                        if (current.get_northwall() == tile.Wall.none || current.get_southwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0, 90, 0));
                        }
                    }
                    else if (current.get_contains() == "butt_column")
                    {
                        if (current.get_eastwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        else if (current.get_southwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0, 180, 0));
                        }
                        else if(current.get_westwall() == tile.Wall.none)
                        {
                            thing.transform.Rotate(new Vector3(0, 270, 0));
                        }
                    }
                    else if (current.get_contains() == "key")
                    {
                        thing.SendMessage("set_number", 1);
                    }
                }
                //east wall decision
                if (current.get_eastwall() != tile.Wall.none)
                {

                    if (current.get_eastwall() == tile.Wall.wall)
                    {
                        GameObject temp_east_wall = (GameObject)Instantiate(resources["wall"], new Vector3(scale + scale * x, wall_height, -offset + -scale * z), Quaternion.identity);
                        temp_east_wall.transform.Rotate(new Vector3(-90, 90, 0));
                    }
                    else if (current.get_eastwall() == tile.Wall.door)
                    {
                        GameObject temp_east_door_frame = (GameObject)Instantiate(resources["door_frame"], new Vector3(scale + scale * x, 0, -offset + -scale * z), Quaternion.identity);
                        temp_east_door_frame.transform.Rotate(new Vector3(-90, 0, 0));
                        GameObject temp_east_door = (GameObject)Instantiate(resources["door"], new Vector3(scale + scale * x, 0, -offset + -scale * z), Quaternion.identity);
                        temp_east_door.transform.Rotate(new Vector3(0, 180, 0));
                        temp_east_door.SendMessage("set_number", 1);
                    }
                    if (current.get_northwall() != tile.Wall.wall) //north east pole
                    {
                        GameObject temp_ne_post = (GameObject)Instantiate(resources["post"], new Vector3(scale + scale * x, post_height, -scale * z), Quaternion.identity);
                        temp_ne_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    //south east pole
                    GameObject temp_se_post = (GameObject)Instantiate(resources["post"], new Vector3(scale + scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                    temp_se_post.transform.Rotate(new Vector3(-90, 0, 0));
                }
                //south wall decision
                if (current.get_southwall() != tile.Wall.none)
                {
                    if (current.get_southwall() == tile.Wall.wall)
                    {
                        GameObject temp_south_wall = (GameObject)Instantiate(resources["wall"], new Vector3(offset + scale * x, wall_height, -scale + -scale * z), Quaternion.identity);
                        temp_south_wall.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    else if (current.get_southwall() == tile.Wall.door)
                    {
                        GameObject temp_south_door_frame = (GameObject)Instantiate(resources["door_frame"], new Vector3(offset + scale * x, 0, -scale + -scale * z), Quaternion.identity);
                        temp_south_door_frame.transform.Rotate(new Vector3(-90, 90, 0));
                        GameObject temp_south_door = (GameObject)Instantiate(resources["door"], new Vector3(offset + scale * x, 0, -scale + -scale * z), Quaternion.identity);
                        temp_south_door.transform.Rotate(new Vector3(0, 90, 0));
                    }
                    //south west pole
                    if (current.get_westwall() != tile.Wall.wall)
                    {
                        GameObject temp_sw_post = (GameObject)Instantiate(resources["post"], new Vector3(scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                        temp_sw_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    //south east pole
                    if (current.get_eastwall() != tile.Wall.wall)
                    {
                        GameObject temp_se_post = (GameObject)Instantiate(resources["post"], new Vector3(scale + scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                        temp_se_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                }
			}
		}
	}
}

