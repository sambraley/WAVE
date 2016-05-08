using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
    Animator door_animation;
    public int number;
    public AudioClip door_sound;
    AudioSource sound;    

    // Use this for initialization
    void Start () {
        door_animation = GetComponent<Animator>();
        sound = gameObject.AddComponent<AudioSource>();
        number = 1;
    }
	
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
            col.gameObject.SendMessage("at_door", gameObject);
            col.gameObject.SendMessage("has_key");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
            col.gameObject.SendMessage("null_door");
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

    void does_have_key()
    {
        Debug.Log("Opening door #" + number);
        door_animation.SetBool("open", true);
        BoxCollider[] colliders = gameObject.GetComponents<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            if (collider.isTrigger) //destroy the trigger
            {
                Destroy(collider);
            }
            else //shift the regular collider to match the animation
            {
                collider.center = new Vector3(-1.8f,0.1f,0);
                collider.size = new Vector3(3.4f,0.2f,2);
            }
        }
        sound.clip = door_sound;
        sound.Play();
    }
}
