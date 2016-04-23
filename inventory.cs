using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventory : MonoBehaviour {
    public Dictionary<string, bool> keys;
    public AudioClip collect_sound;
    AudioSource sound;

    // Use this for initialization
    void Start () {
        keys = new Dictionary<string, bool>();
        sound = gameObject.AddComponent<AudioSource>(); //have to add this for overlapping sounds
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    //add to the keys list
    void got_key(string key)
    {
        Debug.Log("adding " + key + " to inventory");
        keys[key] = true;
        sound.clip = collect_sound;
        sound.Play();
    }
}
