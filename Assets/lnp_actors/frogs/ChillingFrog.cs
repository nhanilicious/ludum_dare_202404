using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChillingFrog : MonoBehaviour
{
    private NavMeshAgent m_agent;

    private const float STOP_RANGE = 0.1f;
    private const float MAX_RANGE = 1.0f;

    private Vector3 m_originPos;
    private Vector3 m_waypoint;
    private bool m_waypointSet = false;
    private float m_restTime;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.stoppingDistance = STOP_RANGE;

        m_originPos = transform.position;
        m_restTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_waypointSet && (m_restTime -= Time.deltaTime) <= 0) setWayPoint();
        if (m_waypointSet && Vector3.Distance(transform.position, m_waypoint) <= STOP_RANGE) stopMovement();
    }

    void setWayPoint()
    {
        float x = Random.Range(-MAX_RANGE, MAX_RANGE);
        float z = Random.Range(-MAX_RANGE, MAX_RANGE);

        m_waypoint = m_originPos + new Vector3(x, 0, z);
        m_agent.SetDestination(m_waypoint);
        m_waypointSet = true;
        m_agent.speed = 0.25f;
    }

    void stopMovement()
    {
        m_waypointSet = false;
        m_agent.speed = 0.1f;
        m_restTime = Random.Range(10.0f, 20.0f);
    }

}
