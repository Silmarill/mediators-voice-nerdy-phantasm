using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour {

    private PlayerController datPlayer;

    // Use this for initialization
    void Start () {
        datPlayer = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "MY_HERO") {
            datPlayer.isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.name == "MY_HERO") {
            datPlayer.isOnLadder = false;
        }
    }
    

}
