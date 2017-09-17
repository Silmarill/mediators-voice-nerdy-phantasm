using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    public int pointsToAdd;

    void OnTriggerEnter2D(Collider2D senpai) {

        if (senpai.GetComponent <PlayerController>() == null) {
            return;
        }

        ScoreManager.AddPoints(pointsToAdd);
        Destroy(gameObject);
    }
}
