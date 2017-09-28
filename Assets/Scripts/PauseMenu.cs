using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string  levelSelect;
    public string   mainMenu;

    public static bool isPaused;
    public GameObject pauseMenuCanvas;

    


    void Update() {
        if (isPaused) {
            pauseMenuCanvas.SetActive(true);       
        } else {
            pauseMenuCanvas.SetActive(false);
        }

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            Messenger.Broadcast("PauseStatus", isPaused);
        }
        #endif
    }

    public void TogglePause() {
        isPaused = !isPaused;
    }

    public void ResumeGame() {
        isPaused = false;
        Messenger.Broadcast("PauseStatus", isPaused);
    }

    public void LevelSelect() {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMain() {
          SceneManager.LoadScene(mainMenu);
    }
}
