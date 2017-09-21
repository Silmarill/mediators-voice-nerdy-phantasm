using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerEnemyMove : MonoBehaviour {

    private Transform thePlayer;

    public float moveSpeed;

    public float seekRange;

    private Transform _tr;

    public LayerMask _lmToFollow;
    public bool isInRange;

    
    private bool isFacingAway;

    public bool isFollowOnLookAway;

    // Use this for initialization
    void Start () {
        _tr = GetComponent <Transform>();
        thePlayer = FindObjectOfType <PlayerController>().GetComponent<Transform>();

    }

    void OnDrawGizmosSelected() {
        //another way to debug
        //gizmo cant find _tr, so we use full form of Transform
        Gizmos.DrawWireSphere(GetComponent <Transform>().position, seekRange);
    }
    
    // Update is called once per frame
    void Update () {

        isInRange = Physics2D.OverlapCircle(_tr.position, seekRange, _lmToFollow);

        if (!isFollowOnLookAway) {

            if (isInRange) {
                _tr.position = Vector3.MoveTowards(_tr.position, thePlayer.position, moveSpeed * Time.deltaTime);
                return;
            }


        }



        // 2 player look right and LEFT from enemy
        if ((thePlayer.position.x < _tr.position.x && thePlayer.localScale.x < 0) ||
            (thePlayer.position.x > _tr.position.x && thePlayer.localScale.x > 0)) {
            isFacingAway = true;
        } else {
            isFacingAway = false;
        }


        if (isInRange && isFacingAway) {
            _tr.position = Vector3.MoveTowards(_tr.position, thePlayer.position, moveSpeed * Time.deltaTime);

        }



    }
}
