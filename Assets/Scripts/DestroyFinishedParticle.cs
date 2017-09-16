using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFinishedParticle : MonoBehaviour {

    private ParticleSystem  _ps;

    
    void Start () {
        _ps = GetComponent <ParticleSystem>();
    }
    
    
    void Update () {
        if (_ps.isPlaying) return;
        // Уничтожение и создание объекта - это аллоакция памяти. Их лучше избегать.
        Destroy(this.gameObject);
    }
}
