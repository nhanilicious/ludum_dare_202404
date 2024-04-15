using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class WhackaflyManager : FrogSpawner
{
    public static WhackaflyManager instance;

    public GameObject butterflyPrefab;
    public GameObject effectPrefab;

    public Transform butterflySpawnPosition;
    public float butterflySpreadRadius = 2.0f;
    public int numberOfButterflies = 5;

    // for debugging
    public bool spawnButterflies = false;

    private int m_score = 0;
    private int m_targetScore = -1;

    private const float MIN_Y = 0.3f;
    private const float MAX_Y = 0.6f;

    private void Awake() { if (instance == null) { instance = this; } }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // for debugging
        if (spawnButterflies)
        {
            SpawnButterflies();
            spawnButterflies = false;
        }
    }

    public void SpawnButterflies()
    {
        m_targetScore = 0;

        for (int i = 0; i < numberOfButterflies; i++)
        {
            float x = Random.Range(butterflySpawnPosition.position.x - butterflySpreadRadius, butterflySpawnPosition.position.x + butterflySpreadRadius);
            float y = Random.Range(butterflySpawnPosition.position.y + MIN_Y, butterflySpawnPosition.position.y + MAX_Y);
            float z = Random.Range(butterflySpawnPosition.position.z - butterflySpreadRadius, butterflySpawnPosition.position.z + butterflySpreadRadius);

            Vector3 targetPos = new Vector3(x, y, z);

            GameObject butterfly = Instantiate(butterflyPrefab, targetPos, butterflyPrefab.transform.rotation);
            butterfly.AddComponent<RewardEnemy>();

            m_targetScore++;
        }

        GameObject effect = Instantiate(effectPrefab, butterflySpawnPosition.position, transform.rotation);
        effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * butterflySpreadRadius;
        Destroy(effect, 1.406f);
    }

    public void increaseScore(int delta = 1) { if ((m_score += delta) == m_targetScore) SpawnFrog(); }
}
