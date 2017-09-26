using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;
    private float speedOnStart;
    public GameObject deathEffect;
    public GameObject impactEffect;

    public Rigidbody2D _r2d;
    public Transform player;

    //TODO: Move this property to enemy
    //public int pointsForKill;

    public float rotationSpeed;
    public int damageToGive;



    // Use this for initialization
    void OnEnable() {
        speedOnStart = speed;

        if (_r2d == null) {
            _r2d = GetComponent <Rigidbody2D>();
        }

        if (player == null) {
            player = PlayerController.me.gameObject.GetComponent <Transform>();
        }

        if (player.localScale.x < 0) {
            rotationSpeed = -rotationSpeed;
            speed = -speed;
        }

        // dont need to keep it in update
        _r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        _r2d.angularVelocity = rotationSpeed;
    }



    void OnDisable() {
        speed = speedOnStart;
    }



    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        player = PlayerController.me.gameObject.GetComponent <Transform>();
    }



    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent <EnemyHealthManager>().GiveDamage(damageToGive);
        }

        if (other.tag == "Boss") {
            other.GetComponent<BossHealthManager>().GiveDamage(damageToGive);
        }

        impactEffect.Spawn(transform.position, transform.rotation);
        gameObject.Recycle();
    }

}
