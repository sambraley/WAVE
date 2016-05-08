using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone {

    uint id = 0;
	public Dictionary<Zone, List<TilePair>> neighbors_tiles; // public for now because it's simpler for testing
	public List<tile> tiles; // List of tiles within this zone, TODO separate this into normal, alcove and special tiles
	public List<Zone> connected; // Zones that are actually connected by a door
	public List<tile> special_tiles;
    public Zone[] neighbors;
    public uint distance;
	public bool touched; 

	public int door;
	public int key;

	public Zone(uint id)
	{
        this.id = id;
		neighbors_tiles = new Dictionary<Zone, List<TilePair>>();
		tiles = new List<tile>();
		connected = new List<Zone>();
		special_tiles = new List<tile>();
        distance = uint.MaxValue;
		touched = false;

		door = -1;
		key = -1;
	}

	public uint get_id() {return id;}


	public void add_tile(tile t)
	{
		if(t != null)
		{
			tiles.Add(t);
		}

	}


	//Connected methods




		
}
