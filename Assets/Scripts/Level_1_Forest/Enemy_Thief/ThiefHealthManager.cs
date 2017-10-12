using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Стандартный класс EnemyHealthManager с небольшим отличием - запускает анимацию 
/// вместо уничтожения которая уже уничтожает игровой объект
///</summary
public class ThiefHealthManager : MonoBehaviour {
    public int enemyHealth;
    public int pointsOnDeath;
    public AudioClip acEnemy;
    public Animator _ator;


    void CheckLive() {
        if (enemyHealth <= 0) {
            Messenger.Broadcast("AddPoints", pointsOnDeath);

            ///<summary>
            /// Запуск анимации Death
            ///</summary
            _ator.SetBool("isDead", true);
        }
    }


    public void GiveDamage(int damageToGive) {
        enemyHealth -= damageToGive;
        VoiceManager.me.PlayNoiseSound(acEnemy); ;
        CheckLive();
    }
}

