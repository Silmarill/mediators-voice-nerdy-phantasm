using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;
    public GameObject deathEffect;
    public GameObject impactEffect;

    private Rigidbody2D _r2d;

    private Transform player;

    //TODO: Move this property to enemy
    public int pointsForKill;

    public float rotationSpeed;
    public int damageToGive;

    // Use this for initialization
    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        player = FindObjectOfType <PlayerController>().GetComponent<Transform>();

        if (player.localScale.x < 0) {
            rotationSpeed = -rotationSpeed;
            speed = -speed;
        }

        // dont need to keep it in update
        _r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        _r2d.angularVelocity = rotationSpeed;
    }

    

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            //Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);
            // ScoreManager.AddPoints(pointsForKill);
            other.GetComponent<EnemyHealthManager>().GiveDamage(damageToGive);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
