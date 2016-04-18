using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class wallmaster : MonoBehaviour {
	tile[,] maze;
	Vector3 spawn;
    int size;
	int zones;
    string seed;

	//init all
	void Start()
	{
		Debug.Log("calling render");
		maze = new tile[20,20];
		spawn = new Vector3(1,2,-1);
        size = maze.GetLength(0);
        for (int z = 0; z < size; z++)
			for (int x = 0; x < size; x++)
				maze[z, x] = new tile();
		invisiblehand renderer = new invisiblehand();
		renderer.render_maze(maze, spawn);
	}
	
	void Update(){
		
	}
	
    void create_maze()
    {
        seed = Time.time.ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        Vector2 current = new Vector2(rand.Next(0,size),rand.Next(0,size));
        Debug.Log("starting node is x: " + current.x + " y: " + current.y);
        List<Vector2> frontiers = new List<Vector2>();
        make_frontiers(current,frontiers);
        while (frontiers.Count > 0)
        {
            //pick frontier
            current = frontiers[rand.Next(0, frontiers.Count)];
            
            //remove from the frontiers list

            //make part of maze
            maze[(int)current.x, (int)current.y].set_status(tile.Status.maze);
            //find neighbors
            List<Vector2> neighbors = find_neighbors(current);
            //pick a neighbor
            Vector2 neighbor;
            if (neighbors.Count > 0)
            {
                neighbor = neighbors[rand.Next(0, neighbors.Count)];
                //knock down wall
                delete_wall(current, neighbor);

            }
            else
            {
                Debug.Log("shits fucked yo");
            }
        }
    }

    void make_frontiers(Vector2 current, List<Vector2> frontiers)
    {
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
            maze[(int)current.x, (int)current.y].set_status(tile.Status.frontier);
            frontiers.Add(current);
        }
    }

    List<Vector2> find_neighbors(Vector2 current)
    {
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
        if (current.y > -1 && current.y < size && current.x > -1 && current.x < size) //is it in bounds?
            if (maze[(int)current.x, (int)current.y].get_status() == tile.Status.maze) //is it a maze piece?
                neighbors.Add(current); //then add it
    }

    void delete_wall(Vector2 a, Vector2 b)
    {

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