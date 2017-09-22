using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string  levelSelect;
    public string   mainMenu;

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

       #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
         if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
         }
        #endif
    }

    public void TogglePause() {
        isPaused = !isPaused;
    }

    public void ResumeGame() {
        isPaused = false;
    }

    public void LevelSelect() {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMain() {
          SceneManager.LoadScene(mainMenu);
    }
}
