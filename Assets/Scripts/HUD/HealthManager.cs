using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {


    public static int playerHealth;

    private Text txtHealt;

    private LevelManager _lvlManager;
    private LifeManager _lfManager;

    
    void Start() {
        txtHealt = GetComponent <Text>();
       
        _lvlManager = FindObjectOfType <LevelManager>();
        _lfManager= FindObjectOfType <LifeManager>();
        playerHealth = PlayerPrefs.GetInt("CurrentHealth"); 
    }


    // TODO: Replace to observer
    void Update() {
        if (playerHealth <= 0) {
            playerHealth = 0;
            _lfManager.TakeLife();
            _lvlManager.RespawnPlayer();
            FullHealth();
            

             
        }

        txtHealt.text = playerHealth.ToString();
       
    }

    
    public static void HurtPlayer(int damageToGive) {
        playerHealth -= damageToGive;
        PlayerPrefs.SetInt("CurrentHealth", playerHealth); 
    }

    public void FullHealth() {
        playerHealth = PlayerPrefs.GetInt("MaxHealth"); 
         PlayerPrefs.SetInt("CurrentHealth", playerHealth); 

       
        
    }
}
