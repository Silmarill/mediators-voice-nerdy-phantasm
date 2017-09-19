using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public int maxPlayerHealth;
    public static int playerHealth;

    public Text txtHealt;

    private LevelManager _lvlManager;

    
    void Start() {
       // txtHealt = GetComponent <Text>();
       
        _lvlManager = FindObjectOfType <LevelManager>();
        FullHealth();
    }


    // TODO: Replace to observer
    void Update() {
        if (playerHealth <= 0) {
            playerHealth = 0;
            _lvlManager.RespawnPlayer();
            FullHealth();

             
        }

        txtHealt.text = playerHealth.ToString();
       
    }


    private void Reset() {
        playerHealth = maxPlayerHealth;
    }

    public static void HurtPlayer(int damageToGive) {
        playerHealth -= damageToGive;
    }

    public void FullHealth() {
        playerHealth = maxPlayerHealth;
    }
}
