﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

 
    private int lifeCounter;

    private Text txtLifes;

    public GameObject gameOverScreen;
    private PlayerController player;

    public string mainMenu;
    public float timeToWaitAfterGameOver;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        txtLifes = GetComponent <Text>();
        lifeCounter = PlayerPrefs.GetInt("PlayerLives");
        RefreshLive();

    }
    
    // Update is called once per frame
    void RefreshLive () {
        if (lifeCounter < 0) {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }

        txtLifes.text = "x " + lifeCounter;
    }

    void Update() {
        if (gameOverScreen.activeSelf) {
           timeToWaitAfterGameOver -= Time.deltaTime;
            if (timeToWaitAfterGameOver < 0) {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }

    public void GiveLife() {
        ++lifeCounter;
        PlayerPrefs.SetInt("PlayerLives", lifeCounter); 
        RefreshLive();
    }

    public void TakeLife() {
        --lifeCounter;
        PlayerPrefs.SetInt("PlayerLives", lifeCounter); 
        RefreshLive();
    }
}
