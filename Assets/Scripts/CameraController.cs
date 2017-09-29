using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

///<summary>
/// Class for smooth camepa follow
/// </summary>
public class CameraController : MonoBehaviour {

   


    [HideInInspector]
    public bool isFollowin;

    ///<summary>
    /// Флаг включения сглаживания
    /// </summary>
    public bool isSmooth;


    ///<summary>
    /// Ссылка на Transform камеры
    ///</summary>
    private Transform _tr;

     ///<summary>
    /// Ссылка на Transform ИГРОКА 
    ///</summary>
    private Transform _trPlayer;


    ///<summary>
    /// Текущая скорость. Сюда передается значение по ссылке методом SmoothDamp. 
    ///</summary>
    private Vector2 velocity;

    ///<summary>
    /// Смещение камеры по X и Y
    ///</summary>
    [Tooltip("Смещение камеры по X и Y")]
    public Vector2 Offset;
    

    ///<summary>
    /// Время сглаживания по X и Y
    ///</summary>
    [Tooltip("Время сглаживания по X и Y")]
    public  Vector2 smoothTime;



    void Start () {
        _tr = GetComponent <Transform>();
        _trPlayer = FindObjectOfType <PlayerController>().GetComponent <Transform>();
        isFollowin = true;
    }
    

    ///<summary>
    /// Благодаря FixedUpdate камера сглаживается более плавно - без рывков при анимации.
    /// Сглаживание происходит методом SmoothDamp, который, в отличии ои Linear
    /// исполльзует сигмоиду для склаживания:  https://en.wikipedia.org/wiki/Smoothstep
    ///</summary>
    void FixedUpdate() {
        
        if (isFollowin && _trPlayer != null) {
            if (isSmooth) {
                
                float posX = Mathf.SmoothDamp(_tr.position.x, _trPlayer.position.x + Offset.x, ref velocity.x,
                    smoothTime.x);
                float posY = Mathf.SmoothDamp(_tr.position.y, _trPlayer.position.y + Offset.y, ref velocity.y,
                    smoothTime.y);

                _tr.position = new Vector3(posX, posY, _tr.position.z);
            } else {
                _tr.position = new Vector3(_trPlayer.position.x + Offset.x,
                                           _trPlayer.position.y + Offset.y,
                                           _tr.position.z);
            }

        }
    }
}
