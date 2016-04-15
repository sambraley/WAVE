using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

   enum types {grass, deadspace, normal};
   enum status {touched, untouched, frontier};
   enum wall {none, door, wall};

   int northwall;
   int westwall;
   int southwall;
   int eastwall;

   int type;
   Vector3 pos;
   int zone;

   public tile(Vector3 position){
      northwall = wall;
      westwall = wall;
      southwall = wall;
      eastwall = wall;
      type = types.normal;
      pos = position;
   }

   void Start () {

   }

   void Update () {
   
   }
   
}