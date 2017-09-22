using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string Level1Tag;

    public int playerStartLives;
    public int playerStartScore;

    public int playerStartHealth;
    public int playerCurrentHealth;

    public string startLevel;
    public string levelSelect;

  

    private void SetupGame() {
         //TODO: get from JSON
        PlayerPrefs.SetInt(Level1Tag, 1);

        PlayerPrefs.SetInt("PlayerLives", playerStartLives);
        PlayerPrefs.SetInt("Score", playerStartScore);

        PlayerPrefs.SetInt("MaxHealth", playerStartHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerCurrentHealth);

        if (!PlayerPrefs.HasKey("LevelIndexPosStore")) {
            PlayerPrefs.SetInt("LevelIndexPosStore", 0);
        }

        
    }

    public void NewGame() {
        SetupGame();
      SceneManager.LoadScene(startLevel);
    }

    public void LevelSelect() {
        SetupGame();
        SceneManager.LoadScene(levelSelect);
    }

    public void LoadCustomLevelWithSetup(string levelName) {
        SetupGame();
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame() {
        Application.Quit();
    }



}
