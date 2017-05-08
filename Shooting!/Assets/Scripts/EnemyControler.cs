using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyControler : MonoBehaviour {

    public GameObject[] EnemyBullets;  //Enemyの出す弾
    public GameObject Explosion;  //爆発エフェクト
    public GameObject ExplosionSoundPlayer;  //爆発音発生Object
    GameObject MainCanvas;
    public GameObject TargetIcon; //TargetIconの画像
    GameObject GameMgr;  //GameMgr

    public float Z_Speed = 0.7f;  //Enemyのz方向の移動速度
    public float X_Range = 0.5f;  //Enemyのx方向の移動範囲
    public float X_Speed = 0.1f;  //Enemyのx方向の移動速度

    public float ShootIntervalTime;  //弾を出すインターバル
    public float EnemyType;  //敵の種類

    int state = 0;  //敵のモード（弾の撃ち方）
    float interval;  //Interval
    float x_moving, z_moving; //x,zの移動量
    Vector3 defaultPosition;  //敵の初期位置
    Quaternion bulletQuat = Quaternion.Euler(-90, 0, 0);  //弾の発射方向

    GameObject targetIcon;

	void Start ()
    {
        interval = 0;
        x_moving = 0;
        z_moving = 0;
        defaultPosition = transform.position;

        //GameMgrを取得する
        GameMgr = GameObject.Find("GameMgr");

        //MainCanvasを取得する
        MainCanvas = GameObject.Find("MainCanvas");

        //生成と同時にiconをCanvasに表示
        targetIcon = (GameObject)Instantiate(
            TargetIcon,
            new Vector3(transform.position.x * 53.0f, transform.position.z * 29.0f, 0), 
            Quaternion.identity);

        //TargetIconをCanvasの子に設定
        targetIcon.transform.SetParent(MainCanvas.transform, false);

    }
	
	void Update ()
    {
        //移動
        Move();

        //インターバル時間を加算して弾を撃つ
        interval += Time.deltaTime;
        if (interval >= ShootIntervalTime)
        {
            interval = 0.0f;
            Shoot();
        }
	
	}

    //弾もしくはPlayerに当たって爆発する
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "PlayerBullet" || collider.gameObject.tag == "Shooter")
        {
            //爆発エフェクト発生
            GameObject explosion = (GameObject)Instantiate(Explosion, transform.position, Quaternion.identity);

            //爆発音再生Objectを生成
            Instantiate(ExplosionSoundPlayer, transform.position, Quaternion.identity);

            //scoreの加算
            GameMgr.GetComponent<GameMgr>().UpdateScore();

            //Objectの破壊
            Destroy(this.gameObject);
        }
    }

    //敵が移動する関数
    void Move()
    {
        transform.position = defaultPosition + new Vector3(X_Range * Mathf.Sin(x_moving += Time.deltaTime * X_Speed), 0, z_moving -= Z_Speed);
    }

    //敵が弾を撃つ関数
    void Shoot()
    {
        EnemyBullets[state].gameObject.tag = "EnemyBullet";
        Instantiate(EnemyBullets[0], transform.position, bulletQuat);
    }
}
