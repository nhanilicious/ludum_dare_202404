using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class FollowerFrog : MonoBehaviour
{
    private GameObject m_player;
    private NavMeshAgent m_agent;
    private Animator m_animator;

    private const float STOP_RANGE = 2.0f;
    private const float CRAWL_RANGE = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");

        m_agent = GetComponent<NavMeshAgent>();
        m_agent.stoppingDistance = STOP_RANGE;
        UpdateDestination();

        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDestination();

        if (m_agent.remainingDistance >= STOP_RANGE)
        {
            if (m_agent.remainingDistance >= CRAWL_RANGE)
            {
                m_agent.speed = 2.0f;
                m_animator.SetFloat("Blend", Mathf.Min(m_animator.GetFloat("Blend") + Time.deltaTime * 1.2f, 1.0f));
            }
            else
            {
                m_agent.speed = 1.0f;

                if (m_animator.GetFloat("Blend") > 0.5f)
                {
                    m_animator.SetFloat("Blend", Mathf.Min(m_animator.GetFloat("Blend") - Time.deltaTime * 1.2f, 0.5f));
                }
                else if (m_animator.GetFloat("Blend") < 0.5f)
                {
                    m_animator.SetFloat("Blend", Mathf.Max(m_animator.GetFloat("Blend") + Time.deltaTime * 1.2f, 0.5f));
                }
            }
        }
        else
        {
            m_animator.SetFloat("Blend", Mathf.Max(m_animator.GetFloat("Blend") - Time.deltaTime * 1.2f, 0.0f));
        }
    }

    private void UpdateDestination() { m_agent.destination = m_player.transform.position; }
}
