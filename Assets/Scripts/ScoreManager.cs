using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private Text text;


    void Start() {
        Messenger.AddListener<int>("AddPoints",AddPoints);
        text = GetComponent <Text>();
        Reset();
    }

    void Destroy() {
        Messenger.RemoveListener<int>("AddPoints",AddPoints);
    }



    void Update() {
        if (score < 0) {
            score = 0;
        }

        text.text = score.ToString();
    }
    

    public void AddPoints(int pointsToAdd) {
        score += pointsToAdd;
        PlayerPrefs.SetInt("Score", score);
    }
    
    public void Reset() {
        score = PlayerPrefs.GetInt("Score");
    }

}
