using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //Console output in Unity3D call through 
        Debug.Log("Sup friends");
        float a = 0.1f;
        int b = 2;
        Debug.LogFormat("Sup friends. Lets count {0} + {1} = {2}", a,b,a+b);
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
