using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {

    
    public int damageToGive;
    public float speed;
    private float speedOnStart;
    public float rotationSpeed;
    public GameObject impactEffect;

    private Transform player;
    private Rigidbody2D _r2d;

    private Transform _tr;

    private bool isPaused;
    private Vector2 velocityBeforePause;


    public float travelDuration;
    public Vector3 moveDistance;
    private Vector3 moveDistanceStorage;

    public float rotationAmount;
    private  float rotationAmountStorage;

    private Sequence s;

    void Awake() {
        player = FindObjectOfType <PlayerController>().GetComponent <Transform>();   
        _tr = GetComponent <Transform>();

        speedOnStart = speed;
        moveDistanceStorage = moveDistance;
        rotationAmountStorage = rotationAmount;
    }



    void OnEnable() {
        speedOnStart = speed;
        
        if (player.position.x < _tr.position.x) {
            speed = -speed;
            rotationAmount = -rotationAmount;
            moveDistance = -moveDistance;
        }

      s = DOTween.Sequence();
        
        //add new animation to sequence 
        s.Append(_tr.DOBlendableMoveBy(moveDistance, travelDuration)
                    .OnComplete(gameObject.Recycle)
                    .SetEase(Ease.Linear));
        //join() will play WITH curret animation. Not After.
        s.Join(_tr.DORotate(new Vector3(0, 0, rotationAmount), travelDuration, RotateMode.FastBeyond360)
                  .SetEase(Ease.Linear));
    }



    void Start() {
        Messenger.AddListener<bool>("PauseStatus", PauseStatus);
    }



    // Получение от слушателя информации о паузе, остановка движения буллетсов
    void PauseStatus(bool isPaused) {
        this.isPaused = isPaused;

        if (isPaused) {
            _tr.DOPause();
        } else {
            _tr.DOPlay();
        }

    }



    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            HealthManager.HurtPlayer(damageToGive);
        }
        if (other.tag == "Untagged") {
            return;
        }
        
        impactEffect.Spawn(_tr.position, _tr.rotation);
        gameObject.Recycle();
    }



    void OnDisable() {
        speed = speedOnStart;
        moveDistance = moveDistanceStorage;
        rotationAmount = rotationAmountStorage;

        s.Kill();
    }



    private void OnDestroy() {
        Messenger.RemoveListener <bool>("PauseStatus", PauseStatus);
    }



}
