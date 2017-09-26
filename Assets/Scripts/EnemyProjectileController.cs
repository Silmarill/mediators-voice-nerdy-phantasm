using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {

    
    public int damageToGive;
    public float speed;
    private float speedOnStart;
    public float rotationSpeed;
    public GameObject impactEffect;

    private Transform player;
    private Rigidbody2D _r2d;

    private Transform _tr;

    void OnEnable() {
        speedOnStart = speed;

        if (_tr == null) {
            _tr = GetComponent <Transform>();
        }


        if (_r2d == null) {
            _r2d = GetComponent <Rigidbody2D>();
        }

        if (player == null) {
            player = PlayerController.me.gameObject.GetComponent <Transform>();
        }


        if (player.position.x < _tr.position.x) {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }

        // dont need to keep it in update
        _r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        _r2d.angularVelocity = rotationSpeed;
    }



    // Use this for initialization
    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        player = PlayerController.me.gameObject.GetComponent<Transform>();
        _tr = GetComponent <Transform>();
    }


     void OnDisable() {
        speed = speedOnStart;
    }
    

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
           HealthManager.HurtPlayer(damageToGive);
        }
        impactEffect.Spawn(transform.position, transform.rotation);
        gameObject.Recycle();
    }
}
