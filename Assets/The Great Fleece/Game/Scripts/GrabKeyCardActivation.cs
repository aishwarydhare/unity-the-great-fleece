using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject grabKeyCardCutscene;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            grabKeyCardCutscene.SetActive(true);
            GameManager.Instance.HasCard = true;
        }
    }
}
