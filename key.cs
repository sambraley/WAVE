using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {
    public int number;

    void Start()
    {
        //number = 1;
        transform.Translate(new Vector3(0, 1, 0));
        transform.Rotate(new Vector3(45,0,0));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
            Debug.Log("detected key collision");
            col.gameObject.SendMessage("got_key", number); //also plays collect sound
            //have to do this message because if we attempt to play the sound in here, 
            //it'll get cut off by the fact that the game object gets destroyed
            Destroy(gameObject); //remove this key now
        }
    }
    void change_color(Material mat)
    {
        GetComponent<Renderer>().material.CopyPropertiesFromMaterial(mat);
    }
    void set_number(int num) //for indentification
    {
        number = num;
    }
}
