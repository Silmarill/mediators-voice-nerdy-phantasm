using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public float startingTime;
    private Text txtTimer;

    private PauseMenu _pauseMenu;


    public GameObject gameOverScreen;
    private PlayerController player;

    private HealthManager _hpManager;


    private float countingTime;
    
    void Start () {
        countingTime = startingTime;
        txtTimer = GetComponent <Text>();
        _hpManager = FindObjectOfType<HealthManager>();
        player = FindObjectOfType<PlayerController>();
        _pauseMenu = FindObjectOfType <PauseMenu>();
    }
    

    void Update () {
        if (_pauseMenu.isPaused) {
            return;
        }


        if (countingTime <= 0) {
            _hpManager.KillPlayer();
            //gameOverScreen.SetActive(true);
            //player.gameObject.SetActive(false);
        } else {
             countingTime -= Time.deltaTime;
             txtTimer.text = Mathf.Round(countingTime).ToString();
        }
        
    }

    public void ResetTime() {
        countingTime = startingTime;
    }


}
