using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;
    private float speedOnStart;
    public GameObject deathEffect;
    public GameObject impactEffect;

    public Rigidbody2D _r2d;
    public Transform player;

    //TODO: Move this property to enemy
    //public int pointsForKill;

    
    public int damageToGive;

    private bool isPaused;
    private Vector2 velocityBeforePause;


    private Transform _tr;


    public float travelDuration;
    public Vector3 moveDistance;
    private Vector3 moveDistanceStorage;

    public float rotationAmount;
    private  float rotationAmountStorage;
    
    private Sequence s;

    void Awake() {
        player = FindObjectOfType <PlayerController>().GetComponent <Transform>();   // PlayerController.me.gameObject
        _tr = GetComponent <Transform>();

        speedOnStart = speed;
        moveDistanceStorage = moveDistance;
        rotationAmountStorage = rotationAmount;
    }



    // Use this for initialization
    void OnEnable() {
        if (player.localScale.x < 0) {
            rotationAmount = -rotationAmount;
            speed = -speed;
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



    // Получение от слушателя информации о паузе, остановка движения пуль
    void PauseStatus(bool isPaused){
        this.isPaused = isPaused;

        if (isPaused) {
            _tr.DOPause();
        } else {
            _tr.DOPlay();
        }
    }



    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Oil") return;

        if (other.tag == "Enemy") {
            other.GetComponent <EnemyHealthManager>().GiveDamage(damageToGive);
        }

        if (other.tag == "Boss") {
            other.GetComponent <BossHealthManager>().GiveDamage(damageToGive);
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

    
    //При разрушении объекта убираем слушатель.
    private void OnDestroy(){
        Messenger.RemoveListener<bool>("PauseStatus", PauseStatus);
    }
    


    

}
