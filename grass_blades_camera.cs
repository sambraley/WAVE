using UnityEngine;
using System.Collections;

public class grass_blades_camera : MonoBehaviour {

    public Camera m_Camera;

    // Use this for initialization
    void Start () {
        m_Camera = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Camera != null)
        {
            transform.LookAt(transform.position - m_Camera.transform.rotation * Vector3.forward); //, m_Camera.transform.rotation * Vector3.up
            //removed up stuff so that the grass won't rotate in the x or z direction
        }
        else
        {
            m_Camera = GameObject.Find("player_character(Clone)").GetComponentInChildren<Camera>();
            if (m_Camera == null)
            {
                Debug.Log("fuckw");
            }
        }
    }
}
