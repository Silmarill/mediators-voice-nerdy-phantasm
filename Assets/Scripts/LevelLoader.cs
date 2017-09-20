using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    private bool isPlayerInZone;
    public string levelToLoad;

    // Use this for initialization
    void Start () {
        isPlayerInZone = false;
    }

    void Update() {
        if (Input.GetAxisRaw("Vertical") < 0 && isPlayerInZone) {
            //Method to switch scenes
           
            SceneManager.LoadScene(levelToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "MY_HERO") {
            isPlayerInZone = true;
        }
    }

     void OnTriggerExit2D(Collider2D other) {
        if (other.name == "MY_HERO") {
            isPlayerInZone = false;
        }
    }
}
