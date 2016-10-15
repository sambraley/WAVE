using UnityEngine;
using System.Collections;

public class TilePair {


	tile tile_one;
	tile tile_two;

	//May need to make this more sorted, so that most upper left is always tile_one 
	public TilePair(tile t1, tile t2)
	{
		tile_one = t1;
		tile_two = t2;
	}


}
