using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{

    public Text ScoreText;  //Scoreのテキスト

    static int score;  //実際のScore
    

	void Start ()
    {
        score = 0;
	}

    void Update()
    {
        //スコアを表示
        ScoreText.text = "Score:" + score;

        //爆発エフェクトを削除
        Destroy(GameObject.Find("Explosion07(Clone)"), 0.5f);
    }

    public void UpdateScore()
    {
        score++;
    }
}
