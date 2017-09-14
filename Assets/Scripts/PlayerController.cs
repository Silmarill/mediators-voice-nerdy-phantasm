using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//Принудительно назначает компонент Rigidbody2D
//на объект, когда мы вешаем этот скрипт. Чтобы потом не проверять на нулл.
[RequireComponent(typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    //паблик-переменные открыты для редактирования в эдиторе
    public float moveSpeed;
    public float jumpHeight;

    //небольшой пустой коппонент под ногами ГГ
    public Transform groundCheck;
    public float groundCheckRadius;
    
    //Слой настраивается на сцене для каждого объекта
    //Маска позволяет выбрать несколько слоев
    public LayerMask whatIsGround;

    private bool isGrounded;
    private bool doubleJumped;


    //кешируем компоненты, чтобы не делать  GetComponent каждый кадр
    private Rigidbody2D _r2d;
    

    void Start() {
        _r2d = GetComponent <Rigidbody2D>();
    }

    //для физики
    void FixedUpdate() {
        //физикой проверяем, есть ли в радиусе объекта объекты, соответствующие выбранным слоям
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);
    }

    // Событие происходит каждый кадр
    void Update() {
        
        // Если на земле - сбросить флаг возможности даблждампа. 
        if (isGrounded) doubleJumped = false;

        //Прыгнуть можно только если ты на земле
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded ) {
            Jump();
        }

        //Второй раз можно пргынуть только находясь в воздухе
        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !isGrounded ) {
            Jump();
            //Поднять флаг даблджампа. Т.у. УЖЕ совершен второй прыжок.
            doubleJumped = true;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            //присваиваем вектор скорости
            _r2d.velocity = new Vector2(moveSpeed, _r2d.velocity.y);
        }

           if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _r2d.velocity = new Vector2(-moveSpeed, _r2d.velocity.y);
        }


    }

    //Le оптимизация
    public void Jump() {
      _r2d.velocity = new Vector2(_r2d.velocity.x, jumpHeight);  
    }
}
