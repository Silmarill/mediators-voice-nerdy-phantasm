using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform platform;
    public float moveSpeed;

    //array
    public Transform[] points;


    public int pointIndex = 0; 



    void Update() {
        platform.position = Vector3.MoveTowards(platform.position,points[pointIndex].position,moveSpeed * Time.deltaTime);
        if (platform.position == points[pointIndex].position) {
            ++pointIndex;
            if (pointIndex == points.Length) {
                pointIndex = 0;
            }
        }
    }
}
