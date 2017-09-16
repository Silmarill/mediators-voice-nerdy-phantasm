using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    // Если мы хотим оставить поле для доступа из других классов
    // но убрать из инспектора используем - [HideInInspector] 
    [HideInInspector] public GameObject currentCheckpoint;

    private PlayerController player;

    // Работа с системой чстиц - занятие творческое. Просто откройте компонент и поиграйте
    // параметрами для лучшего визуального эффекта.
    public GameObject deathParticle;
    public GameObject respawnParticle;

    public float respawnDelay;


    void Start() {
        // TODO: Если будет мультиплеер - тут будет затык
        player = FindObjectOfType <PlayerController>();
    }



    public void RespawnPlayer() {
    //Коорутина - statemashine. Она запускает отдельный поток. Как только доходит до yield
    // скрипты продолжают работу с предыдущей точки вызова коорутины. Коорутина продолжит работу
    // параллельно.

        //Допустимо передавать строкой. Но это может привести к проблемам при рефакторинге.
        StartCoroutine(RespawnPlayerCoorutine());
    }



    public IEnumerator RespawnPlayerCoorutine() {
        Instantiate(deathParticle, player.transform.position, Quaternion.identity);

        //TODO: Подумать, можно ли просто вырубить его через SetActive
        //вырубаем контроль над персонажем во время задержки
        player.enabled = false;
        //выключаем визуальную часть игрока
        player.GetComponent <SpriteRenderer>().enabled = false;


        //Вся корутина затевалась ради ОждиданияНесколькихСекунд
        yield return new WaitForSeconds(respawnDelay);

        // Присваиваем Игроку значение позиции Чекпоинта
        player.transform.position = currentCheckpoint.transform.position;

        //возвращаем всё в зад
        player.enabled = true;
        player.GetComponent <SpriteRenderer>().enabled = true;

        //TODO: Нужно переделать инстанцирование на смену позиции объекта+запуск партикл-эффекта (Play)
        Instantiate(respawnParticle, currentCheckpoint.transform.position, Quaternion.identity);
    }
}
