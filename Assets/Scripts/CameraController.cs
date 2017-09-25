using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

    private PlayerController player;
    public bool isFollowin;

    private Transform _tr;
    private Transform _trPlayer;

    public float xOffset;
    public float yOffset;



    void Start () {
        player = FindObjectOfType <PlayerController>();
        isFollowin = true;
        _tr = GetComponent <Transform>();
        _trPlayer = player.GetComponent <Transform>();

       


    }
    
    //TODO: Make some sweet lerp;   
    void Update () {
        if (isFollowin &&  _trPlayer!=null) {
            

            _tr.position = new Vector3(_trPlayer.position.x + xOffset,
                                        _trPlayer.position.y + yOffset,
                                        _tr.position.z);

    
            



        }
    }
}
