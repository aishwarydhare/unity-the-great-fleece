using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;

    [SerializeField]
    private AudioSource audioSource;

    public static AudioManager Instance {
        get {
            if (_audioManager == null) Debug.Log("audio manager is null");
            return _audioManager;
        }
    }

    private void Awake(){
        _audioManager = this;
    }

    public void PlayClip(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip);
    }
}
