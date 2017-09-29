using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Класс предназначен для передвижения объекта на параметры yShift,zShift и xShift, 
// а если ни один из заданных параметров не предоставлен - то выступают как обычные шипы из под земли
public class TrapsByOnePoint : MonoBehaviour {


    public Transform spikes;

    // Вводимые данные, такие как :
    // Горизонтальное смещение объекта
    public float xShift;

    // Вертикальное смещение объекта
    public float yShift;
    // По оси z 
    public float zShift;
    // Скорость движения шипов в направлении конечной точки с учетом смещений
    public float moveSpeed;

    // Задержки при достижении конечной точки.
    // В верхней точке
    public float DelayTimeOnTop;

    // В нижней точке
    public float DelayTimeOnBot;

    // массивы коллайдеров для обработки в случае неуказанных параметров передает размер по координате y если указан хоть один бокс коллайдер
    public BoxCollider2D[] boxList;
    public CircleCollider2D[] circleList;
    // Базовые точки, от которой к которой двигается объект
    private Vector3 baseStartPoint;
    private Vector3 baseEndPoint;

    // Конечная точка объекта на момент обновления
    private Vector3 endPoint;

    // Проверка на метод Invoke().
    private bool isInvoked;

    // Параметр для проверки, является ли объект одноразовым(напр. падающий камень)
    public bool isOneTimeUse;

    // Use this for initialization
    void Start() {
        isInvoked = false;

        // Получение стартовой позиции объекта
        baseStartPoint = spikes.position;

        // Получение конечной позиции объекта
        baseEndPoint = setEndPoint();

        // Присваивание конечной позиции для старта объекта
        endPoint = baseEndPoint;

    }

    void Update() {
        if (isInvoked) return;
        // Постоянно обновление позиции через Vector3.MoveTowards для более плавного движения.
        spikes.position = Vector3.MoveTowards(spikes.position, endPoint, moveSpeed * Time.deltaTime);
        // Проверка достижений начальной или конечной точки в пути объекта
        if (spikes.position == baseEndPoint) {
            if (!isOneTimeUse) {
                Invoke("toStartPoint", DelayTimeOnTop);
                isInvoked = true;
            }
            else {
                disableTriggers();
                isInvoked = true;
            }
        }
        else if (spikes.position == baseStartPoint) {
            Invoke("toEndPoint", DelayTimeOnBot);
            isInvoked = true;
        }
    }
    // Присваивание в endPoint требуемого Vector3 для обратного пути.
    void toEndPoint() {
        endPoint = baseEndPoint;
        isInvoked = false;
    }
    void toStartPoint() {
        endPoint = baseStartPoint;
        isInvoked = false;
    }
    // Выключение триггеров для одного использования
    void disableTriggers() {
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


    // Вызов метода для определения конечной позиции объекта с полученными в едиторе параметрами.
    // При их отсутствии создает объект со стандартной логикой "шипы из под земли"
    Vector3 setEndPoint() {
        if (boxList.Length > 0 && xShift == 0 && yShift == 0) {
            return new Vector3(baseStartPoint.x, baseStartPoint.y + boxList[0].size.y, zShift);
        }
        return new Vector3(baseStartPoint.x + xShift, baseStartPoint.y + yShift, zShift);
    }
}
