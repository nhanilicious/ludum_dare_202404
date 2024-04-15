using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FrogSpawner : MonoBehaviour
{
    public GameObject frogToBeSpawned;
    public GameObject frogSpawnEffect;

    public Vector3 frogSpawnPosition;

    private const float EFFECT_DURATION = 1.406f;

    protected void SpawnFrog()
    {
        Instantiate(frogToBeSpawned, frogSpawnPosition, transform.rotation);
        Destroy(Instantiate(frogSpawnEffect, frogSpawnPosition, transform.rotation), EFFECT_DURATION);
    }
}
