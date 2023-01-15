using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetTracking : MonoBehaviour
{
    public NavMeshAgent agent;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void SetDestination(GameObject target)
    {
        var endPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        agent.destination = endPoint;
        DrawPath();
    }

    public void DrawPath()
    {
        var path = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, agent.destination, NavMesh.AllAreas, path);
        var positions = path.corners;

        // Draw route
        lineRenderer.widthMultiplier = 0.1F;
        lineRenderer.positionCount = positions.Length;
        for (int i = 0; i < positions.Length; i++)
        {
            Debug.Log(" point " + i + " = " + positions[i]);
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
}