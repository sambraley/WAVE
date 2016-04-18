using UnityEngine;
using System.Collections;

public class invisiblehand : MonoBehaviour
{
    static float offset = 2.0f;
    static float scale = 4.0f;
    static float wall_height = 2.0f;
    static float post_height = 2.25f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void render_maze(tile[,] maze, Vector3 spawn)
    {
        GameObject player = (GameObject)Instantiate((Resources.Load("player_character") as GameObject), spawn, Quaternion.identity);
        GameObject wall = Resources.Load("green_pink_wall") as GameObject;
        GameObject floor = Resources.Load("black_white_checkered_floor") as GameObject;
        GameObject post = Resources.Load("white_wall_post") as GameObject;
        Debug.Log("begin render");
        for (int z = 0; z < maze.GetLength(0); z++)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                Debug.Log("making tile at (" + x + "," + z + ")");

                GameObject temp_floor = (GameObject)Instantiate(floor, new Vector3(offset + scale * x, 0, -offset + -scale * z), Quaternion.identity);
                temp_floor.transform.Rotate(new Vector3(-90, 0, 0));
                
                //north wall decision
                if (maze[z, x].get_northwall() == (int)tile.wall.wall && z == 0)
                {
                    GameObject temp_north_wall = (GameObject)Instantiate(wall, new Vector3(offset + scale * x, wall_height, -scale * z), Quaternion.identity);
                    temp_north_wall.transform.Rotate(new Vector3(-90, 0, 0));
                    //north west pole
                    GameObject temp_nw_post = (GameObject)Instantiate(post, new Vector3(scale * x, post_height, -scale * z), Quaternion.identity);
                    temp_nw_post.transform.Rotate(new Vector3(-90, 0, 0));
                    //north east pole
                    GameObject temp_ne_post = (GameObject)Instantiate(post, new Vector3(scale + scale * x, post_height, -scale * z), Quaternion.identity);
                    temp_ne_post.transform.Rotate(new Vector3(-90, 0, 0));
                }
                //west wall decision
                if (maze[z, x].get_westwall() == (int)tile.wall.wall && x == 0)
                {
                    GameObject temp_west_wall = (GameObject)Instantiate(wall, new Vector3(scale * x, wall_height, -offset + -scale * z), Quaternion.identity);
                    temp_west_wall.transform.Rotate(new Vector3(-90, 90, 0));
                    //south west pole
                    GameObject temp_sw_post = (GameObject)Instantiate(post, new Vector3(scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                    temp_sw_post.transform.Rotate(new Vector3(-90, 0, 0));
                }
                //east wall decision
                if (maze[z, x].get_eastwall() == (int)tile.wall.wall)
                {
                    GameObject temp_east_wall = (GameObject)Instantiate(wall, new Vector3(scale + scale * x, wall_height, -offset + -scale * z), Quaternion.identity);
                    temp_east_wall.transform.Rotate(new Vector3(-90, 90, 0));
                    if (z != 0) //north east pole
                    {
                        GameObject temp_ne_post = (GameObject)Instantiate(post, new Vector3(scale + scale * x, post_height, -scale * z), Quaternion.identity);
                        temp_ne_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    //south east pole
                    GameObject temp_se_post = (GameObject)Instantiate(post, new Vector3(scale + scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                    temp_se_post.transform.Rotate(new Vector3(-90, 0, 0));
                }
                //south wall decision
                if (maze[z, x].get_southwall() == (int)tile.wall.wall)
                {
                    GameObject temp_south_wall = (GameObject)Instantiate(wall, new Vector3(offset + scale * x, wall_height, -scale + -scale * z), Quaternion.identity);
                    temp_south_wall.transform.Rotate(new Vector3(-90, 0, 0));
                    //south west pole
                    if (x != 0)
                    {
                        GameObject temp_sw_post = (GameObject)Instantiate(post, new Vector3(scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                        temp_sw_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                    //south east pole
                    if (maze[z, x].get_eastwall() != (int)tile.wall.wall)
                    {
                        GameObject temp_se_post = (GameObject)Instantiate(post, new Vector3(scale + scale * x, post_height, -scale + -scale * z), Quaternion.identity);
                        temp_se_post.transform.Rotate(new Vector3(-90, 0, 0));
                    }
                }
            }
        }
    }
}

