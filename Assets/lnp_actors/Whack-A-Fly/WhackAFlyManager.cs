using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class WhackaflyManager : MonoBehaviour
{
    public static WhackaflyManager instance;

    public GameObject butterflyPrefab;
    public Vector3 butterflySpawnPosition = new Vector3(0.0f, 0.6f, 0.0f);
    public float butterflySpreadRadius = 5.0f;
    public int numberOfButterflies = 5;

    private int m_score = 0;
    private int m_targetScore = -1;

    private const float BASE_Y_SPREAD = 0.25f;

    private void Awake() { if (instance == null) { instance = this; } }

    // Start is called before the first frame update
    void Start() { /*SpawnButterflies();*/ }

    // Update is called once per frame
    void Update() { }

    public void SpawnButterflies()
    {
        m_targetScore = 0;

        for (int i = 0; i < numberOfButterflies; i++)
        {
            float x = Random.Range(butterflySpawnPosition.x - butterflySpreadRadius, butterflySpawnPosition.x + butterflySpreadRadius);
            float y = Random.Range(butterflySpawnPosition.y - BASE_Y_SPREAD, butterflySpawnPosition.y + BASE_Y_SPREAD);
            float z = Random.Range(butterflySpawnPosition.z - butterflySpreadRadius, butterflySpawnPosition.z + butterflySpreadRadius);

            Vector3 targetPos = new Vector3(x, y, z);

            GameObject butterfly = Instantiate(butterflyPrefab, targetPos, butterflyPrefab.transform.rotation);
            butterfly.AddComponent<RewardEnemy>();

            m_targetScore++;
        }
    }

    public void increaseScore(int delta = 1)
    {
        m_score += delta;

        //Debug.Log("Score increased.");

        //if (m_score == m_targetScore)
        //{
        //    Debug.Log("WIN");
        //}
    }
}
