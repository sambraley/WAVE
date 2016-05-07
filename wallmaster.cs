using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class wallmaster : MonoBehaviour
{

	public enum Direction {north, west, east, south, none};

    public int mazeX;
    public int mazeY;
    public bool makeroom;

    string[] alcove_objects;
    tile[,] maze;
	Vector3 spawn;
	int size;
	uint zid;
    List<Zone> global_zones;
    string seed;

    Zone start_zone;
    Zone end_zone;
    uint distance_from_start;

	//init all
	void Awake()
	{
		Debug.Log("calling render");
		maze = new tile[mazeY, mazeX];
		spawn = new Vector3(1, 2, -1);
		size = maze.GetLength(0);
		for (uint z = 0; z < size; z++)
			for (uint x = 0; x < size; x++)
				maze[z, x] = new tile(z ,x);

        make_room(0, 0, 2, 2);
        start_zone = new Zone(0);
        end_zone = start_zone;
        for (uint z = 0; z < 2; z++)
        {
            for (uint x = 0; x < 2; x++)
            {
                maze[z, x].set_status(tile.Status.maze);
                maze[z, x].set_zone(start_zone);
            }
        }
        global_zones = new List<Zone>();
        global_zones.Add(start_zone);
        //maze[1, 0].set_contains("column_tower");
        //maze[1, 1].set_contains("key");
        //maze[0, 1].set_eastwall(tile.Wall.door);
        //maze[0, 2].set_westwall(tile.Wall.door);
        zid = 1;
        create_maze();
        alcove_objects = new string[3];
        alcove_objects[0] = "flamingo";
        alcove_objects[1] = "hedge";
        alcove_objects[2] = "butt_column";
        
        make_rooms();
        find_alcoves();
        add_deadspace();

        find_zones();
		find_neighbors_tiles();
        get_neighbors();
        
        distance_from_start = 0;
        find_end_zone(start_zone, 0);
        Debug.Log("End zone is " + end_zone.get_id() + " and it's distance is " + end_zone.distance); 
        key_king();

        invisiblehand renderer = new invisiblehand();
		renderer.render_maze(maze, spawn);
	}

    void Update() {

	}

    public tile[,] show_maze ()
    {
        return maze;
    }

    void get_neighbors()
    {
        foreach (Zone z in global_zones)
        {
            z.neighbors = z.neighbors_tiles.Keys.ToArray();

            for (int x = 0; x < z.neighbors.Length; x++)
            {
                Debug.Log("This Zone: " + z.get_id() + " Neighbors: " + z.neighbors[x].get_id());
            }
        }
    }


    //main procedural generation loop
    void create_maze()
	{
		// seed = Time.time.ToString(); creates consistently the same result b/c it's based on seconds since starting the game
		seed = DateTime.Now.ToString();
		System.Random rand = new System.Random(seed.GetHashCode());
		Vector2 current = new Vector2(rand.Next(2, size), rand.Next(2, size));
		maze[(int)current.y, (int)current.x].set_status(tile.Status.maze);
		//Debug.Log("starting tile is x: " + current.x + " y: " + current.y);
		List<Vector2> frontiers = new List<Vector2>();
		make_frontiers(current, frontiers);
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
			{	//should never happen
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
		add_frontier(new Vector2(current.x - 1, current.y), frontiers);
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
		{	//is it in bounds?
			if (maze[(int)current.y, (int)current.x].get_status() == tile.Status.maze && maze[(int)current.y, (int)current.x].get_zone() == null)
			{	//is it a maze piece? does it not already have a designated zone?
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

		if (ax < bx) //east wall
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
	//Zones have a static int in constructor that keeps count
	void find_zones() 
	{
		for(int y = 0; y < size; y++)
		{
			for(int x = 0; x < size; x++)
			{
				tile t = maze[y, x];
				if(t.get_zone() == null)
				{
					Zone z = new Zone(zid);
                    zid++;
                    global_zones.Add(z);
					find_zone(y, x, z);
				}
			}
		}

	}

	//find a singular zone; subfunction of zone
	void find_zone(int y, int x, Zone z)
	{
		tile t = maze[y, x];

		if(!t.get_touch())
		{
			t.set_zone(z);
			Debug.Log(t.get_zone().get_id());
			t.touch(true);
			z.add_tile(t);

			if(t.get_northwall() == tile.Wall.none)
				find_zone(y-1, x, z);
			if(t.get_westwall() == tile.Wall.none)
				find_zone(y, x-1, z);
			if(t.get_eastwall() == tile.Wall.none)
				find_zone(y, x+1, z);
			if(t.get_southwall() == tile.Wall.none)
				find_zone(y+1, x, z);
		}

	}

	void find_neighbors_tiles() 
	{
		for(int y = 0; y < size-1; y++)
		{
			for(int x = 0; x < size-1; x++)
			{
				tile t = maze[y, x];
                tile east = null;
                tile south = null;

                Zone eastzone = null;
                Zone southzone = null;

                if ((x + 1) != size)
                {
                    east = maze[y, x + 1];
                    //					Debug.Log("gonna check east tile at (" + east.x + "," + east.y + ")");
                    eastzone = east.get_zone();
                }

                if ((y + 1) != size)
                {
                    south = maze[y + 1, x];
                    southzone = south.get_zone();
                }

                Zone tzone = t.get_zone();

                //				Debug.Log(tzone.get_id()+ " " + eastzone.get_id());
                if (east != null && tzone.get_id() != eastzone.get_id())
                { //Could probably just be get_zone();

                    List<TilePair> ltp = null;

                    if (tzone.neighbors_tiles.TryGetValue(eastzone, out ltp))
                    {
                        Debug.Log("Found");
                        ltp.Add(new TilePair(t, east));
                        //assuming other zone has neighbor as well
                        List<TilePair> eltp = null;
                        eastzone.neighbors_tiles.TryGetValue(tzone, out eltp);
                        eltp.Add(new TilePair(east, t));
                    }
                    else
                    {
                        Debug.Log("Did not have pair matching for " + tzone.get_id() + " " + eastzone.get_id());
                        //Assuming the other zone does not have a list either
                        List<TilePair> l1 = new List<TilePair>();
                        List<TilePair> l2 = new List<TilePair>();

                        l1.Add(new TilePair(t, east));
                        l2.Add(new TilePair(east, t));
                        tzone.neighbors_tiles.Add(eastzone, l1);
                        eastzone.neighbors_tiles.Add(tzone, l2);

                    }
                }


                if (south != null && tzone.get_id() != southzone.get_id())
                { //Could probably just be get_zone();
                    List<TilePair> ltp = null;
                    if (tzone.neighbors_tiles.TryGetValue(southzone, out ltp))
                    {
                        ltp.Add(new TilePair(t, south));
                        //assuming other zone has neighbor as well
                        List<TilePair> sltp = null;
                        southzone.neighbors_tiles.TryGetValue(tzone, out sltp);
                        sltp.Add(new TilePair(south, t));
                    }
                    else
                    {
                        Debug.Log("Did not have pair matching for " + tzone.get_id() + " " + southzone.get_id());
                        //Assuming the other zone does not have a list either
                        List<TilePair> l1 = new List<TilePair>();
                        List<TilePair> l2 = new List<TilePair>();

                        l1.Add(new TilePair(t, south));
                        l2.Add(new TilePair(south, t));
                        tzone.neighbors_tiles.Add(southzone, l1);
                        southzone.neighbors_tiles.Add(tzone, l2);

                    }
                }
            }
		}
	}


	//make sure all zones are connected through doors; place doors if not
	void check_access() {

	}

	byte get_num_walls(tile t)
	{
		byte count = 0;

		if(t.get_northwall() == tile.Wall.wall)
		{
			count++;	
		}

		if(t.get_westwall() == tile.Wall.wall)
		{
			count++;
		}
		if(t.get_eastwall() == tile.Wall.wall)
		{
			count++;
		}
		if(t.get_southwall() == tile.Wall.wall)
		{
			count++;
		}

		return count;
	}

	void find_alcoves()
	{
		Direction d = Direction.none;
        seed = DateTime.Now.ToString();
        System.Random rand = new System.Random(seed.GetHashCode());
        for (int y = 0; y < size; y++)
		{
			for(int x = 0; x < size; x++)
			{
				tile t = maze[y, x];

				byte count = 0;

				if(t.get_northwall() == tile.Wall.wall)
				{
					count++;	
				}
				else d = Direction.north;

				if(t.get_westwall() == tile.Wall.wall)
				{
					count++;
				}
				else d = Direction.west;

				if(t.get_eastwall() == tile.Wall.wall)
				{
					count++;
				}

				else d = Direction.east;
				if(t.get_southwall() == tile.Wall.wall)
				{
					count++;
				}
				else d = Direction.south;

				//It should max be 3.
				if (count == 3)
				{
					if(d == Direction.north && get_num_walls(maze[y-1,x]) == 2)
                    {
                        t.set_type(tile.Type.special);
                        continue;
                    }
                    else if(d == Direction.west && get_num_walls(maze[y,x-1]) == 2)
                    {
                        t.set_type(tile.Type.special);
                        continue;
                    }
                    else if(d == Direction.east && get_num_walls(maze[y,x+1]) == 2)
                    {
                        t.set_type(tile.Type.special);
                        continue;
                    }
                    else if(d == Direction.south && get_num_walls(maze[y+1,x]) == 2)
                    {
                        t.set_type(tile.Type.special);
                        continue;
                    }

                    t.set_type(tile.Type.alcove);
                    t.set_contains(alcove_objects[rand.Next(0,3)]);
				}					
			}
				
		}
	}

	void make_rooms()
	{
		bool x = make_room(4, 4, 2, 2 );
		Debug.Log(x);
	}
	
	//makes a "room" AKA an open space surrounded on all sides by walls
	bool make_room(int px, int py, int rx, int ry)
	{
		//Go to that position
		for (int y = py; y < py + ry; y++)
		{
			for (int x = px; x < px + rx; x++)
			{
				tile t = maze[y, x];
				t.set_status(tile.Status.maze);

				//If x == 0 set west wall
				if (x == px)
				{

					t.set_westwall(tile.Wall.wall);

					if (!((x - 1) < 0))
					{
						maze[y, x - 1].set_eastwall(tile.Wall.wall);
					}
				}
				else
				{
					t.set_westwall(tile.Wall.none);

					if (!((x - 1) < 0))
					{
						maze[y, x - 1].set_eastwall(tile.Wall.none);
					}
				}
				//if y == 0 set north wall
				if (y == py)
				{
					t.set_northwall(tile.Wall.wall);
					if (!((y - 1) < 0))
					{
						maze[y - 1, x].set_southwall(tile.Wall.wall);
					}
				}
				else
				{
					t.set_northwall(tile.Wall.none);
					if (!((y - 1) < 0))
					{
						maze[y - 1, x].set_southwall(tile.Wall.none);
					}
				}
				//if x == size set east wall
				if (x == px + rx - 1)
				{
					t.set_eastwall(tile.Wall.wall);
					if (!((x + 1) == size))
					{
						maze[y, x + 1].set_westwall(tile.Wall.wall);
					}
				}
				else
				{
					t.set_eastwall(tile.Wall.none);
					if (!((x + 1) == size))
					{
						maze[y, x + 1].set_westwall(tile.Wall.none);
					}
				}
				//if y == size set south wall
				if (y == py + ry - 1)
				{
					t.set_southwall(tile.Wall.wall);

					if (!((y + 1) == size))
					{
						maze[y + 1, x].set_northwall(tile.Wall.wall);
					}


				}
				else
				{
					t.set_southwall(tile.Wall.none);
					if (!((y + 1) == size))
					{
						maze[y + 1, x].set_northwall(tile.Wall.none);
					}
				}

			}
		}

		return true;
		//for loop through the array
		//set every tile to room tile
		//set every tile to have no walls (this would be an issue if it's an edge wall)

	}

    private void find_end_zone(Zone z, uint distance)
    {
        Debug.Log("giving zone " + z.get_id() + " a distance of " + distance);
        z.distance = distance;
        if (distance > end_zone.distance)
            end_zone = z;
        distance++;
        for (int i = 0; i < z.neighbors.GetLength(0);i++)
        {
            if(z.neighbors[i].distance > distance)
                find_end_zone(z.neighbors[i], distance);
        }   
    }

    private void key_king()
    {
        
    }

    void remove_room_walls(tile t)
	{
        
	}
}