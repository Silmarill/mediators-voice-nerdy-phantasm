using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;

    public AudioClip acEnemy;


    void CheckLive () {
        if (enemyHealth <= 0) {
            deathEffect.Spawn(transform.position, transform.rotation);
            Messenger.Broadcast("AddPoints",pointsOnDeath);
            Messenger.RemoveListener("GameIsResumed", onResume);
            
            Destroy(gameObject);
        }
    }

    public void GiveDamage(int damageToGive) {
        enemyHealth -= damageToGive;
        VoiceManager.me.PlayNoiseSound(acEnemy); ;
        CheckLive();
    }

}
