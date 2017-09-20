using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {

    
    public int damageToGive;
    public float speed;
    public float rotationSpeed;
    public GameObject impactEffect;

    private Transform player;
    private Rigidbody2D _r2d;
    
    
    // Use this for initialization
    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        player = FindObjectOfType <PlayerController>().GetComponent<Transform>();

        if (player.position.x < GetComponent<Transform>().position.x) {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }

        // dont need to keep it in update
        _r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        _r2d.angularVelocity = rotationSpeed;
    }

    

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
           HealthManager.HurtPlayer(damageToGive);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
