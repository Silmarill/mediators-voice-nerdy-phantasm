using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThiefAttack : MonoBehaviour {
    private Transform _tr; 
    private bool playerWasHit;
    private Animator _ator;
    
    private void Start() {
        _tr = GetComponent<Transform>();
        _ator = GetComponentInParent<Animator>();
        PlayerWasHit = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerWasHit = true;  
           
            PlayerController player = collision.GetComponent<PlayerController>();
            /*
             
            player.knockbackCount = player.knockbackLength;

            if (collision.GetComponent<Transform>().position.x < _tr.position.x) {
                player.knockFromRight = true;
            }
            else {
                player.knockFromRight = false;
            }
            */
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if(!PlayerWasHit) PlayerWasHit = true;
         }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerWasHit = false;
        }
    }

    public bool PlayerWasHit {
        get {      
            return playerWasHit;
        }

        set {
            playerWasHit = value;
        }
    }
}
