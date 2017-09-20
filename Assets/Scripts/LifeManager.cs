using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public int startingLives;
    private int lifeCounter;

    private Text txtLifes;

    public GameObject gameOverScreen;
    private PlayerController player;

    public SceneAsset mainMenu;
    public float timeToWaitAfterGameOver;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        txtLifes = GetComponent <Text>();
        lifeCounter = startingLives;
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
                SceneManager.LoadScene(mainMenu.name);
            }
        }
    }

    public void GiveLife() {
        ++lifeCounter;
        RefreshLive();
    }

    public void TakeLife() {
        --lifeCounter;
        RefreshLive();
    }
}
