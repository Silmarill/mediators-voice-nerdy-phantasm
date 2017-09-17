﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    
    private LevelManager levelManager;

    void Start () {
        levelManager = FindObjectOfType <LevelManager>();
    }



    void OnTriggerEnter2D(Collider2D senpai) {
        if (senpai.name == "MY_HERO") {
           levelManager.RespawnPlayer();
        }
    }

}
