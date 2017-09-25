using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    private PlayerController _p;
    private LevelLoader levelExit;
    private PauseMenu pausMenu;
    // Use this for initialization
    void Start () {
         #if UNITY_STANDALONE || UNITY_WEBPLAYER 
        gameObject.SetActive(false);
        return;
        #endif
        _p = FindObjectOfType <PlayerController>();
        levelExit = FindObjectOfType <LevelLoader>();
        pausMenu  = FindObjectOfType <PauseMenu>();
    }

    public void UpArrow() {
          Messenger.Broadcast("CheckChildren");
     _p.Climb(1);
    }

    public void DownArrow() {
     _p.Climb(-1);
    }
    public void ResetClimb() {
     _p.Climb(0);
    }

    // Update is called once per frame
    public void LeftArrow () {
        _p.Move(-1);
    }

    public void RightArrow () {
        _p.Move(1);
    }

    public void UnpressedArrow () {
        _p.Move(0);
    }


    public void Sword () {
        _p.Sword();
    }

    public void ResetSword () {
        _p.SwordReset();
    }


    public void Fire () {
        _p.Fire();
    }

    public void Jump () {
        _p.Jump();

        if (levelExit.isPlayerInZone) {
            levelExit.LoadLevel();
        }
    }

    public void Pause() {
        pausMenu.TogglePause();

    }

}
