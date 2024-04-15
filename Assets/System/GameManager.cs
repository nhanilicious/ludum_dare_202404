using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum FrogKnight
    {
        Croakaint = 0,
        Fromapond = 1,
        Hopsalot = 2,
        Ribbitan = 3
    }

    public static GameManager Instance;

    public GameObject player;

    private Dictionary<FrogKnight, bool> m_isSummoned = new Dictionary<FrogKnight, bool>();

    public event Action<FrogKnight> CompleteQuest;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        foreach (FrogKnight id in FrogKnight.GetValues(typeof(FrogKnight))) m_isSummoned[id] = false;
    }

    public void NotifyOfSummoning(GameObject obj)
    {
        if (obj.GetComponent<FollowerFrog>())
        {
            switch (obj.name)
            {
                case "Sir Croakaint":
                    m_isSummoned[FrogKnight.Croakaint] = true;
                    CompleteQuest?.Invoke(FrogKnight.Croakaint);
                    break;
                case "Sir Fromapond":
                    m_isSummoned[FrogKnight.Fromapond] = true;
                    CompleteQuest?.Invoke(FrogKnight.Fromapond);
                    break;
                case "Sir Hopsalot":
                    m_isSummoned[FrogKnight.Hopsalot] = true;
                    CompleteQuest?.Invoke(FrogKnight.Hopsalot);
                    break;
                case "Sir Ribbitan":
                    m_isSummoned[FrogKnight.Ribbitan] = true;
                    CompleteQuest?.Invoke(FrogKnight.Ribbitan);
                    break;
            }
        }
    }

    public bool IsFrogSpawned(FrogKnight id) { return m_isSummoned[id]; }
}
