using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
// TODO Сделать и потестировать другой вариант джойстика - круг, разделенный на 4 части
public class TouchControls : MonoBehaviour {

    private PlayerController _p;
    private LevelLoader levelExit;
    private PauseMenu pausMenu;

    ///<summary>
    /// Значения ось X и ось Y для дальнейшего использования
    ///</summary
    private float xAxis;
    private float yAxis;
    ///<summary>
    /// Проверка на состояние покоя джойстика 
    ///</summary
    private bool isMoving;
    ///<summary>
    /// Переменная для определения минимального отклонения джойстика по оси X
    /// и Y для реагирования методов для передвижения персонажа 
    ///</summary>
    public float axisShift;
    ///<summary>
    /// Переменная для определения минимального отклонения джойстика по оси X
    /// для выскакивания с лестницы, при условии нахождения в/на лестнице
    ///</summary>
    public float xLadderAxis;
    ///<summary>
    /// Переменная для определения минимального отклонения джойстика по оси Y вверх 
    /// для прохождения на следующий уровень, при условии нахождения у прохода
    ///</summary>
    public float levelExit_yAxis;
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

    private void Update() {
        ///<summary>
        /// Каждый кадр выясняет оси координат джойстика для дальнейшей проверки
        ///</summary>
        xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        yAxis = CrossPlatformInputManager.GetAxis("Vertical");
        AxisCheck();
    }


    ///<summary>
    /// Движение и проверка на лестницы. На лестнице нужно увести джойстик по горизонтали
    /// в более дальнее положение
    ///</summary>
    private void AxisCheck() {
        if (xAxis == 0) {
            _p.Move(0);
        }
        if (yAxis == 0) {
            _p.Climb(0);
        }
        if (xAxis == 0 && yAxis == 0) {
            isMoving = false;
        }
        else {
            isMoving = true;
        }

        if (!isMoving) return;

        if (yAxis > levelExit_yAxis) {
            if (levelExit.isPlayerInZone) {
                levelExit.LoadLevel();
            }
        }

        if (_p.isOnLadder) {
            if (yAxis > 0) {
                _p.Climb(1);
                _p.Move(0);
                if (xAxis > xLadderAxis) {
                    _p.Move(1);
                    return;
                }
                else if (xAxis < -xLadderAxis) {
                    _p.Move(-1);
                    return;
                }
            }
            if (yAxis < -axisShift) {
                _p.Climb(-1);
                if (xAxis > xLadderAxis) {
                    _p.Move(1);
                    return;
                }
                else if (xAxis < -xLadderAxis) {
                    _p.Move(-1);
                    return;
                }
            }
            return;
        }
        if (xAxis > axisShift) {
            _p.Move(1);
        }
        if (xAxis < -axisShift) {
            _p.Move(-1);
        }
    }

    public void UpArrow() {
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
        Messenger.Broadcast("PauseStatus", true);
    }

}
