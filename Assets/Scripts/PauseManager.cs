using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private Animator _ator;
    private Rigidbody2D _r2d;
    private Component[] monoList;

    // Use this for initialization
    void Start () {
        Messenger.AddListener<bool>("PauseStatus", pauseStatus);
        monoList = gameObject.GetComponents(typeof(MonoBehaviour));
        _ator = GetComponent<Animator>();
        _r2d = GetComponent<Rigidbody2D>();
        
        

    }
    void pauseStatus(bool isPaused) {
        if (isPaused)
        {
            if (_r2d != null) {
                _r2d.velocity = new Vector2(0, 0);
                _r2d.Sleep();
            }
            if (_ator != null) {
                _ator.enabled = false;
            }
            
            foreach (MonoBehaviour mono in monoList)
            {
                mono.enabled = false;
                
            }
        }
        else {
            if (_ator != null)
            {
                _ator.enabled = true;
            }
            if (_r2d != null)
            {
                _r2d.WakeUp();
            }
            foreach (MonoBehaviour mono in monoList)
            {
                mono.enabled = true;
            }
         
        }
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener<bool>("PauseStatus", pauseStatus);
    }

}
