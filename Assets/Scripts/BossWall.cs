using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour {
    

    void Start() {
        Messenger.AddListener("BossDead", DestroyWall);
    }

    void Destroy() {
        Messenger.RemoveListener("BossDead", DestroyWall);
    }

    private void DestroyWall() {
        //Debug.Log("BossDead here");
        Destroy(gameObject);

    }

}
