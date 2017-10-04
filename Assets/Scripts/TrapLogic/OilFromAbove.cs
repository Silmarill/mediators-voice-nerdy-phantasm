using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OilFromAbove : MonoBehaviour {

    public GameObject impactEffect;
    public float slowValue;

    private bool isPaused;
    private Transform _tr;


    void Awake() {
        _tr = GetComponent<Transform>(); 
    }
   


    void Start() {
        Messenger.AddListener<bool>(EMessage.PauseStatus.ToString(), PauseStatus);
    }



    // Получение от слушателя информации о паузе, остановка движения пуль
    void PauseStatus(bool isPaused) {
        this.isPaused = isPaused;

        if (isPaused) {
            _tr.DOPause();
        }
        else {
            _tr.DOPlay();
        }
    }



    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player") {
            other.gameObject.GetComponent <PlayerController>().gotOiled(slowValue);
            Debug.Log("gotOiled");
        }
        impactEffect.Spawn(_tr.position, _tr.rotation);
        gameObject.Recycle();
    }



    //При разрушении объекта убираем слушатель.
    private void OnDestroy() {
        Messenger.RemoveListener<bool>(EMessage.PauseStatus.ToString(), PauseStatus);
    }

}
