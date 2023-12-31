using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
   
    [SerializeField]
    private AudioClip voiceOverClip;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            AudioManager.Instance.PlayClip(voiceOverClip);
        }
    }
}
