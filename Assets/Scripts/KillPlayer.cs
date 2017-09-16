using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    
    private LevelManager levelManager;

    void Start () {
        /* Условимся, что Менеджер Уровней у нас 1. FindObjectOfType - супермедленная операция, 
         * но если мы используем её раз в новолуние, то все в порядке.
         */
        levelManager = FindObjectOfType <LevelManager>();
    }

    /* Коллайдеры - это мешы (сетки, описывающие объект), с которыми можно взаимодействовать через 
     *  физику. Ригидбоди будет сталкиваться со всем, на чем висит коллайдер.
     */
    
    /* У коллайдера должна быть галочка isTrigger, чтобы метод ниже отработал. Превращает "коробку"
     * в "область". На аффекторе должен быть ригидбоди и НЕ-isTrigger коллайдер.
     */

    
    //Если любой senpai вошел в область..
    void OnTriggerEnter2D(Collider2D senpai) {
      
        //Для группы объектов вместо name можно использовать tag.
        if (senpai.name == "MY_HERO") {
            //Cбрасываем скорость, чтобы не летел по инерции
            senpai.GetComponent <Rigidbody2D>().velocity = Vector2.zero;
            levelManager.RespawnPlayer();
        }
    }

}
