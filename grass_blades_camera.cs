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
            transform.LookAt(new Vector3(m_Camera.transform.position.x, 0, m_Camera.transform.position.z)); 
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
