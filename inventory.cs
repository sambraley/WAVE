using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour {
    bool[] key_ring;
    public AudioClip collect_sound;
    AudioSource sound;
    GameObject door;

    // Use this for initialization
    void Start () {
        key_ring = new bool[10];
        sound = gameObject.AddComponent<AudioSource>(); //have to add this for overlapping sounds
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //add to the keys list
    void got_key(int key)
    {
        Debug.Log("adding key #" + key + " to inventory");
        key_ring[key] = true;
        sound.clip = collect_sound;
        sound.Play();
    }

    void at_door(GameObject door)
    {
        this.door = door;
    }

    void null_door()
    {
        door = null;
    }

    void has_key(int key)
    {
        if (key_ring[key])
        {
            if (door)
            {
                door.SendMessage("does_have_key");
            }
            else
            {
                Debug.Log("PANIC");
            }
        }
    }
}
