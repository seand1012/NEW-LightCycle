using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Credit to Walter Reid on github, https://gist.github.com/walterreid

public class NPCController : MonoBehaviour
{
    public NavMeshAgent m_NavMeshAgent { get; private set; }

    public NPCPatrol patrolPath;
    public int m_PathDestinationNodeIndex = 0;

    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        m_PathDestinationNodeIndex = patrolPath.UpdatePathDestination(gameObject.transform, m_PathDestinationNodeIndex);

        Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, m_PathDestinationNodeIndex);

        SetNavDestination(nextDestination);
    }

    public void SetNavDestination(Vector3 destination)
    {
        if (m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.SetDestination(destination);
        }
    }
}
