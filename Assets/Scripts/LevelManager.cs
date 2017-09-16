using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    // Если мы хотим оставить поле для доступа из других классов
    // но убрать из инспектора используем - [HideInInspector] 
    [HideInInspector]
    public GameObject currentCheckpoint;

    private PlayerController player;

    

    void Start () {
        // TODO: Если будет мультиплеер - тут будет затык
        player = FindObjectOfType <PlayerController>();
    }



    public void RespawnPlayer() {
        Debug.Log("DEATH");
        // .transform делает GetComponent<Transform> - это тоже тяжелая операция и по уму трнасформ
        // надо тоже хешировать. Но тут подразумевается разовое использование, поэтому пойдет.
        // А вот за такой код в Update() нужно уже наказывать. Жестоко наказывать.

        // Присваиваем Игроку значение позиции Чекпоинта
        player.transform.position = currentCheckpoint.transform.position;
    }
}
