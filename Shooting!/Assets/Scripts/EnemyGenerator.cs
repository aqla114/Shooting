using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] Enemy;  //敵のPrefabを入れる
    public GameObject Shooter;  //Shooter

    public float GenerateIntervalTime;  //敵を生成する間隔

    public float Generate_X_Range;  //敵生成の横幅

    float interval;  //Interval

	void Start ()
    {
        interval = 0;
	}
	
	void Update ()
    {
        interval += Time.deltaTime;

        if (interval > GenerateIntervalTime)
        {
            interval = 0.0f;
            Generate();
        }
	}

    //敵の生成
    void Generate()
    {
        Instantiate(
            Enemy[0], 
            new Vector3(
                transform.position.x + Random.Range(-Generate_X_Range, Generate_X_Range), 
                transform.position.y, 
                transform.position.z),
            Quaternion.identity
            );
    }

}
