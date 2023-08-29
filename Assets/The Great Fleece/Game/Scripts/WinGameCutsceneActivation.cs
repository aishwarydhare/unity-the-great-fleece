using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameCutsceneActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject winCutscene;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && GameManager.Instance.HasCard){
            winCutscene.SetActive(true);
        }
    }
}
