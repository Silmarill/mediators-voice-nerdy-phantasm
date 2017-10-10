using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThiefController : MonoBehaviour {

    private Transform thePlayer;
    public float moveSpeed;
    private bool isMoveRight;


    public float playerRange;

    private Transform _tr;
    private Animator _ator;
    private Rigidbody2D _r2d;
    private bool isInRange;
    public AudioClip playerHurt;

    public float returnTime;
    private bool isReturning;
    private float timeCheck;

    private bool isFacingAway;
    public bool isFollowOnLookAway;


    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool isWallHitted;

    private bool atEdge;
    public Transform edgeCheck;

    public float yDifference;

    public BoxCollider2D attackCollider;
    private bool isAttacking;
    public float attackTime;
    private float attackCheck;
    public int hurtPlayerValue;

    public float angryTime;
    private float angryCheck;
    private bool inAngryState;
    private BoxCollider2D boxCollider2D;
    private EnemyThiefAttack enemyThiefAttack;

    // Use this for initialization
    void Start() {
        _tr = GetComponent<Transform>();
        _ator = GetComponent<Animator>();
        _r2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        thePlayer = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        enemyThiefAttack = FindObjectOfType<EnemyThiefAttack>();
        isAttacking = false;
        inAngryState = false;
        attackCheck = attackTime;
    }

    // Update is called once per frame
    void Update() {
        if (!isFollowOnLookAway) return;
        
        if (inAngryState) {  
            
            angryCheck -= Time.deltaTime;
            
            if (angryCheck < 0) {

                DeActivateTrigger();

                DeAngryMode();
            }
            return;
        }
        if (isAttacking) {
            attackCheck -= Time.deltaTime;   
            if (attackCheck < 0) { 
                DeActivateTrigger();
            }
            return;
        }
        _ator.SetFloat("Speed", Mathf.Abs(_r2d.velocity.x));
        isWallHitted = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if ((thePlayer.position.x < _tr.position.x && thePlayer.localScale.x < 0) ||
            (thePlayer.position.x > _tr.position.x && thePlayer.localScale.x > 0)) {
            isFacingAway = true;
        }
        else {
            isFacingAway = false;
        }

        if (isReturning) {
            timeCheck -= Time.deltaTime;
            if (timeCheck < 0) {
                isReturning = false;
                CheckDirection();
                if (IfInRangeX()) MovingDirection();
                return;
            }
            MovingDirection();
            return;
        }

        if (!(IfInRangeX() && IfInRangeY())) return;

        if (isFacingAway) {
            if (isWallHitted || !atEdge) {
                ToStepBefore();
                MovingDirection();
                timeCheck = Time.deltaTime + returnTime;
                isReturning = true;
                return;
            }
            CheckDirection();
            MovingDirection();
        }
    }


    private bool IfInRangeX() {
        if (Mathf.Abs(thePlayer.position.x - _tr.position.x) < playerRange) {
            isInRange = true;
            return true;
        }
        isInRange = false;
        return false;
    }
    private bool IfInRangeY() {
        if ((Mathf.Abs(thePlayer.position.y - _tr.position.y) < yDifference)) {
            return true;
        }
        return false;
    }
    void ToStepBefore() {

        isMoveRight = !isMoveRight;

    }
    void CheckDirection() {
        if (thePlayer.position.x > _tr.position.x) {
            isMoveRight = true;
        }
        else if (thePlayer.position.x < _tr.position.x) {
            isMoveRight = false;
        }
    }
    void MovingDirection() {
        if (isMoveRight) {
            _tr.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(moveSpeed, _r2d.velocity.y);
        }
        else {
            _tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(-moveSpeed, _r2d.velocity.y);
        }
    }
    public void Destroyer() {
        Destroy(gameObject);
    }
    public void ActivateTrigger() {
        if (isAttacking) return;
        attackCheck = attackTime;
        attackCollider.enabled = true;
        isAttacking = true;
        _r2d.velocity = new Vector2(0, _r2d.velocity.y);
    }
    public void DeActivateTrigger() {
        _ator.SetBool("isAttacking", false);
        attackCollider.enabled = false;
        isAttacking = false;
    }
    public void DeAngryMode() {
        _ator.SetBool("isAngry", false);
        boxCollider2D.enabled = false;
        inAngryState = false;
        enemyThiefAttack.PlayerWasHit = false;
    }
    public void CheckAngryMode() {
        if ((!enemyThiefAttack.PlayerWasHit) && (!inAngryState)) {
            _ator.SetBool("isAngry", true);
            angryCheck = angryTime;
            inAngryState = true;
            attackCollider.enabled = false;
            isAttacking = false;
            boxCollider2D.enabled = true;
        }
    }
    public void PlaySoundIfWasHit() {
        if (enemyThiefAttack.PlayerWasHit) {
            VoiceManager.me.PlayNoiseSound(playerHurt); 
        }
    }
    public void HitPlayer() {
        if (enemyThiefAttack.PlayerWasHit) HealthManager.HurtPlayer(hurtPlayerValue);
    }  
}
