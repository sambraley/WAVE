using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class wallmaster : MonoBehaviour 
{
	tile[,] maze;
	Vector3 spawn;
    int size;
	int zones;
    string seed;

	//init all
	void Start()
	{
		Debug.Log("calling render");
		maze = new tile[5,5];
		spawn = new Vector3(1,2,-1);
        size = maze.GetLength(0);
        for (int z = 0; z < size; z++)
			for (int x = 0; x < size; x++)
				maze[z, x] = new tile();
        create_maze();
		invisiblehand renderer = new invisiblehand();
		renderer.render_maze(maze, spawn);
	}
	
	void Update(){
		
	}
	
    //main procedural generation loop 
    void create_maze()
    {
        // seed = Time.time.ToString(); creates consistently the same result b/c it's based on seconds since starting the game
        seed = DateTime.Now.ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        Vector2 current = new Vector2(rand.Next(0,size),rand.Next(0,size));
        maze[(int)current.y, (int)current.x].set_status(tile.Status.maze);
        //Debug.Log("starting tile is x: " + current.x + " y: " + current.y);
        List<Vector2> frontiers = new List<Vector2>();
        make_frontiers(current,frontiers);
        //Debug.Log("entering while loop");
        while (frontiers.Count > 0)
        {
            //pick frontier
			int chosen = rand.Next(0, frontiers.Count);
            //Debug.Log("Chosen int is: " + chosen);
            current = frontiers[chosen];
            //Debug.Log("Current/frontier selected for this iteration is: (" + current.x + "," + current.y + ")");
            //remove from the frontiers list
			frontiers.RemoveAt(chosen);
            //make part of maze
            maze[(int)current.y, (int)current.x].set_status(tile.Status.maze);
            //find neighbors
            List<Vector2> neighbors = find_neighbors(current);
            //pick a neighbor
            if (neighbors.Count > 0)
            {
                Vector2 neighbor;   
                neighbor = neighbors[rand.Next(0, neighbors.Count)];
                //knock down wall
                delete_wall(current, neighbor);
				//Add current's frontiers to frontier list
				make_frontiers(current, frontiers);
            }
            else
            { //should never happen
                Debug.Log("shits fucked yo"); //the ultimate debug code
            }
        }
    }

    //makes all cardinal tiles that are status none into status frontiers
    void make_frontiers(Vector2 current, List<Vector2> frontiers)
    {
        //Debug.Log("making frontiers");
        //north
        add_frontier(new Vector2(current.x, current.y - 1), frontiers);
        //west
        add_frontier(new Vector2(current.x -1, current.y), frontiers);
        //east
        add_frontier(new Vector2(current.x + 1, current.y), frontiers);
        //south
        add_frontier(new Vector2(current.x, current.y + 1), frontiers);
    }

    void add_frontier(Vector2 current, List<Vector2> frontiers)
    {
        if (current.y > -1 && current.y < size && current.x > -1 && current.x < size)
        {
            //TODO check if this is status.none
            if (maze[(int)current.y, (int)current.x].get_status() == tile.Status.none)
            {
                maze[(int)current.y, (int)current.x].set_status(tile.Status.frontier); //this needs to be here because otherwise frontiers will get double added
                frontiers.Add(current);
                //Debug.Log("adding frontier (" + current.x + "," + current.y + ")");
            }
        }
    }

    //finds all neighbors AKA tiles cardinal to the current that are of status maze
    List<Vector2> find_neighbors(Vector2 current)
    {
        //Debug.Log("finding neighbors");
        List<Vector2> neighbors = new List<Vector2>();
        //north
        check_neighbor(new Vector2(current.x, current.y - 1), neighbors);
        //west
        check_neighbor(new Vector2(current.x - 1, current.y), neighbors);
        //east
        check_neighbor(new Vector2(current.x + 1, current.y), neighbors);
        //south
        check_neighbor(new Vector2(current.x, current.y + 1), neighbors);
        return neighbors;
    }

   
    void check_neighbor(Vector2 current, List<Vector2> neighbors)
    {
        if (current.y > -1 && current.y < size && current.x > -1 && current.x < size)
        { //is it in bounds?
            if (maze[(int)current.y, (int)current.x].get_status() == tile.Status.maze)
            { //is it a maze piece?
                neighbors.Add(current); //then add it
                //Debug.Log("adding neighbor (" + current.x + "," + current.y + ")");
            }
        }
    }

    //removes the wall that separates the tile located at a and b
    //TODO make this more general, like set_wall()
    void delete_wall(Vector2 a, Vector2 b)
    {
		int ax = (int)a.x;
		int ay = (int)a.y;
		int bx = (int)b.x;
		int by = (int)b.y;

		if(ax < bx) //east wall
		{
            //Debug.Log("Removing east wall");
			maze[ay, ax].set_eastwall(tile.Wall.none);
			maze[by, bx].set_westwall(tile.Wall.none);
		}
		else if (ax > bx) //west wall
		{
            //Debug.Log("Removing west wall");
            maze[ay, ax].set_westwall(tile.Wall.none);
			maze[by, bx].set_eastwall(tile.Wall.none);
		}
		else if (ay < by) //southwall
		{
            //Debug.Log("Removing south wall");
            maze[ay, ax].set_southwall(tile.Wall.none);
			maze[by, bx].set_northwall(tile.Wall.none);
		}
		else if (ay > by) //north wall
		{
            //Debug.Log("Removing north wall");
            maze[ay, ax].set_northwall(tile.Wall.none);
			maze[by, bx].set_southwall(tile.Wall.none);
		}

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
	
    //makes a "room" AKA an open space surrounded on all sides by walls
    void make_room()
    {

    }
}