using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    
    [SerializeField]
    private List<Transform> waypoints;
    [SerializeField]
    private int maxSecondsToWait = 5;
    private Animator guardAnimator;
    private NavMeshAgent navMeshAgent;
    private int targetIndex;
    private bool goBackNext;
    private bool isDistractedByCoin = false;
    private Vector3 coinDistractionPosition;

 
    // Start is called before the first frame update
    void Start()
    {
        guardAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(waypoints.Count > 0) {
            targetIndex = 0;
            navMeshAgent.SetDestination(waypoints[targetIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(waypoints.Count < 1) return;

        float distance;
        if(isDistractedByCoin) {
            distance = Vector3.Distance(transform.position, coinDistractionPosition);
            if(distance < 2.0f) {
                StartCoroutine(WaitBeforeMoving());
            }
        }

        distance = Vector3.Distance(transform.position, waypoints[targetIndex].position);
        if(distance < 1.0f && waypoints.Count > 1) {
            ChangeTarget();
        }
    }

    private void ChangeTarget() {
        Debug.Log("changing destination");

        int totalTargetIndexes = waypoints.Count - 1;
        
        if(targetIndex == 0) {
            goBackNext = false;
        }

        if(targetIndex < totalTargetIndexes && !goBackNext) {
            targetIndex += 1;
        } else if (targetIndex < totalTargetIndexes && goBackNext) {
            targetIndex -= 1;
        } else if (targetIndex == totalTargetIndexes) {
            goBackNext = true;
            targetIndex -= 1;
            
            // special handle for guard 3
            if(totalTargetIndexes == 3) {
                targetIndex = 1;
            }
        }

        // special handles for guard 3
        if(totalTargetIndexes == 3 && targetIndex == 2) {
            targetIndex = Random.Range(2, 4);
            goBackNext = true;
        }

        Debug.Log("moving to " + targetIndex);
        if(targetIndex == 1) {
            // means the guard is either at store location
            // or final location
            StartCoroutine(WaitBeforeMoving());
        } else {
            navMeshAgent.SetDestination(waypoints[targetIndex].position);
        }            
    }
    
    private IEnumerator WaitBeforeMoving(){
        guardAnimator.SetBool("walk", false);
        int waitFor = Random.Range(2, maxSecondsToWait+1);
        Debug.Log("waiting for " + waitFor);
        yield return new WaitForSeconds(waitFor);        
        navMeshAgent.SetDestination(waypoints[targetIndex].position);        
        guardAnimator.SetBool("walk", true);
        StopCoroutine(WaitBeforeMoving());
        isDistractedByCoin = false;
    }

    public void MoveToCoinPos(Vector3 coinPos){
        isDistractedByCoin = true;
        coinDistractionPosition = coinPos;
        navMeshAgent.SetDestination(coinDistractionPosition);
    }
}
