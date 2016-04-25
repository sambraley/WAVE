using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour
{
    public enum Type {grass, deadspace, normal, alcove};
    public enum Status {none, maze, frontier};
    public enum Wall {none, door, wall};

    Wall northwall;
    Wall westwall;
    Wall southwall;
    Wall eastwall;

    Status status;

    Type type;
    //Vector3 pos;
    //int zone;
	uint zone;
	bool touched;

    public tile()
    {
        northwall = Wall.wall;
        westwall = Wall.wall;
        southwall = Wall.wall;
        eastwall = Wall.wall;
        type = Type.normal;
        status = Status.none;
		zone = 0;
    }

    public Wall get_northwall(){return northwall;}
    public Wall get_westwall(){return westwall;}
    public Wall get_southwall(){return southwall;}
    public Wall get_eastwall(){return eastwall;}
    public void set_northwall(Wall val){northwall = val;}
    public void set_southwall(Wall val) { southwall = val; }
    public void set_westwall(Wall val) { westwall = val; }
    public void set_eastwall(Wall val) { eastwall = val; }

    public Status get_status() { return status; }
    public void set_status(Status val) { status = val; }

	public Type get_type() { return type; }
	public void set_type(Type val) { type = val; }

	public uint get_zone() { return zone; }
	public void set_zone(uint z) { zone = z; }

	public bool get_touch() { return touched; }
	//I want this to be idempotent for now
	public void touch(bool b) { touched =  b; }


    void Start ()
    {

    }

    void Update ()
    {
   
    }
   
}