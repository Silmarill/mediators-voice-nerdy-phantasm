using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Используется для движения объекта по заранее заданным точкам
public class TrapsByPoints : MonoBehaviour {
    public Transform spikes;

    // Скорость движения шипов в направлении конечной точки с учетом смещений
    public float moveSpeed;

   
    // массивы коллайдеров для обработки в случае неуказанных параметров передает размер по координате y если указан хоть один бокс коллайдер
    public BoxCollider2D[] boxList;
    public CircleCollider2D[] circleList;
    

    // Движение по точкам.
    public Transform[] points;

    // индекс,с какой точки начинать
    public int pointIndex;

    // Если последняя координата достижена,Update() return; 
    private bool pointEndFlag;

    // Параметр для проверки, является ли объект одноразовым(напр. падающий камень)
    public bool isOneTimeUse;

    // Use this for initialization
    void Start () {   
        pointEndFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (pointEndFlag) return;
        // На случай,если объект нужно двигать по точкам
            spikes.position = Vector3.MoveTowards(spikes.position, points[pointIndex].position, moveSpeed * Time.deltaTime);
            if (spikes.position == points[pointIndex].position) {
                ++pointIndex;         
                if (pointIndex == points.Length) {
                    pointIndex = 0;
                    if (isOneTimeUse) {
                        DisableTriggers();
                        pointEndFlag = true;
                    }
                }
            }
        }
    
    // Выключение триггеров для одного использования
    void DisableTriggers() {
        // для всех Collider2D убираем триггер для безопасного хождения по ним
        if (boxList.Length > 0) {
            for (int i = 0; i < boxList.Length; i++) {
                boxList[i].isTrigger = false;
            }
        }
        if (circleList.Length > 0) {
            for (int i = 0; i < circleList.Length; i++) {
                circleList[i].isTrigger = false;
            }
        }
    }
}
