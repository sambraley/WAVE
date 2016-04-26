using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone {

	static uint id = 0;
	//List of neighbors zones that are not YET connected with a door
	//list of tiles within this zone
	List<Zone> connected;


	public uint get_id() {return id;}

	//Connected methods

	public Zone()
	{
		id++;
		connected = new List<Zone>();
	}


		
}
