using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class FrogMoveAnimation : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_agent.speed)
        {
            case 3.0f:
                m_animator.SetFloat("Blend", Mathf.Min(m_animator.GetFloat("Blend") + Time.deltaTime * 1.2f, 1.0f));
                break;
            case 0.5f:
                if (m_animator.GetFloat("Blend") > 0.5f) m_animator.SetFloat("Blend", Mathf.Min(m_animator.GetFloat("Blend") - Time.deltaTime * 1.2f, 0.5f));
                else if (m_animator.GetFloat("Blend") < 0.5f) m_animator.SetFloat("Blend", Mathf.Max(m_animator.GetFloat("Blend") + Time.deltaTime * 1.2f, 0.5f));
                break;
            case 0.1f:
                m_animator.SetFloat("Blend", Mathf.Max(m_animator.GetFloat("Blend") - Time.deltaTime * 1.2f, 0.0f));
                break;
        }
    }
}
