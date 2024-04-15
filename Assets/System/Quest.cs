using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameManager.FrogKnight Knight;

    private void Start()
    {
        GameManager.Instance.CompleteQuest += CompleteQuest;
    }

    private void OnDestroy()
    {
        GameManager.Instance.CompleteQuest -= CompleteQuest;
    }

    private void CompleteQuest(GameManager.FrogKnight knight)
    {
        if (Knight == knight)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
