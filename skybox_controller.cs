using UnityEngine;
using System.Collections;

public class skybox_controller : MonoBehaviour {
    public Material skybox;

	// Use this for initialization
	void Awake () {
        RenderSettings.skybox = skybox;
	}
	
	// Update is called once per frame
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1);
	}
}
