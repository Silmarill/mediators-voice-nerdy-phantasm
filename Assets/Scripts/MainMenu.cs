using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int playerStartLives;
    public int playerStartScore;

    public int playerStartHealth;
    public int playerCurrentHealth;

    public string startLevel;
    public string levelSelect;

    public void NewGame() {
        PlayerPrefs.SetInt("PlayerLives", playerStartLives);
        PlayerPrefs.SetInt("Score", playerStartScore);

        PlayerPrefs.SetInt("MaxHealth", playerStartHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerCurrentHealth);

        SceneManager.LoadScene(startLevel);
    }

    public void LevelSelect() {
        PlayerPrefs.SetInt("PlayerLives", playerStartLives);
        PlayerPrefs.SetInt("Score", playerStartScore);

        PlayerPrefs.SetInt("MaxHealth", playerStartHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerCurrentHealth);

        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame() {
        Application.Quit();
    }



}
