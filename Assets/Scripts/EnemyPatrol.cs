using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float moveSpeed;
    public bool isMoveRight;


    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool isWallHitted;

    private bool atEdge;
    public Transform edgeCheck;

    private Rigidbody2D _r2d;
    private Transform _tr;


    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
        _tr = GetComponent <Transform>();
    }


    void Update () {
        isWallHitted = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        //if no edge on floor OR wall reached
        if (isWallHitted || !atEdge) {
            isMoveRight = !isMoveRight;
        }

        if (isMoveRight) {
            _tr.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(moveSpeed, _r2d.velocity.y);
        } else {
             _tr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _r2d.velocity = new Vector2(-moveSpeed, _r2d.velocity.y);
        }



    }
}
