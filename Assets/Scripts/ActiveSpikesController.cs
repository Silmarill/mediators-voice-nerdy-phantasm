using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Класс предназначен для вертикального передвижения объекта на параметры yShift,zShift и xShift, 
// а если ни один из заданных параметров не предоставлен - то выступают как обычные шипы из под земли
public class ActiveSpikesController : MonoBehaviour {

    
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

    // BoxCollider2D в случае неуказанных параметров передает размер по координате y 
    private BoxCollider2D _bc2D;

    // Базовые точки, от которой к которой двигается объект
    private Vector3 baseStartPoint;
    private Vector3 baseEndPoint;

    // Конечная точка объекта на момент обновления
    private Vector3 endPoint;

    // Проверка на метод Invoke().
    private bool isInvoked;

    // Use this for initialization
    void Start() {
        _bc2D = GetComponent<BoxCollider2D>();
        isInvoked = false;

        // Получение стартовой позиции объекта
        baseStartPoint = spikes.position;

        
       // Получение конечной позиции объекта
        baseEndPoint = setEndPoint();
       
       // Присваивание конечной позиции для старта объекта
        endPoint = baseEndPoint;
        
    }
   
    void Update () {
        // Постоянно обновление позиции через Vector3.MoveTowards для более плавного движения.
        spikes.position = Vector3.MoveTowards(spikes.position, endPoint, moveSpeed * Time.deltaTime);
        if (isInvoked) return;
        // Проверка достижений начальной или конечной точки в пути объекта
        if (spikes.position == baseEndPoint) {
                Invoke("toStartPoint", DelayTimeOnTop);
                isInvoked = true;    
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

    // Вызов метода для определения конечной позиции объекта с полученными в едиторе параметрами.
    // При их отсутствии создает объект со стандартной логикой "шипы из под земли"
    Vector3 setEndPoint() {
        float x = 0;
        float y = 0;
        if (xShift != 0) {
            x = xShift;
            if (yShift != 0) {
                y = yShift;
                return new Vector3(x, y, zShift);
            }
            return new Vector3(x, baseStartPoint.y, zShift);
        }
        if (yShift != 0) {
            y = yShift;
            return new Vector3(baseStartPoint.x, y, zShift);
        }
        return new Vector3(baseStartPoint.x, baseStartPoint.y + _bc2D.size.y, zShift);
    }
}
