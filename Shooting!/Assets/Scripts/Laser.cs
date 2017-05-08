using UnityEngine;
using System.Collections;
using VolumetricLines;

public class Laser : MonoBehaviour
{
    GameObject Shooter; //自機
    public float LaserSpeed = 1.0f;  //弾の速度
    public float LaserTime = 2.0f;  //弾の寿命

    VolumetricLineBehavior LineBehavior;  //VLBクラス
    Vector3 LaserExpansion;
    BoxCollider LaserCollider;
   

    void Start ()
    {
        //レーザーの伸びる速度ベクトル
        LaserExpansion = new Vector3(0, 0, LaserSpeed);

        //VLBクラスを取得
        LineBehavior = GetComponent<VolumetricLineBehavior>();
        
        //Shooterを取得
        Shooter = GameObject.Find("omega_fighter");

        //Colliderの取得
        LaserCollider = GetComponent<BoxCollider>();

        //寿命時間後にレーザーを消す
        Destroy(this.gameObject, LaserTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //自機から常にレーザーが出るように
        transform.position = Shooter.transform.position;

        //長さを延ばしていく
        LineBehavior.EndPos += LaserExpansion;
        LaserCollider.center = (LineBehavior.EndPos - LineBehavior.StartPos) / 2;
        LaserCollider.size += LaserExpansion;
	}
}
