using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManTrap : MonoBehaviour {
    public BoxCollider2D trapTrigger;
    public int damageToGive;
    public AudioClip acHurt;

    public bool isOneTimeUse;

    private Transform _tr;

    void Start() {

        _tr = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D senpai) {       
        if (senpai.name == "MY_HERO") {
            HealthManager.HurtPlayer(damageToGive);
            VoiceManager.me.PlayNoiseSound(acHurt);

            PlayerController player = senpai.GetComponent<PlayerController>();
            player.knockbackCount = player.knockbackLength;

            if (senpai.GetComponent<Transform>().position.x < _tr.position.x) {
                player.knockFromRight = true;
            }
            else {
                player.knockFromRight = false;
            }

        }
        if (isOneTimeUse) {
            trapTrigger.enabled = false;
        }
    }
}
