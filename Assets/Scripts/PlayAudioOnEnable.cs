using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour {

    public AudioClip clipToPlay;

    // Use this for initialization
    void OnEnable () {
      VoiceManager.me.PlayNoiseSound(clipToPlay);	
    }
    
    
}
