using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Vector3 diff;

    public GameObject target;
    public float followspeed;

	void Start ()
    {
        diff = target.transform.position - transform.position;
	}
	
	void Update ()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followspeed
        );
	}
}
