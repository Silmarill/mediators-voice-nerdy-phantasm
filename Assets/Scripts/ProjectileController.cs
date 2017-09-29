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

    public float rotationSpeed;
    public int damageToGive;

    private bool isPaused;
    private Vector2 velocityBeforePause;


    private Transform _tr;


    public Vector3 moveDistance;
    private Vector3 moveDistanceStorage;
    public float moveDuration;
    public float rotateDuration;
    private Sequence s;

    void Awake() {
      //  _r2d = GetComponent <Rigidbody2D>();
        player = FindObjectOfType <PlayerController>().GetComponent <Transform>();   // PlayerController.me.gameObject
        _tr = GetComponent <Transform>();

        


    }
    // Use this for initialization
    void OnEnable() {
        speedOnStart = speed;
        moveDistanceStorage = moveDistance;

        /*
        if (_r2d == null) {
              _r2d = GetComponent <Rigidbody2D>();
        }

        if (player == null) {
           player = PlayerController.me.gameObject.GetComponent <Transform>();
        }*/

        if (player.localScale.x < 0) {
            rotationSpeed = -rotationSpeed;
            speed = -speed;
            moveDistance = -moveDistance;
        }


        

        // dont need to keep it in update
        //_r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        // _r2d.angularVelocity = rotationSpeed;
        
        _tr.DOBlendableMoveBy(moveDistance, moveDuration).SetEase(Ease.Linear).OnComplete(gameObject.Recycle);

        //t.DOBlendableRotateBy(new Vector3(0, 0, rotationSpeed),rotateDuration).SetLoops(-1,LoopType.Incremental);

        
        /*
        s = DOTween.Sequence();

        s.Append(_tr.DOBlendableMoveBy(moveDistance, moveDuration)
                    .OnComplete(gameObject.Recycle)
                    .SetEase(Ease.Linear));

        s.Join(_tr.DOBlendableRotateBy(new Vector3(0, 0, rotationSpeed), rotateDuration)
                  .SetLoops(-1)
                  .SetEase(Ease.Linear));
                  */
        //http://easings.net/ru



    }

    // Получение от слушателя информации о паузе, остановка движения буллетсов
    void pauseStatus(bool isPaused)
    {
        this.isPaused = isPaused;


        if (isPaused) {
            _tr.DOPause();
            //velocityBeforePause = _r2d.velocity;
            // _r2d.velocity = Vector3.zero;
            //  _r2d.angularVelocity = 0.0f;
        }
        // В случае отжатия паузы всем буллетсам возвращаются их параметры для перемещения
        else
        {
           _tr.DOPlay();
            //_r2d.velocity = velocityBeforePause;
            //_r2d.angularVelocity = rotationSpeed;
        }

    }

    //При разрушении объекта убираем слушатель.
    private void OnDestroy()
    {
        Messenger.RemoveListener<bool>("PauseStatus", pauseStatus);
    }

    void OnDisable() {
        speed = speedOnStart;
        moveDistance = moveDistanceStorage;
        _tr.DOKill();
       // s.Kill();
    }



    void Start() {
        // Добавление слушателя из-за нужды в переопределении логики
        Messenger.AddListener<bool>("PauseStatus", pauseStatus);

        //_r2d = GetComponent <Rigidbody2D>();
        // player = PlayerController.me.gameObject.GetComponent <Transform>();

        //_tr = GetComponent <Transform>();
        //_tr.DOM
    }



    void OnTriggerEnter2D(Collider2D other) {
      
        if (other.tag == "Enemy") {
            other.GetComponent <EnemyHealthManager>().GiveDamage(damageToGive);
        }

        if (other.tag == "Boss") {
            other.GetComponent<BossHealthManager>().GiveDamage(damageToGive);
        }

        impactEffect.Spawn(transform.position, transform.rotation);

        //_tr.DOKill();
        gameObject.Recycle();

    }

}
