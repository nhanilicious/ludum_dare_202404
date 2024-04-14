using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public CapsuleCollider TongueCollider;

    public void StartAttack()
    {
        TongueCollider.enabled = true;
    }

    public void EndAttack()
    {
        TongueCollider.enabled = false;
    }
}
