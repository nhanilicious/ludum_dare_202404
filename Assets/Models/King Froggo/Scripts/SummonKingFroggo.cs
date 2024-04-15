using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonKingFroggo : MonoBehaviour
{
    public GameObject KingFroggo;
    public GameObject SpawnEffect;
    public Transform SpawnLocation;

    private const float EFFECT_DURATION = 1.406f;

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
        if (other.tag == "Player")
        {
            Instantiate(KingFroggo, SpawnLocation.transform.position, transform.rotation);
            Destroy(Instantiate(SpawnEffect, SpawnLocation.transform.position, transform.rotation), EFFECT_DURATION);
            Destroy(gameObject);
        }
    }
}
