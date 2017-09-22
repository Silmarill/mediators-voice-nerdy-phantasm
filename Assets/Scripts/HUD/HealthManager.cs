using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {


    public static int playerHealth;
    private int maxPlayerHealth ;

    private Slider _hpBar;
   //    private Text txtHealt;

    private LevelManager _lvlManager;
    private LifeManager _lfManager;
    private TimeManager _timManager;

    void Start() {
        //txtHealt = GetComponent <Text>();
        _hpBar = GetComponent <Slider>();

        _timManager = FindObjectOfType <TimeManager>();
        _lvlManager = FindObjectOfType <LevelManager>();
        _lfManager = FindObjectOfType <LifeManager>();
        playerHealth = PlayerPrefs.GetInt("CurrentHealth");
        maxPlayerHealth =  PlayerPrefs.GetInt("MaxHealth");
        _hpBar.maxValue = maxPlayerHealth;

    }


    // TODO: Replace to observer
    void Update() {
        if (playerHealth <= 0) {
            playerHealth = 0;
            _lfManager.TakeLife();
            _lvlManager.RespawnPlayer();
            _timManager.ResetTime();
            FullHealth();
            
             
        }

        if (playerHealth > maxPlayerHealth) {
            playerHealth = maxPlayerHealth;
        }

       // txtHealt.text = playerHealth.ToString();
        _hpBar.value = playerHealth;

    }

    
    public static void HurtPlayer(int damageToGive) {
        playerHealth -= damageToGive;
        PlayerPrefs.SetInt("CurrentHealth", playerHealth); 
    }

    public void FullHealth() {
        playerHealth = maxPlayerHealth;
         PlayerPrefs.SetInt("CurrentHealth", playerHealth);
    }

    public void KillPlayer() {
        playerHealth = 0;

    }

}
