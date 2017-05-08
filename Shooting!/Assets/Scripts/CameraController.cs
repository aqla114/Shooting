using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    Vector3 diff;  //最初のカメラとShooterの位置関係

    public GameObject target;  //Shooterを登録
    public float followspeed;  //カメラが追うスピード

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
