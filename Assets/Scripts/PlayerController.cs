using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Принудительно назначает компонент Rigidbody2D
//на объект, когда мы вешаем этот скрипт. Чтобы потом не проверять на нулл.
[RequireComponent(typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody2D _r2d;
    private Transform _tr;



    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
    }

    // Событие происходит каждый кадр
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _r2d.velocity = new Vector2(_r2d.velocity.x, jumpHeight);
        }

         if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _r2d.velocity = new Vector2(moveSpeed, _r2d.velocity.y);
        }

           if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _r2d.velocity = new Vector2(-moveSpeed, _r2d.velocity.y);
        }


    }
}
