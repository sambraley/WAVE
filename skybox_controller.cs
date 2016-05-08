using UnityEngine;
using System.Collections;
using System;

public class skybox_controller : MonoBehaviour {
    public Material[] skybox;
	string seed;

	// Use this for initialization
	void Awake () {
		seed = DateTime.Now.ToString ();
		System.Random rand = new System.Random (seed.GetHashCode ());
		int length = skybox.Length;

		Material s = skybox[rand.Next(0, length-1)];
        RenderSettings.skybox = s;
	}
	
	// Update is called once per frame
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1);
	}
}
