using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class ChillingButterfly : MonoBehaviour
{
    private const float STOP_RANGE = 0.1f;
    private const float MAX_RANGE = 0.2f;

    private Vector3 m_originPos;
    private Vector3 m_waypoint;
    private bool m_waypointSet = false;
    private float m_restTime;

    // Start is called before the first frame update
    void Start()
    {
        m_originPos = transform.position;
        m_restTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_waypointSet && (m_restTime -= Time.deltaTime) <= 0) setWayPoint();

        if (m_waypointSet && Vector3.Distance(transform.position, m_waypoint) <= STOP_RANGE) stopMovement();
        else updatePosition();
    }

    void setWayPoint()
    {
        float x = Random.Range(-MAX_RANGE, MAX_RANGE);
        float y = Random.Range(-MAX_RANGE, MAX_RANGE);
        float z = Random.Range(-MAX_RANGE, MAX_RANGE);

        //float x = 1.0f, y = 0.0f, z = 0.0f;

        m_waypoint = m_originPos + new Vector3(x, y, z);
        m_waypointSet = true;

        //Debug.Log("m_originPos: " + m_originPos);
        //Debug.Log("m_waypoint: " + m_waypoint);

        Vector3 lookVector = (new Vector3(x - transform.position.x, 0, z - transform.position.z)).normalized;
        //Debug.Log("lookVector: " + lookVector);

        if (lookVector != Vector3.zero)
        //if (false)
        {
            transform.rotation = Quaternion.Euler(-60, 0, 90);
            float angle = Vector3.SignedAngle(lookVector, new Vector3(0, 0, -1), new Vector3(0, -1, 0));
            //Debug.Log("m_currentRot: " + m_currentRot);
            //Debug.Log("angle: " + angle);
            transform.Rotate(Vector3.up, angle, Space.World);
        }
    }

    void stopMovement()
    {
        m_waypointSet = false;
        m_restTime = Random.Range(10.0f, 20.0f);
    }

    void updatePosition()
    {
        transform.position += Vector3.Normalize(m_waypoint - transform.position) * Time.deltaTime * 0.3f;
    }

}
