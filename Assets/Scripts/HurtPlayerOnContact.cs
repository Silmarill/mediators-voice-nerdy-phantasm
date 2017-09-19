using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;
    
    void OnTriggerEnter2D(Collider2D senpai) {
        if (senpai.name == "MY_HERO") {
           HealthManager.HurtPlayer(damageToGive);
        }
    }
}
