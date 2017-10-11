using UnityEngine;
using System.Collections;

public class ThiefAttackChecker : MonoBehaviour {
    private Animator _ator;
    public EnemyThiefController _etc;
    void Start() {
     //   _etc = FindObjectOfType<EnemyThiefController>();
        _ator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "Player") return;  
        _ator.SetBool("isAttacking", true);      
    }
   /* private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag != "Player") return;
        
        _ator.SetBool("isAttacking", true);
     }
   */ 
   
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag != "Player") return;
        _etc.DeActivateTrigger();
        
    }
}
