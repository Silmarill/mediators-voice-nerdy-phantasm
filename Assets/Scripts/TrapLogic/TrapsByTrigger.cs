using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsByTrigger : MonoBehaviour {
    public Transform spikes;



    ///<summary>
    /// массивы коллайдеров, которые должны выключаться при срабатывании триггера
    /// </summary>
    public BoxCollider2D[] boxColliderDisableList;
    public CircleCollider2D[] circleColliderDisableList;
    public BoxCollider2D trapTrigger;

    ///<summary>
    /// Параметр для проверки, является ли объект одноразовым(напр. падающий камень)
    /// </summary>
    public bool isOneTimeUse;

    ///<summary>
    /// Время срабатывания ловушки
    /// </summary>
    public float triggerDelay;
    public float recoveryDelay;

    // Выключение триггеров для одного использования
    void setTriggersState(bool state) {
        // для всех Collider2D убираем триггер для безопасного хождения по ним
        if (boxColliderDisableList.Length > 0) {
            for (int i = 0; i < boxColliderDisableList.Length; i++) {
                boxColliderDisableList[i].enabled = state;
            }
        }
        if (circleColliderDisableList.Length > 0) {
            for (int i = 0; i < circleColliderDisableList.Length; i++) {
                circleColliderDisableList[i].enabled = state;
            }
        }
    }
    private IEnumerator OnCollisionEnter2D(Collision2D collision) {
        if (trapTrigger.IsTouching(collision.collider)) {
            Debug.Log(collision.collider);
            yield return new WaitForSeconds(triggerDelay);
            setTriggersState(false);
            if (!isOneTimeUse) {
                yield return new WaitForSeconds(recoveryDelay);
                setTriggersState(true);
            }
        }
    }
}