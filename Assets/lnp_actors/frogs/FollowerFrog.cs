using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class FollowerFrog : MonoBehaviour
{
    private GameObject m_player;
    private NavMeshAgent m_agent;

    private const float STOP_RANGE = 2.0f;
    private const float CRAWL_RANGE = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");

        m_agent = GetComponent<NavMeshAgent>();
        m_agent.stoppingDistance = STOP_RANGE;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDestination();

        if (m_agent.remainingDistance >= STOP_RANGE)
            if (m_agent.remainingDistance >= CRAWL_RANGE) m_agent.speed = 2.0f;
            else m_agent.speed = 0.25f;
        else m_agent.speed = 0.1f;
    }

    private void UpdateDestination() { m_agent.destination = m_player.transform.position; }
}
