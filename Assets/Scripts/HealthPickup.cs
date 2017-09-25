using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthPickup : MonoBehaviour {

    public int healthToAdd;

    public AudioClip acHealth;

    void OnTriggerEnter2D(Collider2D senpai) {

        if (senpai.tag == "Player") {
           HealthManager.HurtPlayer(-healthToAdd);
           VoiceManager.me.PlayNoiseSound(acHealth);
           Destroy(gameObject);
        }


    }
}
