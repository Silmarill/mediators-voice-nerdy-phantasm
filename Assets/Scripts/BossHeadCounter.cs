using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadCounter : MonoBehaviour {

    private Transform _tr;


    
    void Start () {
        _tr = GetComponent <Transform>();
        Messenger.AddListener("CheckChildren",CheckChildren);
    }



    void Destroy() {
        Messenger.RemoveListener("CheckChildren", CheckChildren);
    }



    void CheckChildren() {
        int countOfActiveChilds = 0;

        for (int i = 0; i < _tr.childCount; ++i) {
            if (_tr.GetChild(i).gameObject.activeSelf) {
                ++countOfActiveChilds;
            }
        }

        if (countOfActiveChilds == 0) {
            Messenger.Broadcast("BossDead");
        }
       
    }

}
