using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.IO;

public class MainMenu : MonoBehaviour {

    public string Level1Tag;

    public int playerStartLives;
    public int playerStartScore;

    public int playerStartHealth;
    public int playerCurrentHealth;

    public string startLevel;
    public string levelSelect;

    private String json;
    private JSONNode jsonNode;
    private TextAsset jsonFile;
    private readonly string FILE_NAME = "settings";
    private string playerName;
    private int playerLives;
    private int score;
    private int maxHealth;
    private int currentHealth;

    private void SetupGame() {
        jsonFile = (TextAsset)Resources.Load(FILE_NAME);
          
        string theJsonText = jsonFile.text;      
        jsonNode = JSON.Parse(theJsonText);
    

        var playerStatus = jsonNode["Player"];
        playerName = playerStatus["PlayerName"];
        playerLives = playerStatus["PlayerLives"].AsInt;
        score = playerStatus["Score"].AsInt;           
        maxHealth = playerStatus["MaxHealth"].AsInt;
        currentHealth = playerStatus["CurrentHealth"].AsInt;


        PlayerPrefs.SetInt(Level1Tag, 1);

        PlayerPrefs.SetInt("PlayerLives", playerLives);
        PlayerPrefs.SetInt("Score", score);

        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);

        if (!(jsonNode["LevelIndexPosStore"] != null)) {
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
