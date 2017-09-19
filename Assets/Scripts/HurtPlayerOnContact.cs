using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;
    public AudioClip acHurt;

    private AudioSource _aus;

    void Start() {
        _aus = GetComponent <AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D senpai) {
        if (senpai.name == "MY_HERO") {
           HealthManager.HurtPlayer(damageToGive);

           //This static method instantiate copy of AudioSource to play sound once and destroy then          
           _aus.PlayOneShot(acHurt);
        }
    }
}
