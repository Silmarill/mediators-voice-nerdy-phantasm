using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public SceneAsset   startLevel;
    public SceneAsset   levelSelect;

    public void NewGame() {
        SceneManager.LoadScene(startLevel.name);
    }

    public void LevelSelect() {
        SceneManager.LoadScene(levelSelect.name);
    }

    public void QuitGame() {
        Application.Quit();
    }



}
