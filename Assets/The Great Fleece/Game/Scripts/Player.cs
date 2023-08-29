using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    // handle to Nav Mesh agent
    private NavMeshAgent navMeshAgent;
    private Animator darrenAnimator;
    private Vector3 destination;

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private AudioClip coinThrowSound;
    private GameObject coin;
    private bool coinThrown = false;

    // Start is called before the first frame update
    void Start()
    {
        // assign that handdle to the navmesh component
        navMeshAgent = GetComponent<NavMeshAgent>();
        darrenAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHitInfo;

            if (Physics.Raycast(rayOrigin, out raycastHitInfo)) {
                destination = raycastHitInfo.point;
                navMeshAgent.SetDestination(destination);
                darrenAnimator.SetBool("walk", true);
            }
        }

        // check if arrived at destination
        float distanceFromDestination = Vector3.Distance(transform.position, destination);
        if(distanceFromDestination < 1.0f) {
            darrenAnimator.SetBool("walk", false);
        }

        if (Input.GetMouseButtonDown(1) && !coinThrown){
            // stop darren if he is walking
            darrenAnimator.SetTrigger("throw");
            
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHitInfo;
            if (Physics.Raycast(rayOrigin, out raycastHitInfo)) {
                coin = Instantiate(coinPrefab, raycastHitInfo.point, Quaternion.identity);
                coin.SetActive(true);
                AudioSource.PlayClipAtPoint(coinThrowSound, raycastHitInfo.point);
                SendGuardToCoinSpot(raycastHitInfo.point);
                coinThrown = true;
            }
            
        }
    }

    private void SendGuardToCoinSpot(Vector3 coinPos) {
        GuardAI[] guards = GameObject.FindObjectsOfType<GuardAI>();
        foreach (var guard in guards)
        {
            guard.MoveToCoinPos(coinPos);
        }
    }
}
