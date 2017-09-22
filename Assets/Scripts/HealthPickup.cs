using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class HealthPickup : MonoBehaviour {

    public int healthToAdd;

    private AudioSource _aus;

    void Start() {
        _aus = GetComponent <AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D senpai) {

        if (senpai.tag == "Player") {
           HealthManager.HurtPlayer(-healthToAdd);
            _aus.Play();
            Destroy(gameObject);
        }


    }
}
