using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShooterControler : MonoBehaviour
{
    public GameObject Explosion;  //爆発のエフェクト
    public GameObject ExplosionSoundPlayer; //爆発音発生Object
    public GameObject GameOver;  //GameOverの文字を取得

    public float X_Speed = 1.0f;  //x方向の速度
    public float Z_Speed = 1.0f;  //z方向の速度
    public float X_Max = 9.5f;
    public float Z_Max = 4.5f;

    public float Rotate_Speed = 1.0f;  //回転の速度
    public float MaxRotation = 20.0f;  //最大回転角

    public GameObject[] PlayerBullets;  //弾のprefab
    public float[] ShootIntervalTimes; //弾発射のインターバル

    AudioSource shootSound;  //発射音
    float interval;  //Interval
    int state = 2;  //Shooterのモード


	void Start ()
    {
        //AudioSourceの取得
        shootSound = GetComponent<AudioSource>();

        //ゲーム開始直後から弾を撃てるようにintervalを与えておく
        interval = 10.0f;

	}
	
	void Update ()
    {
        //横方向変化量分のベクトル
        Vector3 diff = new Vector3();
        diff.x = Input.GetAxis("Horizontal");
        diff.z = Input.GetAxis("Vertical");

        if (Input.GetKey("up"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + diff, Time.deltaTime * Z_Speed);
        }
        else if (Input.GetKey("down"))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + diff, Time.deltaTime * Z_Speed);
        }
        else if (Input.GetKey("left"))
        {
            if (transform.eulerAngles.z < MaxRotation)
            {
                transform.eulerAngles = 
                    new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, MaxRotation, Rotate_Speed * Time.deltaTime));
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + diff, Time.deltaTime * X_Speed);
        }
        else if (Input.GetKey("right"))
        {
            if (transform.eulerAngles.z > -MaxRotation)
            {
                transform.eulerAngles = 
                    new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, -MaxRotation, Rotate_Speed * Time.deltaTime));
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + diff, Time.deltaTime * X_Speed);
        }


        //位置が領域端を越えないように調整
        transform.position = (
            new Vector3 (
                Mathf.Clamp(transform.position.x, -X_Max, X_Max),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -Z_Max, Z_Max)
            )
            );

        //機体の回転角を初期値に戻す
        if (Mathf.Abs(transform.rotation.z) > 0)
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, 0.0f, Rotate_Speed * Time.deltaTime));

        //弾の発射
        interval += Time.deltaTime;
        if (Input.GetKey("space"))
        {
            if(interval > ShootIntervalTimes[state])
            { 
                interval = 0.0f;
                Shoot(state);
            }
        }

        
        //弾の変更
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = (state + 1) % 3;
        }

        //Debug.Log(state);
    }

    //弾もしくはEnemyに当たって爆発する
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "EnemyBullet" || collider.gameObject.tag == "Enemy")
        {
            //爆発エフェクトの発生
            Instantiate(Explosion, transform.position, Quaternion.identity);

            //爆発音再生Objectを生成
            Instantiate(ExplosionSoundPlayer, transform.position, Quaternion.identity);

            //GameOverを表示する
            GameOver.SetActive(true);

            //タイトル画面に戻る
            Invoke("ReturnToTitle", 3.0f);

            //Objectを破壊する(無効にする)
            this.gameObject.SetActive(false);
        }
    }

    //弾を発射する関数
    public void Shoot(int state)
    {
        PlayerBullets[state].gameObject.tag = "PlayerBullet";

        switch (state)
        {
            case 0:  //炎
                Instantiate(PlayerBullets[0], transform.position, Quaternion.identity);
                break;
            case 1:  //雑魚弾（水色）
                Instantiate(PlayerBullets[1], transform.position, Quaternion.Euler(90, 0, 0));
                break;
            case 2:  //レーザー
                Instantiate(PlayerBullets[2], transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        shootSound.Play();
    }

    //タイトル画面に戻る関数
    public void ReturnToTitle()
    {
        Application.LoadLevel("Title");
    }
}
