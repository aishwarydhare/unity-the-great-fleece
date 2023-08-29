using UnityEngine;
using UnityEngine.Playables;

public class GuardEyes : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCutscene;

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag + " visible to enemy");
        if (other.tag == "Player"){
            Debug.Log("darren caught");
            gameOverCutscene.SetActive(true);
        }
    }
}
