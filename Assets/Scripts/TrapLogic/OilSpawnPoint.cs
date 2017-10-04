using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpawnPoint : MonoBehaviour {
    public float spawnTime;
    private float spawnCounter;
    public GameObject projectile;
    public Transform oilPoint;
	// Use this for initialization
	void Start () {
        spawnCounter = spawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter < 0) {
            projectile.Spawn(oilPoint.position,oilPoint.rotation);
            spawnCounter = spawnTime;
        }
        

    }
}
