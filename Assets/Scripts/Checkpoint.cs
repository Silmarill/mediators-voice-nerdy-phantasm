using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private LevelManager levelManager;
    
    void Start() {
        levelManager = FindObjectOfType <LevelManager>();
    }



    void OnTriggerEnter2D(Collider2D senpai) {
        if (senpai.name == "MY_HERO") {
            Debug.Log("Checkpoint Reached!" + transform.position);
            //this - скрипть Checkpoint.cs 
            //gameObject - объект на котором он висит (можно использовать без this)
            levelManager.currentCheckpoint = this.gameObject;
        }
    }
}
