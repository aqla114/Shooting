using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {

    public Text ScoreText;  //Scoreのテキスト

    static int score;  //実際のScore
    

	void Start ()
    {
        score = 0;
	}

    void Update()
    {
        ScoreText.text = "Score:" + score;
        //Debug.Log(score);
    }

    public void UpdateScore()
    {
        score++;
    }
}
