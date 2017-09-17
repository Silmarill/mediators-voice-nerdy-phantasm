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


    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        _ator = GetComponent <Animator>();
        _tr = GetComponent <Transform>();
    }



    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }



    void Update() {

        if (isGrounded) {
            isDoubleJumped = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDoubleJumped && !isGrounded) {
            Jump();
            isDoubleJumped = true;
        }

        //каждый кадр сбрасывается в 0, чтобы игрок не скользил из-за материала
        moveVelosity = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveVelosity = moveSpeed;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveVelosity = -moveSpeed;
        }

        _r2d.velocity = new Vector2(moveVelosity, _r2d.velocity.y);

        _ator.SetFloat("Speed", Mathf.Abs(_r2d.velocity.x));
        _ator.SetBool("isGrounded", isGrounded);


        if (_r2d.velocity.x > 0) {
            _tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else if (_r2d.velocity.x < 0) {
            _tr.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

         if (Input.GetKeyDown(KeyCode.Return) ) {
             Instantiate(projectile, firePoint.position, firePoint.rotation);
         }


    }



    public void Jump() {
        _r2d.velocity = new Vector2(_r2d.velocity.x, jumpHeight);
    }
}
