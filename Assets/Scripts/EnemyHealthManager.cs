using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;

    private AudioSource _aus;


    // Use this for initialization
    void Start () {
        _aus = GetComponent <AudioSource>();
    }


    
    void CheckLive () {
        if (enemyHealth <= 0) {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
        }
    }



    public void GiveDamage(int damageToGive) {
        enemyHealth -= damageToGive;
        _aus.Play();
        CheckLive();
    }

}
