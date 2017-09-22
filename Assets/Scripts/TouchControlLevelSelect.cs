using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControlLevelSelect : MonoBehaviour {

    public LevelSelectManager lsm;

    // Use this for initialization
    void Start() {
        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        gameObject.SetActive(false);
        return;
        #endif
        lsm = FindObjectOfType <LevelSelectManager>();
        lsm.isToucheModeEnabled = true;
    }

    public void MoveLeft() {
        --lsm.posIndex;

        if (lsm.posIndex < 0) {
            lsm.posIndex = 0;
        }
    }

    public void MoveRight() {
        ++lsm.posIndex;

        if (lsm.posIndex >= lsm.levelTags.Length) {
            lsm.posIndex = lsm.levelTags.Length - 1;
        }
    }

    public void Select() {
        if (lsm.levelUnlocked[lsm.posIndex]) {
            PlayerPrefs.SetInt("LevelIndexPosStore", lsm.posIndex);
            SceneManager.LoadScene(lsm.levelNames[lsm.posIndex]);
        }
    }
}
