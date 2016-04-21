using UnityEngine;
using System.Collections;

public class ladder_script : MonoBehaviour {
    GameObject playerObject;

    // Use this for initialization
    void Start() {
        playerObject = GameObject.Find("player_character");
        if (playerObject != null)
            Debug.Log("found player");
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
        if (col.gameObject == playerObject)
        {
        Debug.Log("detected collision");
            playerObject.SendMessage("canClimb");//.GetComponent("player_ladder_script").canClimb();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == playerObject)
        {
        Debug.Log("detected collision exit");
            playerObject.SendMessage("cantClimb");
        }
    }
}
