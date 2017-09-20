using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int playerStartLives;
    public int playerStartScore;

    public int playerStartHealth;
    public int playerCurrentHealth;

    public SceneAsset startLevel;
    public SceneAsset levelSelect;

    public void NewGame() {
        PlayerPrefs.SetInt("PlayerLives", playerStartLives);
        PlayerPrefs.SetInt("Score", playerStartScore);

        PlayerPrefs.SetInt("MaxHealth", playerStartHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerCurrentHealth);

        SceneManager.LoadScene(startLevel.name);
    }

    public void LevelSelect() {
        PlayerPrefs.SetInt("PlayerLives", playerStartLives);
        PlayerPrefs.SetInt("Score", playerStartScore);

        PlayerPrefs.SetInt("MaxHealth", playerStartHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerCurrentHealth);

        SceneManager.LoadScene(levelSelect.name);
    }

    public void QuitGame() {
        Application.Quit();
    }



}
