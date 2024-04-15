using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FrogSpawner : MonoBehaviour
{
    public GameObject frogToBeSpawned;
    public GameObject frogSpawnEffect;

    public Transform frogSpawnPosition;

    private const float EFFECT_DURATION = 1.406f;

    protected void SpawnFrog()
    {
        Instantiate(frogToBeSpawned, frogSpawnPosition.position, transform.rotation);
        Destroy(Instantiate(frogSpawnEffect, frogSpawnPosition.position, transform.rotation), EFFECT_DURATION);
    }
}
