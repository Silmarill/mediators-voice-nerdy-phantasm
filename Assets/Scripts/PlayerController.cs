using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Принудительно назначает компонент Rigidbody2D
//на объект, когда мы вешаем этот скрипт. Чтобы потом не проверять на нулл.
[RequireComponent(typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour {


    private float moveVelosity;
    public float moveSpeed;
    public float jumpHeight;


    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private bool isDoubleJumped;


    private Animator _ator;
    private Rigidbody2D _r2d;
    private Transform _tr;

    public Transform firePoint;
    public GameObject projectile;

    public float shotDelay;
    private float shotDelayCounter;

    // AudioClip with a jumping Sound
    public AudioClip acJump;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool  knockFromRight;

    //Ladder var's
    public bool  isOnLadder;
    private float climbVelosity;
    public float climbSpeed;
    private float gravityStore;

    public static PlayerController me { get; private set; }



    void Start() {
        me = this;
        _r2d = GetComponent <Rigidbody2D>();
        _ator = GetComponent <Animator>();
        _tr = GetComponent <Transform>();
        gravityStore = _r2d.gravityScale;
    }



    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }



    void Update() {

        if (isGrounded) {
            isDoubleJumped = false;
        }

       
        _ator.SetBool("isGrounded", isGrounded);
         #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }

        if (Input.GetButtonDown("Jump")  && !isDoubleJumped && !isGrounded) {
            Jump();
            isDoubleJumped = true;
        }  

         Move(Input.GetAxisRaw("Horizontal"));
        #endif

        //каждый кадр сбрасывается в 0, чтобы игрок не скользил из-за материала
        //moveVelosity = 0f;

        // Edit - Project Setting - Input (for more info)
        // moveVelosity = moveSpeed * Input.GetAxisRaw("Horizontal");
       

        if (knockbackCount <= 0) {
            _r2d.velocity = new Vector2(moveVelosity, _r2d.velocity.y);
        } else {
            if (knockFromRight) {
                _r2d.velocity = new Vector2(-knockback, knockback);
            }

            if (!knockFromRight) {
                _r2d.velocity = new Vector2(knockback, knockback);
            }

            knockbackCount -= Time.deltaTime;
        }


         _ator.SetFloat("Speed", Mathf.Abs(_r2d.velocity.x));


        if (_r2d.velocity.x > 0) {
            _tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else if (_r2d.velocity.x < 0) {
            _tr.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        }

      
        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetButton("Fire1")) {
            shotDelayCounter -= Time.deltaTime;
            if (shotDelayCounter <= 0) {
                shotDelayCounter = shotDelay;
                Fire();
            }
        }

        if (_ator.GetBool("isSword")) {
             //_ator.SetBool("isSword", false);
              SwordReset();
        }

        if (Input.GetButtonDown("Fire2")) {
            Sword();
            //_ator.SetBool("isSword", true);
        }

        #endif

        //TODO: Jump vector is NOT affected
        if (isOnLadder) {
            _r2d.gravityScale = 0.0f;
             #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            Climb(Input.GetAxisRaw("Vertical"));
            #endif
            //climbVelosity = climbSpeed * ;
            _r2d.velocity = new Vector2(_r2d.velocity.x, climbVelosity);

        }

        if (!isOnLadder) {
            _r2d.gravityScale = gravityStore;
        }

    }

    public void Climb(float moveInput) {
        climbVelosity = climbSpeed * moveInput;
    }

    public void Move(float moveInput) {
        moveVelosity = moveSpeed * moveInput;
    }

    public void Fire() {
        projectile.Spawn(firePoint.position, firePoint.rotation);
    }

    public void Sword() {
        _ator.SetBool("isSword", true);
    }

     public void SwordReset() {
        _ator.SetBool("isSword", false);
    }

    public void Jump() {
        // Вызов метода PlayNoiseSound для проигрывания звука прыжка
        VoiceManager.me.PlayNoiseSound(acJump);

        if (isGrounded) {
            _r2d.velocity = new Vector2(_r2d.velocity.x, jumpHeight);
        }

        if (!isDoubleJumped && !isGrounded) {
            _r2d.velocity = new Vector2(_r2d.velocity.x, jumpHeight);
            isDoubleJumped = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform") {
            _tr.parent = other.transform.GetComponent<Transform>();
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.transform.tag == "MovingPlatform") {
            _tr.parent = null;
        }
    }
}
