using UnityEngine;
using System.Collections;

public class ThiefAttackChecker : MonoBehaviour {
    private Animator _ator;
    void Start() {
        _ator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "Player") return;  
        _ator.SetBool("isAttacking", true);      
    }
    private void OnTriggerStay2D(Collider2D collision) {
            if (collision.tag != "Player") return;
            _ator.SetBool("isAttacking", true);
        
    }
    
}
