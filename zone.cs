using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone {

	uint id;
	public Dictionary<Zone, List<TilePair>> neighbors_tiles; // public for now because it's simpler for testing
	public Zone[] neighbors;
	List<tile> tiles; // List of tiles within this zone
	List<Zone> connected; // Zones that are actually connected by a door


	public Zone(uint id)
	{
		this.id = id;
		neighbors_tiles = new Dictionary<Zone, List<TilePair>>();
		tiles = new List<tile>();
		connected = new List<Zone>();
		neighbors = null;


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
