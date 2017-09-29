using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Usage AddComponent добавить данный скрипт к требуемому объекту. 
 * Выполняет остановку определенных скриптов,отвечающих за  обновление и анимацию
 */

public class PauseManager : MonoBehaviour {
    private Animator _ator;
    private Rigidbody2D _r2d;
    private Component[] monoList;
    private bool[] scriptStatuses;
    // Use this for initialization
    void Start () {
        // Инициализация и кеширование требуемых переменных
        Messenger.AddListener<bool>("PauseStatus", pauseStatus);
        monoList = gameObject.GetComponents(typeof(MonoBehaviour));
        scriptStatuses = new bool[monoList.Length];
        _ator = GetComponent<Animator>();
        _r2d = GetComponent<Rigidbody2D>();
    }

    // После Messenger.Broadcast<bool>("PauseStatus", pauseStatus) слушатель перенаправляет в данный метод с bool переменной, отвечающей за паузу
    void pauseStatus(bool isPaused) {
        // При включении паузы требуется остановить анимацию и движение
        if (isPaused) {
            setScriptStatuses();
            if (_r2d != null) {
                _r2d.velocity = new Vector2(0, 0);
                _r2d.Sleep();
            }
            if (_ator != null) {
                _ator.enabled = false;
            }
        // Выключает каждый скрипт в  объекте
            foreach (MonoBehaviour mono in monoList) {
                mono.enabled = false;
                
            }
        }
        // Включение скриптов
        else {
            if (_ator != null) {
                _ator.enabled = true;
            }
            if (_r2d != null) {
                _r2d.WakeUp();
            }
            for (int i = 0; i < monoList.Length; i++) {
                if (scriptStatuses[i]) {
                    ((MonoBehaviour) monoList[i]).enabled = true;
                }
            }
         
        }
    }
    // Удаление слушателя
    private void OnDestroy() {
        Messenger.RemoveListener<bool>("PauseStatus", pauseStatus);
    }
    // статусы скриптов для того,чтобы выключенные скрипты оставались выключенными
    private void setScriptStatuses() {
        for (int i = 0; i < monoList.Length; i++) {
            scriptStatuses[i] = ((MonoBehaviour)monoList[i]).enabled;
        }
    }
   
}
