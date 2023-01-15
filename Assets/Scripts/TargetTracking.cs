using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetTracking : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public void SetDestination()
    {
        var endPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        agent.destination = endPoint;
    }
}