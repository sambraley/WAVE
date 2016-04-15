using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour
{

    public enum types {grass, deadspace, normal};
    public enum status {touched, untouched, frontier};
    public enum wall {none, door, wall};

    int northwall;
    int westwall;
    int southwall;
    int eastwall;

    int type;
    Vector3 pos;
    int zone;

    public tile(Vector3 position)
    {
      pos = position;
      northwall = (int)wall.wall;
      westwall = (int)wall.wall;
      southwall = (int)wall.wall;
      eastwall = (int)wall.wall;
      type = (int)types.normal;
    }

    public int get_northwall(){return northwall;}
    public int get_westwall(){return westwall;}
    public int get_southwall(){return southwall;}
    public int get_eastwall(){return eastwall;}
    public void set_northwall(int val){northwall = val;}
    public void set_southwall(int val) { southwall = val; }
    public void set_westwall(int val) { westwall = val; }
    public void set_eastwall(int val) { eastwall = val; }

    void Start ()
    {

    }

    void Update ()
    {
   
    }
   
}