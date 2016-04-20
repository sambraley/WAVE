using UnityEngine;
using System.Collections;

public class key_rotate : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.RotateAround(new Vector3(transform.localPosition.x, 0, transform.localPosition.z), Vector3.up, Time.deltaTime * 100);
    }
}
