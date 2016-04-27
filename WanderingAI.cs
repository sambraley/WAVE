using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour
{
	public const float baseSpeed = 3.0f;
	public float speed = 1.0f;
	float obstacleRange = 1.5f;
	private bool _alive;
	[SerializeField]
	private GameObject _fireballPrefab;
	private GameObject _fireball;
	private float _lineOfSightRadius;

	void Start ()
	{
		_alive = true;
		_lineOfSightRadius = 1.0f;
	}

//	void Awake() {
//		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
//	}
//
//	void OnDestroy() {
//		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
//	}

	private void OnSpeedChanged(float value) {
		speed = baseSpeed * value;
	}

	// Update is called once per frame
	void Update ()
	{
		if (_alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.SphereCast (ray, _lineOfSightRadius , out hit)) {
				GameObject hitObject = hit.transform.gameObject;


				if (hitObject.CompareTag("Player")) {
					if (_fireball == null) {
						_fireball = Instantiate (_fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.7f);
						_fireball.transform.rotation = transform.rotation;

					}
				} else if (hit.distance < obstacleRange) {
					float rotationAngle = Random.Range (-110, 110);
					transform.Rotate (0, rotationAngle, 0);
				}
			}
		}
	}

	//This is a setter for alive value, encapsulation!
	public void SetAlive (bool value)
	{
		_alive = value;
	}
}
