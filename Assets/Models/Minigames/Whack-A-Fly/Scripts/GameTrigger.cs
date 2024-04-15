using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    private bool m_triggeredOnce = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_triggeredOnce && other.tag == "Player")
        {
            GetComponentInChildren<WhackaflyManager>().SpawnButterflies();
            m_triggeredOnce = true;
        }
    }
}
