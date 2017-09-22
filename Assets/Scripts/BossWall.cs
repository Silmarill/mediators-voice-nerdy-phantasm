using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour {

  

    // Use this for initialization
    void Update() {
        if (FindObjectOfType <BossHealthManager>()) {
            return;
        }
        Destroy(gameObject);
    }
    
}
