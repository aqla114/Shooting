using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float BulletSpeed = 5.0f;  //弾の速度
    public float BulletTime = 5.0f;  //弾の寿命
    public string BulletType;

	void Start ()
    {
        Destroy(this.gameObject, BulletTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BulletType == "Bullet2")
        {
            transform.Translate(0, BulletSpeed, 0);
        }
        else
        {
            transform.Translate(0, 0, BulletSpeed);
        }
	}
}
