using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour
{
    public enum Type {grass, deadspace, normal};
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

    public tile()
    {
        northwall = Wall.wall;
        westwall = Wall.wall;
        southwall = Wall.wall;
        eastwall = Wall.wall;
        type = Type.normal;
        status = Status.none;
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

    void Start ()
    {

    }

    void Update ()
    {
   
    }
   
}