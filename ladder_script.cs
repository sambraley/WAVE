using UnityEngine;
using System.Collections;

public class ladder_script : MonoBehaviour {
    GameObject playerObject;

    // Use this for initialization
    void Start() {
        if (GetComponent<Collider>().isTrigger)
        {
            Debug.Log("this is a trigger");
        }
        else
        {
            Debug.Log("this ain't a trigger");
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
        Debug.Log("detected collision");
            col.gameObject.SendMessage("canClimb");//.GetComponent("player_ladder_script").canClimb();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
        Debug.Log("detected collision exit");
            col.gameObject.SendMessage("cantClimb");
        }
    }
}
