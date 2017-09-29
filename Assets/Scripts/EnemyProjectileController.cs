using System.Collections;
using System.Collections.Generic;
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

    void OnEnable() {
        speedOnStart = speed;

        if (_tr == null) {
            _tr = GetComponent <Transform>();
        }


        if (_r2d == null) {
            _r2d = GetComponent <Rigidbody2D>();
        }

        if (player == null) {
            player = PlayerController.me.gameObject.GetComponent <Transform>();
        }

        
        if (player.position.x < _tr.position.x) {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }

        // dont need to keep it in update
        _r2d.velocity = new Vector2(speed, _r2d.velocity.y);
        _r2d.angularVelocity = rotationSpeed;
    }



    // Use this for initialization
    void Start() {
        // Добавление слушателя из-за нужды в переопределении логики
        Messenger.AddListener<bool>("PauseStatus", pauseStatus);

        _r2d = GetComponent <Rigidbody2D>();
        player = PlayerController.me.gameObject.GetComponent<Transform>();
        _tr = GetComponent <Transform>();
        
    }

    // Получение от слушателя информации о паузе, остановка движения буллетсов
    void pauseStatus(bool isPaused) {
        this.isPaused = isPaused;
        if (isPaused) {
            velocityBeforePause = _r2d.velocity;
            _r2d.velocity = Vector3.zero;
            _r2d.angularVelocity = 0.0f;
        }
        // В случае отжатия паузы всем буллетсам возвращаются их параметры для перемещения
        else
        {
            _r2d.velocity = velocityBeforePause;
            _r2d.angularVelocity = rotationSpeed;
        }
        
    }

    //При разрушении объекта убираем слушатель.
    private void OnDestroy()
    {
        Messenger.RemoveListener<bool>("PauseStatus", pauseStatus);
    }

    void OnDisable() {
        speed = speedOnStart;
    }
    

    
    void OnTriggerEnter2D(Collider2D other) {
        
            if (other.tag == "Player")
            {
                HealthManager.HurtPlayer(damageToGive);
            }
            impactEffect.Spawn(transform.position, transform.rotation);
            gameObject.Recycle();
        
    }
}
