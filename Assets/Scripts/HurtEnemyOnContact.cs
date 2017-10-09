using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyOnContact : MonoBehaviour {

    public int damageToGive;
    public float bounceOnEnemy;

    private Rigidbody2D _r2d;



    // Use this for initialization
    void Start () {
        _r2d = GetComponentInParent <Rigidbody2D>();
    }
    

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent<EnemyHealthManager>().GiveDamage(damageToGive);
            _r2d.velocity = new Vector2(_r2d.velocity.x, bounceOnEnemy);
        }
        if (other.tag == "Enemy_Thief") {
            other.GetComponent<ThiefHealthManager>().GiveDamage(damageToGive);
            _r2d.velocity = new Vector2(_r2d.velocity.x, bounceOnEnemy);
        }
    }
}
