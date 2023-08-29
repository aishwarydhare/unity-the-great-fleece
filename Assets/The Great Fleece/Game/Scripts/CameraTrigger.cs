using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject newCamera;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            Debug.Log("player passing camera");
            Camera.main.transform.position = newCamera.transform.position;
            Camera.main.transform.rotation = newCamera.transform.rotation;
        }
    }
}
