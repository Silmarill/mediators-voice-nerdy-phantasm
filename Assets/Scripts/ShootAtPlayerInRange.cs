using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayerInRange : MonoBehaviour {

    public float playerRange;
    public GameObject enemyStar;
    private Transform player;
    public Transform launchPoint;

    public float waitBetweenShoots;
    private float shootsCounter;

    private Transform _tr;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType <PlayerController>().GetComponent<Transform>();
        _tr = GetComponent <Transform>();
        shootsCounter = waitBetweenShoots;

    }
    
    // Update is called once per frame
    void Update () {


        Debug.DrawLine(new Vector3(_tr.position.x - playerRange, _tr.position.y, _tr.position.z),
                       new Vector3(_tr.position.x + playerRange, _tr.position.y, _tr.position.z));

        shootsCounter -= Time.deltaTime;
        if (shootsCounter < 0) {
          
            //if enemy  move RIGHT, faced to player and in range
            if (_tr.localScale.x < 0 && player.position.x > _tr.position.x &&
                player.position.x < _tr.position.x + playerRange) {
                enemyStar.Spawn(launchPoint.position, launchPoint.rotation);
                //== gameObject.SetActive(true);

                //Instantiate(enemyStar,);
            }


            if (_tr.localScale.x > 0 && player.position.x < _tr.position.x &&
                player.position.x > _tr.position.x - playerRange) {
                //Instantiate(enemyStar, launchPoint.position, launchPoint.rotation);
                enemyStar.Spawn( launchPoint.position, launchPoint.rotation);
            }

            shootsCounter = waitBetweenShoots;
        }


    }
}
