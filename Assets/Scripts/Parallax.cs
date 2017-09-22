using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parallaxScales;

    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;

    // Use this for initialization
    void Start () {
        cam = Camera.main.GetComponent <Transform>();
        previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; ++i) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

    }

    //LateUpdate execute AFTER Update. We need it couse we have script that move camera with player in
    // other Update 
    void LateUpdate() {
        for (int i = 0; i < backgrounds.Length; ++i) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            Vector3 bgTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }

}
