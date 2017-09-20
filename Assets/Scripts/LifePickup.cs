using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour {

    private LifeManager _lifeManager;

    // Use this for initialization
    void Start () {
        _lifeManager = FindObjectOfType <LifeManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "MY_HERO") {
            _lifeManager.GiveLife();
            Destroy(gameObject);
        }
    }

}
