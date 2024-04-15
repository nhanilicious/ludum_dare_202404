using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 SavedCoords;

    // Memory Game
    public bool IsSirCroakaintSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SaveCoords(Vector3 coords)
    {
        SavedCoords = coords;
    }
}
