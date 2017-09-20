using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private Text text;


    void Start() {
        text = GetComponent <Text>();
        Reset();
    }



    void Update() {
        if (score < 0) {
            score = 0;
        }

        text.text = score.ToString();
    }

    

    public static void AddPoints(int pointsToAdd) {
        score += pointsToAdd;
        PlayerPrefs.SetInt("Score", score);
    }




    public static void Reset() {
        score = PlayerPrefs.GetInt("Score");
    }

}
