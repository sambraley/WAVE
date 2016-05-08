using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour {
    int key_ring;
    public AudioClip collect_sound;
    public AudioClip door_sound;
    AudioSource sound;
    GameObject door;

    // Use this for initialization
    void Start () {
		key_ring = 100;
        sound = gameObject.AddComponent<AudioSource>(); //have to add this for overlapping sounds
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //add to the keys list
    void got_key()
    {
		key_ring++;
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

    void has_key()
    {
		if (key_ring != 0)
        {
            if (door)
            {
                door.SendMessage("does_have_key");
                sound.clip = door_sound;
                sound.Play();
				key_ring--;
            }
            else
            {
                Debug.Log("PANIC");
            }
        }
    }
}
