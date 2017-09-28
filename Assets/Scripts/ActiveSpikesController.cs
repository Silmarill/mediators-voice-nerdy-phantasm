using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpikesController : MonoBehaviour {

    public Transform spikes;
    public float rotation;
    public float scale;
    public float moveSpeed;
    public float DelayTimeOnTop;
    public float DelayTimeOnBot;

    private BoxCollider2D _bc2D;
    private Vector3 startPoint;
    private Vector3 baseEndPoint;
    private Vector3 endPoint;


    public int ifcount;
    private bool isInvoked;

    // Use this for initialization
    void Start() {
        _bc2D = GetComponent<BoxCollider2D>();
        isInvoked = false;
        startPoint = spikes.position;
        
        baseEndPoint = new Vector3(spikes.position.x, spikes.position.y + _bc2D.size.y, spikes.position.z);
        // Debug.Log("BASE END POINT_ " + baseEndPoint);
         endPoint = baseEndPoint;
        
    }

    // Update is called once per frame
    
    void Update () {
        spikes.position = Vector3.MoveTowards(spikes.position, endPoint, moveSpeed * Time.deltaTime);
        if (isInvoked) return;
        // Вызов апдейта
        if (spikes.position == baseEndPoint) {
                Invoke("toStartPoint", DelayTimeOnTop);
                isInvoked = true;    
        }
        else if (spikes.position == startPoint) {
                Invoke("toEndPoint", DelayTimeOnTop);
                isInvoked = true;
        }
  
    }
    void toEndPoint() {
        endPoint = baseEndPoint;
        isInvoked = false;
    }
    void toStartPoint() {
        endPoint = startPoint;
        isInvoked = false;
    }
}
