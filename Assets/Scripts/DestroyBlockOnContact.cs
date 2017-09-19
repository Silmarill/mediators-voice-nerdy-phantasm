using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockOnContact : MonoBehaviour {

     void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
           Destroy(other.gameObject);
        }
    }
}
