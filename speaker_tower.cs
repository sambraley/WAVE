using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class speaker_tower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.Translate(0f, 2.25f, 0f);
		gameObject.transform.Rotate(-90, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player_character(Clone)")
        {
//            Debug.Log("Killing the game.");
//            Application.Quit();
			SceneManager.LoadScene("maze_generation");

        }
    }
}
