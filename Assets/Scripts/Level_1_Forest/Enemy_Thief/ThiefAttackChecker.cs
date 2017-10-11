using UnityEngine;
using System.Collections;

public class ThiefAttackChecker : MonoBehaviour {
    private Animator _ator;
    private EnemyThiefController _etc;
    private bool playerWasHit;

    

    void Start() {
        _etc = (EnemyThiefController)GetComponentInParent(typeof(EnemyThiefController));
        _ator = GetComponentInParent<Animator>();
        PlayerWasHit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "Player") return;
        PlayerWasHit = true;
        _etc.ActivateTrigger();     
    }
   
    
   
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag != "Player") return;
        PlayerWasHit = false;
        _etc.DeActivateTrigger();
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



