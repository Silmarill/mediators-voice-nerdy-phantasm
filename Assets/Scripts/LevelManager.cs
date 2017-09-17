using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [HideInInspector]
    public GameObject currentCheckpoint;

    private PlayerController player;

    public GameObject deathParticle;
    public GameObject respawnParticle;

    public float respawnDelay;


    //Пенальти потом нужно перенести в  Hazard-объекты
    public int pointPenaltyOnDeath;



    void Start() {
        // TODO: Если будет мультиплеер - тут будет затык
        player = FindObjectOfType <PlayerController>();
    }



    public void RespawnPlayer() {
         StartCoroutine(RespawnPlayerCoorutine());
    }



    public IEnumerator RespawnPlayerCoorutine() {
        Instantiate(deathParticle, player.transform.position, Quaternion.identity);

        //TODO: Подумать, можно ли просто вырубить его через SetActive
        player.enabled = false;
        player.GetComponent <SpriteRenderer>().enabled = false;
        player.GetComponent <Rigidbody2D>().velocity = Vector2.zero;

        //TODO: Перенести в Hazards
        ScoreManager.AddPoints(-pointPenaltyOnDeath);

        yield return new WaitForSeconds(respawnDelay);

        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent <SpriteRenderer>().enabled = true;

        //TODO: Нужно переделать инстанцирование на смену позиции объекта+запуск партикл-эффекта (Play)
        Instantiate(respawnParticle, currentCheckpoint.transform.position, Quaternion.identity);
    }
}
