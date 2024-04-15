using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEnemy : MonoBehaviour
{
    private void OnDestroy() { WhackaflyManager.instance.increaseScore(); }
}
