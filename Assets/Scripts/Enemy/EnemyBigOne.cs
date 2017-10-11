using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBigOne : MonoBehaviour {
 public int damageToGive;
    public AudioClip acHurt;

        public BoxCollider2D HeadCollider;
    public BoxCollider2D BodyCollider;

  
    
    private Transform _tr;
    private Rigidbody2D _r2d;

    public Vector2 VelocityVector = new Vector2(-1,0);

    void Start() {
        _tr = GetComponent <Transform>();
        _r2d = GetComponent <Rigidbody2D>();
         _r2d.velocity = VelocityVector;
    }

    void Knock(GameObject playerGO) {
        PlayerController player = playerGO.GetComponent <PlayerController>();
        player.knockbackCount = player.knockbackLength;

        if (playerGO.GetComponent <Transform>().position.x < _tr.position.x) {
            player.knockFromRight = true;
        } else {
            player.knockFromRight = false;
        }
    }


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (isEmenyFacingToPlayer(col.transform, _tr)) {

                _tr.DOMoveX(col.transform.position.x, 3.0f, false).SetEase(Ease.Linear);
                
            } 
        }
    }

    

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            _tr.DOKill();

        }
    }

   
    bool isEmenyFacingToPlayer(Transform player, Transform enemy) {
        return (player.position.x < enemy.position.x &&  enemy.localScale.x > 0) ||
               (player.position.x > enemy.position.x &&  enemy.localScale.x < 0) ;
    }


    void OnCollisionExit2D(Collision2D col) {
        if (col.otherCollider == HeadCollider) {
                   StopAllCoroutines();
        }
    }


    IEnumerator LookToSides() {
        yield return new WaitForSeconds(1.0f);
        while (true) {
            _tr.localScale = new Vector3(-_tr.localScale.x, _tr.localScale.y, _tr.localScale.z);
            yield return new WaitForSeconds(0.75f);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
             Debug.Log("Player");

            if (col.otherCollider == BodyCollider) {
                VoiceManager.me.PlayNoiseSound(acHurt);
                HealthManager.HurtPlayer(damageToGive);
                Knock(col.gameObject);
            }

            if (col.otherCollider == HeadCollider) {
                StartCoroutine(LookToSides());
            }
        }
    }
}


