using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {
    public float lifeTime;



    void OnEnable() {
      Invoke("DestroyThisGameObject",lifeTime);
    }



    void OnDisable() {
        CancelInvoke();
    }



    void DestroyThisGameObject() {
        gameObject.Recycle();
    }

}
