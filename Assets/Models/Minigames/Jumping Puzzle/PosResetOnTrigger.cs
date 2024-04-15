using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosResetOnTrigger : MonoBehaviour
{
    public Transform playerResetPosition;

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
            GameObject player = GameManager.Instance.player;
            //FrogCharacterController charControllerScript = player.GetComponent<FrogCharacterController>();
            //CharacterController charController = player.GetComponent<CharacterController>();

            //charControllerScript.enabled = false;
            //charController.enabled = false;

            player.transform.position = playerResetPosition.position;
            Physics.SyncTransforms();

            //charControllerScript.enabled = true;
            //charController.enabled = true;
        }
    }
}
