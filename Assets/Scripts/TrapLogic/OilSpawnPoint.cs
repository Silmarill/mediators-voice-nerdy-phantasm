using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OilSpawnPoint : MonoBehaviour {
   

    public GameObject projectile;

    public Transform startPoint;
    public Transform endPoint;


    [Range(0.5f,5)]
    public float spawnTime;

    [Range(0.5f,5)]
    public float travelDuration;

    public Ease easeFunc;
    private bool isPaused;

    // Use this for initialization
    void Start () {
        Messenger.AddListener<bool>(EMessage.PauseStatus.ToString(), PauseStatus);
        Invoke("Spawn",spawnTime);
    }

    void OnDestroy() {
         Messenger.RemoveListener<bool>(EMessage.PauseStatus.ToString(), PauseStatus);
    }


    void PauseStatus(bool isPaused) {
        this.isPaused = isPaused;

        if (isPaused) {
            CancelInvoke();
        } else {
            Invoke("Spawn", spawnTime);

        }
    }


    void Spawn() {
        GameObject goBullet = projectile.Spawn(startPoint.position,startPoint.rotation);

        goBullet.GetComponent<Transform>().DOMove(endPoint.position, travelDuration)
                                          .SetEase(easeFunc)
                                          .OnComplete(goBullet.Recycle);
      if (!isPaused) Invoke("Spawn",spawnTime);
    }
 
}
