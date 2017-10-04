using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFromAbove : MonoBehaviour {
    
    public BoxCollider2D oilPathCollider;
    private Transform playerTransform;
    private PlayerController playerController;
    public float intervalBetweenDrops;
    private float dropInterval;
    public float recoveryDelay;

    // Use this for initialization
    void Start () {
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.GetComponent<Transform>();
        dropInterval = intervalBetweenDrops;
    }
    
    // Update is called once per frame
   
    private void OnTriggerStay2D(Collider2D collision) {
        if (oilPathCollider.bounds.Contains(playerTransform.position)) {
            dropInterval -= Time.deltaTime;
            if (dropInterval < 0) {      
                playerController.gotOiled();
                dropInterval = intervalBetweenDrops;
            }
        }
    }
    private IEnumerator OnTriggerExit2D(Collider2D collision) {
        yield return new WaitForSeconds(recoveryDelay);
        playerController.gotFreeFromOil();
    }
}
