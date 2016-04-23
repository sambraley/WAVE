using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
            Debug.Log("detected key collision");
            col.gameObject.SendMessage("got_key", "orange"); //also plays collect sound
            //have to do this message because if we attempt to play the sound in here, 
            //it'll get cut off by the fact that the game object gets destroyed
            Destroy(gameObject); //remove this key now
        }
    }
}
