using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform platform;
    public float moveSpeed;
    public Transform[] points;

    private int pointIndex = 0;

    

    ///<summary>
    /// Fixed нужен для того,чтобы камера успевала за платформой
    /// </summary>
    void FixedUpdate() {
        platform.position = Vector3.MoveTowards(platform.position,points[pointIndex].position,moveSpeed * Time.deltaTime);
        if (platform.position == points[pointIndex].position) {
            ++pointIndex;
            if (pointIndex == points.Length) {
                pointIndex = 0;
            }
        }
    }
}
