using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCutscene;

    [SerializeField]
    private Material redLighConeMaterial;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player"){
            Debug.Log("darren caught");

            SecurityCamera[] securityCameras = GameObject.FindObjectsOfType<SecurityCamera>();
            foreach (var camera in securityCameras)
            {                
                MeshRenderer renderer = camera.GetComponent<MeshRenderer>();
                renderer.SetMaterials(new List<Material>() { redLighConeMaterial });
            }

            GetComponentInParent<Animator>().enabled = false;
            StartCoroutine(StartCutsceneAfterDelay());
        }
    }

    private IEnumerator StartCutsceneAfterDelay() {
        yield return new WaitForSeconds(0.5f);
        gameOverCutscene.SetActive(true);
        StopCoroutine(StartCutsceneAfterDelay());
    }
}
