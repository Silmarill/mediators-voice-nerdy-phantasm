using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public SceneAsset   levelSelect;
    public SceneAsset   mainMenu;

    public bool isPaused;
    public GameObject pauseMenuCanvas;


    void Update() {
        if (isPaused) {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        } else {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused = !isPaused;
        }
    }

    public void ResumeGame() {
        isPaused = false;
    }

    public void LevelSelect() {
        SceneManager.LoadScene(levelSelect.name);
    }

    public void QuitToMain() {
          SceneManager.LoadScene(mainMenu.name);
    }
}
